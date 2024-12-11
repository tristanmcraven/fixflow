using fixflow.Model;
using fixflow.Pages;
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
using System.Windows.Shapes;

namespace fixflow.Windows
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private Grid _selectedSetting = new Grid();
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void settings_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedSetting = ((ListBoxItem)settings_ListBox.SelectedItem).Content as Grid;
            currentSetting_TextBlock.Text = (_selectedSetting.Children.OfType<TextBlock>().First()).Text;
            
            switch (settings_ListBox.SelectedIndex)
            {
                case 0:
                    PageManager.SettingsFrame.Navigate(new GeneralSettingsPage());
                    break;
                case 1:
                    PageManager.SettingsFrame.Navigate(new ProgramSettingsPage());
                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PageManager.SettingsFrame = Frame;
            settings_ListBox.SelectedIndex = 0;
        }

        private void SettingsWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Settings.WindowLocations.Add(this.Name, new WindowLocation(this.Top, this.Left));
            App.Settings.WindowSizes.Add(this.Name, new WindowSize(this.ActualWidth, this.ActualHeight));
        }
    }
}
