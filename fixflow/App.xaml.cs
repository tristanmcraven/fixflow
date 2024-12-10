using fixflow.Model;
using System.Configuration;
using System.Data;
using System.Windows;

namespace fixflow
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Settings Settings { get; set; }

        public void UpdateSettings()
        {
            Settings = SettingsManager.GetSettings();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //base.OnStartup(e);

            UpdateSettings();
        }
    }

}
