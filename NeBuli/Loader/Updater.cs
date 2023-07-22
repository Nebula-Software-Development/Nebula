using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading.Tasks;
using Nebuli.API.Features;
using Newtonsoft.Json;
using PluginAPI.Helpers;
using RoundRestarting;

namespace Nebuli.Loader
{
    public class Updater
    {
        internal static FileStream PendingUpdate = null;
        internal static Stream Stream = null;

        public void CheckForUpdates()
        {
            Log.Info("Checking for updates...", "Updater");
            Task.Run(CheckForUpdatesAsync);
        }

        private HttpClient CreateHttpClient()
        {
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(480);
            client.DefaultRequestHeaders.Add("User-Agent", $"NebuliUpdater (https://github.com/NotIntense/Nebuli,{NebuliInfo.NebuliVersion})");
            return client;
        }

        private async Task CheckForUpdatesAsync()
        {
            try
            {
                using HttpClient client = CreateHttpClient();
                string latestReleaseUrl = "https://api.github.com/repos/NotIntense/Nebuli/releases/latest";
                string responseBody = await client.GetStringAsync(latestReleaseUrl);

                GitHubRelease latestRelease = JsonConvert.DeserializeObject<GitHubRelease>(responseBody);
                if (latestRelease == null || string.IsNullOrEmpty(latestRelease.AssetsUrl))
                {
                    Log.Error("Failed to get the latest release information from GitHub.", "Updater");
                    return;
                }

                // Get the download URL of the DLL from the latest release
                string dllDownloadUrl = GetDllDownloadUrl(latestRelease.AssetsUrl, client);
                if (dllDownloadUrl == null)
                {
                    Log.Error("Failed to get the DLL download URL from the latest release.", "Updater");
                    return;
                }

                if(Version.Parse(latestRelease.TagName) > NebuliInfo.NebuliVersion)
                {
                    Log.Info($"A new Nebuli version, ({latestRelease.TagName}), is available on GitHub. Installing...", "Updater");
                    Update(client, dllDownloadUrl);
                }
                else
                {
                    Log.Info("Nebuli is up-to-date!");
                    return;
                }

            }
            catch (Exception ex)
            {
                Log.Error($"Error occurred while checking for updates: {ex}", "Updater");
            }
        }

        private string GetDllDownloadUrl(string assetsUrl, HttpClient client)
        {
            try
            {
                string responseBody = client.GetStringAsync(assetsUrl).GetAwaiter().GetResult();
                GitHubAsset[] assets = JsonConvert.DeserializeObject<GitHubAsset[]>(responseBody);
                foreach (GitHubAsset asset in assets)
                {
                    if (asset.Name.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
                    {
                        return asset.BrowserDownloadUrl;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to get the DLL download URL: {ex}", "Updater");
            }

            return null;
        }

        private void Update(HttpClient client, string dllDownloadUrl)
        {
            try
            {
                Log.Info("Downloading new update...");
                using (HttpResponseMessage installer = client.GetAsync(dllDownloadUrl).ConfigureAwait(false).GetAwaiter().GetResult())
                {
                    Log.Info("Downloaded!");
                    using (Stream installerStream = installer.Content.ReadAsStreamAsync().ConfigureAwait(false).GetAwaiter().GetResult())
                    {
                        string destinationFilePath = Path.Combine(PluginAPI.Helpers.Paths.GlobalPlugins.Plugins, "Nebuli.dll");

                        using (FileStream fs = new(destinationFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            PendingUpdate = fs;
                            installerStream.CopyTo(fs);
                            Log.Info("Auto-update complete! It will be installed once the server restarts!");
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(Update)} throw an exception");
                Log.Error(ex);
            }
        }


        private class GitHubRelease
        {
            [JsonProperty("tag_name")]
            public string TagName { get; set; }

            [JsonProperty("assets_url")]
            public string AssetsUrl { get; set; }
        }

        private class GitHubAsset
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("browser_download_url")]
            public string BrowserDownloadUrl { get; set; }
        }
    }
}

