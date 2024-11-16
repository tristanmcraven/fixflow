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
    /// Interaction logic for AddBrandWindow.xaml
    /// </summary>
    public partial class AddBrandWindow : Window
    {
        public bool DialogResult = false;
        public AddBrandWindow()
        {
            InitializeComponent();
        }

        private async void addBrand_Button_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(brand_TextBox.Text))
            {
                MessageBox.Show(Rm.Get("enter_brand_name"), Rm.Get("error"), MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var brand = await ApiClient.DeviceBrand.GetByName(brand_TextBox.Text);
            if (brand != null)
            {
                MessageBox.Show(Rm.Get("brand_exists"), Rm.Get("error"), MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if ((await ApiClient.DeviceBrand.Post(brand_TextBox.Text)) == true)
            {
                DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show(Rm.Get("smth_went_wrong"), Rm.Get("error"), MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
