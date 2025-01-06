using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;

class Program
{
    private const string RepoOwner = "tristanmcraven";
    private const string RepoName = "fixflow";
    private const string MainExeName = "FixFlow.exe"; // Main program name
    private const string DownloadDirectory = "Update"; // Temporary folder for update
    private static readonly string CurrentVersion = GetCurrentVersion().ToString();

    static async Task Main(string[] args)
    {
        Console.WriteLine($"Current version: {CurrentVersion}");
        var latestRelease = await GetLatestRelease();

        if (latestRelease == null)
        {
            Console.WriteLine("Failed to fetch release information. Exiting.");
            return;
        }

        Console.WriteLine($"Latest version: {latestRelease.TagName}");
        if (IsNewerVersion(latestRelease.TagName, CurrentVersion))
        {
            Console.WriteLine("New version found! Starting update...");
            await Update(latestRelease);
        }
        else
        {
            Console.WriteLine("No update available.");
        }

        LaunchMainApp();
    }

    private static Version GetCurrentVersion()
    {
        const string fixflowExe = "fixflow.exe";

        if (!File.Exists(fixflowExe))
        {
            Console.WriteLine($"{fixflowExe} not found. Downloading program...");
            return new Version(0, 0, 0);
        }

        try
        {
            var versionInfo = FileVersionInfo.GetVersionInfo(fixflowExe);
            if (Version.TryParse(versionInfo.FileVersion, out var version))
            {
                return version;
            }
            else
            {
                Console.WriteLine("Failed to parse version from fixflow.exe. Assuming lowest version (0.0.0).");
                return new Version(0, 0, 0);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving version of {fixflowExe}: {ex.Message}");
            return new Version(0, 0, 0); // Default to lowest version on error
        }
    }


    private static async Task<GitHubRelease> GetLatestRelease()
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.UserAgent.ParseAdd("FixFlow-Updater"); // Required by GitHub API
        var url = $"https://api.github.com/repos/{RepoOwner}/{RepoName}/releases/latest";

        try
        {
            var response = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<GitHubRelease>(response);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching latest release: {ex.Message}");
            return null;
        }
    }

    private static bool IsNewerVersion(string latestTag, string currentVersion)
    {
        return Version.Parse(latestTag.TrimStart('v')) > Version.Parse(currentVersion);
    }

    private static async Task Update(GitHubRelease release)
    {
        var asset = release.Assets.Find(a => a.Name.EndsWith(".zip"));
        if (asset == null)
        {
            Console.WriteLine("No suitable asset found for update.");
            return;
        }

        Console.WriteLine($"Downloading update: {asset.BrowserDownloadUrl}");
        Directory.CreateDirectory(DownloadDirectory);

        var zipPath = Path.Combine(DownloadDirectory, "update.zip");

        // Download the file
        using (var client = new HttpClient())
        {
            var response = await client.GetAsync(asset.BrowserDownloadUrl);
            using (var fs = new FileStream(zipPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await response.Content.CopyToAsync(fs);
            }
        }

        Console.WriteLine("Download complete. Extracting files...");

        // Extract after closing the file stream
        ZipFile.ExtractToDirectory(zipPath, DownloadDirectory, overwriteFiles: true);

        Console.WriteLine("Replacing old files...");
        try
        {
            var files = Directory.GetFiles(DownloadDirectory);
            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                File.Copy(file, fileName, overwrite: true);
            }
        }
        catch (Exception)
        {

        }

        Console.WriteLine("Update complete!");
    }


    private static void LaunchMainApp()
    {
        Console.WriteLine("Launching main application...");
        Process.Start(MainExeName);
    }
}

public class GitHubRelease
{
    [JsonProperty("tag_name")]
    public string TagName { get; set; }

    [JsonProperty("assets")]
    public List<GitHubAsset> Assets { get; set; }
}

public class GitHubAsset
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("browser_download_url")]
    public string BrowserDownloadUrl { get; set; }
}
