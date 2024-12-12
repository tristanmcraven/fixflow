using fixflow.Model;
using fixflow.Utility;
using fixflow.Windows;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
using System.Windows;

namespace fixflow
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Settings Settings { get; set; }

        public static readonly string url = $"https://api.github.com/repos/tristanmcraven/fixflow/releases/latest";
        public static readonly string VisitUrl = $"https://github.com/tristanmcraven/fixflow/releases/latest";

        public void UpdateSettings()
        {
            Settings = SettingsManager.GetSettings();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //base.OnStartup(e);

            UpdateSettings();
            UpdateApp();
            if (App.Settings.CheckForUpdates == true) CheckForNewVersion();
        }

        public async void UpdateApp()
        {
            var lastVersion = (await GetLatestRelease());
            if (!GetAppVersion().Equals(lastVersion))
            {
                if (Settings.EnableAutoUpdates == true)
                {
                    Process.Start("updater.exe");
                    Application.Current.Shutdown();
                }
            }
        }

        public async void CheckForNewVersion()
        {
            await Task.Delay(4000);
            var lastVersion = (await GetLatestRelease());
            if (!GetAppVersion().Equals(lastVersion))
            {
                var mw = WindowManager.Get<MainWindow>();
                mw.lower_Grid.Visibility = Visibility.Visible;
                mw.update_StackPanel.Visibility = Visibility.Visible;
                mw.newVersion_TextBlock.Text = $"({lastVersion})";
            }
        }
        private static async Task<string?> GetLatestRelease()
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd("FixFlow-Updater"); // required
            try
            {
                var response = await client.GetStringAsync(url);
                var returnBody = JsonConvert.DeserializeObject<GitHubRelease>(response).TagName;

                while (returnBody.EndsWith('0'))
                {
                    returnBody = returnBody.Substring(0, returnBody.LastIndexOf('.'));
                }
                return returnBody;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private class GitHubRelease
        {
            [JsonProperty("tag_name")]
            public string TagName { get; set; }
        }

        public string GetAppVersion()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString();
            if (string.IsNullOrEmpty(version)) return "";
            while (version.EndsWith(".0"))
            {
                version = version.Substring(0, version.LastIndexOf('.'));
            }
            return $"v{version}";
        }

    }

}
