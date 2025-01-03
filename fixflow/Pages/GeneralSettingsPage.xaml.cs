using fixflow.Model;
using fixflow.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for GeneralSettingsPage.xaml
    /// </summary>
    public partial class GeneralSettingsPage : Page
    {
        public GeneralSettingsPage()
        {
            InitializeComponent();
        }

        private void rememberWindowLocation_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SettingsManager.UpdateSetting("RememberWindowLocation", true);
        }

        private void rememberWindowLocation_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            SettingsManager.UpdateSetting("RememberWindowLocation", false);
        }

        private void rememberWindowSize_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SettingsManager.UpdateSetting("RememberWindowSize", true);
        }

        private void rememberWindowSize_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            SettingsManager.UpdateSetting("RememberWindowSize", false);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            rememberWindowSize_CheckBox.IsChecked = App.Settings.RememberWindowSize;
            rememberWindowLocation_CheckBox.IsChecked = App.Settings.RememberWindowLocation;
            theme_ComboBox.SelectedIndex = (int)App.Settings.AppTheme;
            language_ComboBox.SelectedIndex = (int)App.Settings.AppLanguage;
        }

        private void theme_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = (AppTheme)theme_ComboBox.SelectedIndex;
            SettingsManager.UpdateSetting("AppTheme", index);
            SettingsManager.UpdateAppTheme(index);
        }

        private void language_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var index = (AppTheme)language_ComboBox.SelectedIndex;
            //SettingsManager.UpdateSetting("AppLanguage", index);
        }
    }
}
