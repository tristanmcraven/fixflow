using fixflow.Model;
using fixflow.Utility;
using fixflow.Windows;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
using System.Windows;
using System.Net;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace fixflow
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Settings Settings { get; set; }
        public static Backup Backup { get; set; }

        public static readonly string url = $"https://api.github.com/repos/tristanmcraven/fixflow/releases/latest";
        public static readonly string VisitUrl = $"https://github.com/tristanmcraven/fixflow/releases/latest";

        public static bool OfflineMode = false;
        public static bool HasInternetConnection = true;
        

        public void UpdateSettings()
        {
            Settings = SettingsManager.GetSettings();
            
        }

        //backup logic is in mainwindow.xaml.cs;
        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            //base.OnStartup(e);

            UpdateSettings();
            UpdateApp();
            SettingsManager.UpdateLangugage(Settings.AppLanguage);
            SettingsManager.UpdateAppTheme((AppTheme)Settings.AppTheme);

            if (App.Settings.CheckForUpdates == true) CheckForNewVersion();

            

            //await BackupManager.CreateBackup();
            //BackupManager.SetBackup();
            //CheckConnection();
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
            await Task.Delay(7000);
            var lastVersion = (await GetLatestRelease());
            if (!GetAppVersion().Equals(lastVersion))
            {
                var mw = WindowManager.Get<MainWindow>();
                mw.lower_Grid.Visibility = Visibility.Visible;
                mw.update_StackPanel.Visibility = Visibility.Visible;
                mw.newVersion_TextBlock.Text = $"({lastVersion})";

                if (mw.offlineMode_StackPanel.Visibility == Visibility.Visible)
                {
                    mw.update_StackPanel.Background = new LinearGradientBrush
                    {
                        StartPoint = new Point(0, 0.5),
                        EndPoint = new Point(1, 0.5),
                        GradientStops =
                        {
                            new GradientStop
                            {
                                Color = Rm.GetColor("blue_update"),
                                Offset = 0
                            },
                            new GradientStop
                            {
                                Color = Rm.GetColor("blue_update"),
                                Offset = 0.465
                            },
                            new GradientStop
                            {
                                Color = Colors.Transparent,
                                Offset = 0.565
                            },
                            new GradientStop
                            {
                                Color = Colors.Transparent,
                                Offset = 1
                            }
                        }
                    };
                }
            }
        }

        public static async Task CheckConnection()
        {
            try
            {
                var response = await new HttpClient().GetAsync("https://www.google.com/generate_204");
                if (response.IsSuccessStatusCode)
                {
                    App.HasInternetConnection = true;
                    try
                    {
                        var db = await new HttpClient().GetAsync("http://localhost:5108/api/devicebrand");
                    }
                    catch
                    {
                        App.OfflineMode = true;
                        BackupManager.SetBackup();
                    }
                }
            }
            catch
            {
                App.HasInternetConnection = false;
                App.OfflineMode = true;
                BackupManager.SetBackup();
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

        private void Application_Exit(object sender, ExitEventArgs e)
        {

        }
    }

}
