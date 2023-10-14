using Newtonsoft.Json;
using PluginAPI.Core;
using PluginAPI.Loader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Log = Nebuli.API.Features.Log;
using Server = Nebuli.API.Features.Server;

namespace Nebuli.Loader;

public class Updater
{
    internal static FileStream PendingUpdate = null;
    internal static Stream Stream = null;
    internal static string NeubliPath;

    public void CheckForUpdates()
    {
        Log.Info("Checking for updates...", "Updater");
        NeubliPath = FindNebuliPath();
        Task.Run(CheckForUpdatesAsync);
    }

    private HttpClient CreateHttpClient()
    {
        HttpClient client = new()
        {
            Timeout = TimeSpan.FromSeconds(480)
        };
        client.DefaultRequestHeaders.Add("User-Agent", $"NebuliUpdater (https://github.com/NotIntense/Nebuli,{NebuliInfo.NebuliVersion})");
        return client;
    }

    private async Task CheckForUpdatesAsync()
    {
        try
        {
            using HttpClient client = CreateHttpClient();
            string latestReleaseUrl = "https://api.github.com/repos/Nebuli-Team/Nebuli/releases/latest";
            string responseBody = await client.GetStringAsync(latestReleaseUrl);

            GitHubRelease latestRelease = JsonConvert.DeserializeObject<GitHubRelease>(responseBody);
            if (latestRelease == null || string.IsNullOrEmpty(latestRelease.AssetsUrl))
            {
                Log.Error("Failed to get the latest release information from GitHub.", "Updater");
                return;
            }
            string dllDownloadUrl = GetDllDownloadUrl(latestRelease.AssetsUrl, client);
            if (dllDownloadUrl == null)
            {
                Log.Error("Failed to get the DLL download URL from the latest release.", "Updater");
                return;
            }

            string latestVersion = latestRelease.TagName.Replace("-alpha", "");

            if (Version.Parse(latestVersion) > NebuliInfo.NebuliVersion)
            {
                Log.Info($"A new Nebuli version, ({latestRelease.TagName}), is available on GitHub. Preparing download...", "Updater");
                Update(client, dllDownloadUrl);
            }
            else
            {
                Log.Info("Nebuli is up-to-date!", "Updater");
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
            Log.Info("Prepare complete! Downloading new update...");
            using HttpResponseMessage installer = client.GetAsync(dllDownloadUrl).ConfigureAwait(false).GetAwaiter().GetResult();
            Log.Info("Downloaded!");
            using Stream installerStream = installer.Content.ReadAsStreamAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            using FileStream fs = new(NeubliPath, FileMode.Create, FileAccess.Write, FileShare.None);
            PendingUpdate = fs;
            installerStream.CopyTo(fs);
            Log.Info("Auto-update complete! It will be installed once the server restarts!");
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

    public static string FindNebuliPath()
    {
        KeyValuePair<Type, PluginHandler> nebuliPlugin =
            AssemblyLoader.Plugins.SelectMany(assemblyEntry => assemblyEntry.Value)
            .FirstOrDefault(pluginEntry => pluginEntry.Value.PluginName == "Nebuli Loader" && pluginEntry.Value.PluginVersion == NebuliInfo.NebuliVersionConst);
        return nebuliPlugin.Value.PluginFilePath;
    }

    internal static void ForceInstall(string url)
    {
        if (!IsGitHubUrl(url))
        {
            Log.Info("The provided URL is not a valid GitHub URL. Only Github URLs are allowed for security reasons.");
            return;
        }

        Log.Info($"Force installing Nebuli from {url}...");
        using WebClient client = new();
        string filePath = FindNebuliPath();

        try
        {
            client.DownloadFile(url, filePath);
            Log.Info($"DLL downloaded and saved at: {filePath}");
        }
        catch (Exception ex)
        {
            Log.Info($"An error occurred while downloading the DLL: {ex}");
        }

        Log.Info("Download complete! Restarting server...");
        Server.RestartServer();
    }

    private static bool IsGitHubUrl(string url)
    {
        string githubPattern = @"^(https?:\/\/)?(www\.)?github\.com\/.*";
        return Regex.IsMatch(url, githubPattern, RegexOptions.IgnoreCase);
    }
}