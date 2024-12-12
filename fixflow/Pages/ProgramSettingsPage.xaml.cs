using fixflow.Model;
using fixflow.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace fixflow.Pages
{
    /// <summary>
    /// Interaction logic for ProgramSettingsPage.xaml
    /// </summary>
    public partial class ProgramSettingsPage : Page
    {
        public ProgramSettingsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            autoUpdate_CheckBox.IsChecked = App.Settings.EnableAutoUpdates;
            checkForUpdates_CheckBox.IsChecked = App.Settings.CheckForUpdates;
            var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            appVersion_TextBlock.Text = "Версия: " + version.Substring(0, version.LastIndexOf('.'));
        }

        private void autoUpdate_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SettingsManager.UpdateSetting("EnableAutoUpdates", true);
        }

        private void autoUpdate_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            SettingsManager.UpdateSetting("EnableAutoUpdates", false);
        }

        private void checkForUpdates_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SettingsManager.UpdateSetting("CheckForUpdates", true);
        }

        private void checkForUpdates_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            SettingsManager.UpdateSetting("CheckForUpdates", false);
        }
    }
}
