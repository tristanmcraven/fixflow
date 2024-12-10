using fixflow.Model;
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
        }

        private void autoUpdate_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SettingsManager.UpdateSetting("EnableAutoUpdates", true);
        }

        private void autoUpdate_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            SettingsManager.UpdateSetting("EnableAutoUpdates", false);
        }
    }
}
