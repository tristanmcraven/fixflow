using fixflow.Utility;
using System.Windows;
using System.Windows.Controls;

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
            AddMalfunction();
        }

        private void addKit_Button_Click(object sender, RoutedEventArgs e)
        {
            AddKit();
        }

        private void addTicket_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadingOverlay.Show(this);
            await UpdateBrands();
            await UpdateModels();
            await UpdateStatuses();
            AddMalfunction();
            AddKit();
            LoadingOverlay.Remove(this);
        }

        private async Task UpdateBrands()
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

        private async Task UpdateModels()
        {
            var models = await ApiClient.DeviceBrand.GetModelsByName(brands_ComboBox.SelectedItem.ToString());
            models_ComboBox.Items.Clear();
            if (models != null)
            {
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
            else models_ComboBox.IsEnabled = false;
        }

        private async void addBrand_Button_Click(object sender, RoutedEventArgs e)
        {
            var abw = new AddBrandWindow()
            {
                Owner = this
            };
            abw.ShowDialog();
            LoadingOverlay.Show(this);
            if (abw.DialogResult == true)
            {
                await UpdateBrands();
            }
            LoadingOverlay.Remove(this);
        }

        private async void addModel_Button_Click(object sender, RoutedEventArgs e)
        {
            var amw = new AddModelWindow(brands_ComboBox.SelectedItem.ToString()!)
            {
                Owner = this
            };
            amw.ShowDialog();
            LoadingOverlay.Show(this);
            if (amw.DialogResult == true)
            {
                await UpdateModels();
                models_ComboBox.SelectedItem = amw.NewModel;
            }
            LoadingOverlay.Remove(this);
        }

        private async void brands_ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            await UpdateModels();
        }

        private async Task UpdateStatuses()
        {
            var statuses = (await ApiClient.Status.Get()).OrderBy(s => s.Id).ToList();
            status_ComboBox.Items.Clear();
            foreach (var status in statuses)
            {
                status_ComboBox.Items.Add(status.Name);
            }
            status_ComboBox.SelectedIndex = 0;
        }

        private void AddMalfunction()
        {
            malfunctions_StackPanel.Children.Add(new TextBox());
        }

        private void AddKit()
        {
            kit_StackPanel.Children.Add(new TextBox());
        }
    }
}
