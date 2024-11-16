using fixflow.Utility;
using System.Windows;

namespace fixflow.Windows
{
    /// <summary>
    /// Interaction logic for AddTicketWindow.xaml
    /// </summary>
    public partial class AddTicketWindow : Window
    {
        public AddTicketWindow()
        {
            InitializeComponent();
        }

        private void addMalfunction_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addKit_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addTicket_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateBrands();
            UpdateModels();
        }

        private async void UpdateBrands()
        {
            var brands = await ApiClient.DeviceBrand.Get();
            brands_ComboBox.Items.Clear();
            if (brands.Any())
            {
                foreach (var brand in brands)
                {
                    brands_ComboBox.Items.Add(brand.Name);
                }
                brands_ComboBox.IsEnabled = true;
                brands_ComboBox.SelectedIndex = 0;
            }
            else brands_ComboBox.IsEnabled = false;
        }

        private async void UpdateModels()
        {
            var models = await ApiClient.DeviceModel.Get();
            models_ComboBox.Items.Clear();
            if (models.Any())
            {
                foreach (var model in models)
                {
                    models_ComboBox.Items.Add(model.Name);
                }
                models_ComboBox.IsEnabled = true;
                models_ComboBox.SelectedIndex = 0;
            }
            else models_ComboBox.IsEnabled = false;
        }

        private void addBrand_Button_Click(object sender, RoutedEventArgs e)
        {
            var abw = new AddBrandWindow()
            {
                Owner = this
            };
            abw.ShowDialog();
            if (abw.DialogResult == true) UpdateBrands();
        }
    }
}
