using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Nebuli.API.Features;
using Newtonsoft.Json;
using FileMode = System.IO.FileMode;

namespace Nebuli.Loader;

#pragma warning disable CS4014
public class Updater
{
    public static void CheckForUpdates()
    {
        Log.Info("Checking for updates...", "Updater");
        CheckForUpdatesAsync();
    }

    public static async Task<bool> CheckForUpdatesAsync()
    {
        try
        {
            Log.Info("passing");
            Version currentVersion = NebuliInfo.NebuliVersion;

            string latestReleaseUrl = $"https://github.com/NotIntense/Nebuli/releases/latest";
            using HttpClient client = new();
            client.DefaultRequestHeaders.Add("User-Agent", "NebuliUpdater");

            HttpResponseMessage response = await client.GetAsync(latestReleaseUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            GitHubRelease latestRelease = JsonConvert.DeserializeObject<GitHubRelease>(responseBody);

            if (latestRelease == null || latestRelease.TagName == null)
            {
                Log.Error("Failed to get the latest release information from GitHub.", "Updater");
                return false;
            }

            Version latestVersion = new(latestRelease.TagName.TrimStart('v'));
            if (latestVersion > currentVersion)
            {
                Log.Info($"A new Nebuli version, ({latestVersion}), is available on GitHub. Installing...", "Updater");
                string updateUrl = latestRelease.Assets[0].BrowserDownloadUrl;
                await DownloadAndReplaceDllAsync(updateUrl);
                return true;
            }
            else
            {
                Log.Info($"Nebuli is up-to-date : ({currentVersion})");
            }

            return false;
        }
        catch (Exception ex)
        {
            Log.Error($"Error occurred while checking for updates: {ex}", "Nebuli");
            return false;
        }
    }


    private static async Task DownloadAndReplaceDllAsync(string updateUrl)
    {
        using (HttpClient client = new HttpClient())
        {
            using Stream stream = await client.GetStreamAsync(updateUrl);
            using FileStream fileStream = new FileStream(Paths.MainDirectory.FullName, FileMode.Create);
            await stream.CopyToAsync(fileStream);
        }

        string destinationFilePath = Paths.MainDirectory.FullName;
        bool replacementSuccessful = TryReplaceUpdatedDll(destinationFilePath);

        if (replacementSuccessful)
        {
            Log.Info($"New Nebuli version has been succesfully installed. Server will restart after this round.", "Updater");
            Server.NextRoundAction = ServerStatic.NextRoundAction.Restart;
        }
        else
        {
            Log.Error("Failed to replace the DLL file. Make sure you have proper permissions and check for any errors.", "Updater");
        }
    }

    private class GitHubRelease
    {
        [JsonProperty("tag_name")]
        public string TagName { get; set; }

        [JsonProperty("assets")]
        public GitHubReleaseAsset[] Assets { get; set; }
    }

    private class GitHubReleaseAsset
    {
        [JsonProperty("browser_download_url")]
        public string BrowserDownloadUrl { get; set; }
    }

    private static bool TryReplaceUpdatedDll(string destinationFilePath)
    {
        try
        {
            string currentAssemblyPath = Assembly.GetExecutingAssembly().Location;
            File.Copy(destinationFilePath, currentAssemblyPath, true);
            File.Delete(destinationFilePath);
            return true;
        }
        catch (Exception ex)
        {
            Log.Error($"Failed to replace the DLL: {ex}", "Updater");
            return false;
        }
    }
}
