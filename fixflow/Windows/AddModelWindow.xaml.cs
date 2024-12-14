using fixflow.Utility;
using System.Windows;

namespace fixflow.Windows
{
    /// <summary>
    /// Interaction logic for AddModelWindow.xaml
    /// </summary>
    public partial class AddModelWindow : Window
    {
        public bool DialogResult = false;
        public string NewModel = "";
        public AddModelWindow(string brand)
        {
            InitializeComponent();
            brands_ComboBox.Items.Add(brand);
            brands_ComboBox.SelectedIndex = 0;
        }

        private async void addModel_Button_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(model_TextBox.Text))
            {
                MessageBox.Show(Rm.Get("enter_model_name"), Rm.Get("error"), MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            LoadingOverlay.Show(this);
            var model = await ApiClient.DeviceModel.GetByName(model_TextBox.Text);
            if (model != null)
            {
                MessageBox.Show(Rm.Get("model_exists"), Rm.Get("error"), MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                var brand = await ApiClient.DeviceBrand.GetByName(brands_ComboBox.SelectedItem.ToString()!);
                var result = await ApiClient.DeviceModel.Post(brand.Guid, model_TextBox.Text);
                if (result)
                {
                    DialogResult = true;
                    NewModel = model_TextBox.Text;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(Rm.Get("smth_went_wrong"), Rm.Get("error"), MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            LoadingOverlay.Remove(this);
        }
    }
}
