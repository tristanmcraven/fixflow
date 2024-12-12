using fixflow.Model;
using fixflow.Utility;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowManager.SetProperties(this);
            LoadingOverlay.Show(this);
            await UpdateBrands();
            await UpdateModels();
            await UpdateStatuses();
            AddMalfunction();
            AddKit();
            LoadingOverlay.Remove(this);
            clientName_TextBox.Focus();
        }

        private void addMalfunction_Button_Click(object sender, RoutedEventArgs e)
        {
            AddMalfunction();
        }

        private void addKit_Button_Click(object sender, RoutedEventArgs e)
        {
            AddKit();
        }

        private async void addTicket_Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsSomethingEmpty())
            {
                MessageBox.Show(Rm.Get("smth_empty"), Rm.Get("error"), MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            LoadingOverlay.Show(this);
            var result = await PostTicketAsync();
            if (!result)
            {
                MessageBox.Show(Rm.Get("smth_went_wrong"), Rm.Get("error"), MessageBoxButton.OK, MessageBoxImage.Error);
                LoadingOverlay.Remove(this);
                return;
            }
            else
            {
                this.Close();
            }
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
            var selectedItem = brands_ComboBox.SelectedItem;
            if (selectedItem != null)
            {
                var models = await ApiClient.DeviceBrand.GetModelsByName(selectedItem.ToString()!);
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
        }

        private async void addBrand_Button_Click(object sender, RoutedEventArgs e)
        {
            addBrand_Grid.Visibility = Visibility.Visible;
            addBrand_Button.Visibility = Visibility.Collapsed;
            //var abw = new AddBrandWindow()
            //{
            //    Owner = this
            //};
            //abw.ShowDialog();
            //LoadingOverlay.Show(this);
            //if (abw.DialogResult == true)
            //{
            //    await UpdateBrands();
            //}
            //LoadingOverlay.Remove(this);
        }

        private async void addModel_Button_Click(object sender, RoutedEventArgs e)
        {
            addModel_Grid.Visibility = Visibility.Visible;
            addModel_Button.Visibility = Visibility.Collapsed;
            //var amw = new AddModelWindow(brands_ComboBox.SelectedItem.ToString()!)
            //{
            //    Owner = this
            //};
            //amw.ShowDialog();
            //LoadingOverlay.Show(this);
            //if (amw.DialogResult == true)
            //{
            //    await UpdateModels();
            //    models_ComboBox.SelectedItem = amw.NewModel;
            //}
            //LoadingOverlay.Remove(this);
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
            var textBox = new TextBox();
            textBox.KeyUp += Malfunction_TextBox_KeyUp;
            malfunctions_StackPanel.Children.Add(textBox);
            textBox.Focus();
        }

        private void Malfunction_TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddMalfunction();
            }
        }

        private void AddKit()
        {
            var textBox = new TextBox();
            textBox.KeyUp += Kit_TextBox_KeyUp;
            kit_StackPanel.Children.Add(textBox);
            textBox.Focus();
        }

        private void Kit_TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddKit();
            }
        }

        private async Task<bool> PostTicketAsync()
        {
            var brand = await ApiClient.DeviceBrand.GetByName(brands_ComboBox.SelectedItem.ToString());
            var model = await ApiClient.DeviceModel.GetByName(models_ComboBox.SelectedItem.ToString());

            var ticket = await ApiClient.Ticket.Post(brand.Id, model.Id, clientName_TextBox.Text, clientPhoneNumber_TextBox.Text, ticketNote_TextBox.Text, null);
            if (ticket == null) return false;

            var kits = GetKit();
            foreach (var kit in kits)
            {
                var ticketKit = await ApiClient.TicketKit.Post(ticket.Id, kit);
                if (!ticketKit) return false;
            }

            var malfs = GetMalfunctions();
            foreach (var malf in malfs)
            {
                var malfres = await ApiClient.TicketMalfunction.Post(ticket.Id, malf);
                if (!malfres) return false;
            }

            var status = await ApiClient.Status.GetByName(status_ComboBox.SelectedItem.ToString());
            var statusres = await ApiClient.TicketStatus.Post(ticket.Id, status.Id);
            if (!statusres) return false;

            return true;
        }

        private List<string> GetKit()
        {
            List<string> kits = new List<string>();
            foreach (var obj in kit_StackPanel.Children)
            {
                if (obj is TextBox)
                {
                    if (!String.IsNullOrWhiteSpace(((TextBox)obj).Text))
                    { kits.Add(((TextBox)obj).Text); }
                }
            }
            return kits;
        }

        private List<string> GetMalfunctions()
        {
            List<string> malfs = new();
            foreach (var obj in malfunctions_StackPanel.Children)
            {
                if (obj is TextBox)
                {
                    if (!String.IsNullOrWhiteSpace(((TextBox)obj).Text))
                    { malfs.Add(((TextBox)obj).Text); }
                }
            }
            return malfs;
        }

        private void rejectNewBrand_Button_Click(object sender, RoutedEventArgs e)
        {
            addBrand_Grid.Visibility = Visibility.Collapsed;
            addBrand_Button.Visibility = Visibility.Visible;
        }

        private async void acceptNewBrand_Button_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(newBrand_TextBox.Text))
            {
                addBrand_Grid.Visibility = Visibility.Collapsed;
                addBrand_Button.Visibility = Visibility.Visible;
            }
            else
            {
                await AddNewBrand();
                await UpdateBrands();
                brands_ComboBox.SelectedItem = newBrand_TextBox.Text;
                newBrand_TextBox.Text = String.Empty;
                addBrand_Grid.Visibility = Visibility.Collapsed;
                addBrand_Button.Visibility = Visibility.Visible;
            }
        }

        private async Task AddNewBrand()
        {
            if (String.IsNullOrEmpty(newBrand_TextBox.Text))
            {
                MessageBox.Show(Rm.Get("enter_brand_name"), Rm.Get("error"), MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var brand = await ApiClient.DeviceBrand.GetByName(newBrand_TextBox.Text);
            if (brand != null)
            {
                MessageBox.Show(Rm.Get("brand_exists"), Rm.Get("error"), MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            LoadingOverlay.Show(this);
            var result = await ApiClient.DeviceBrand.Post(newBrand_TextBox.Text);
            if (result == true)
            {

            }
            else
            {
                MessageBox.Show(Rm.Get("smth_went_wrong"), Rm.Get("error"), MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            LoadingOverlay.Remove(this);
        }

        private void newBrand_TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                acceptNewBrand_Button_Click(null, null);
            }
        }

        private void rejectNewModel_Button_Click(object sender, RoutedEventArgs e)
        {
            addModel_Grid.Visibility = Visibility.Collapsed;
            addModel_Button.Visibility = Visibility.Visible;
        }

        private async void acceptNewModel_Button_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(newModel_TextBox.Text))
            {
                addModel_Grid.Visibility = Visibility.Collapsed;
                addModel_Button.Visibility = Visibility.Visible;
            }
            else
            {
                await AddNewModel();
                await UpdateModels();
                models_ComboBox.SelectedItem = newModel_TextBox.Text;
                newModel_TextBox.Text = String.Empty;
                addModel_Grid.Visibility = Visibility.Collapsed;
                addModel_Button.Visibility = Visibility.Visible;
            }
        }

        private void newModel_TextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                acceptNewModel_Button_Click(null, null);
            }
        }

        private async Task AddNewModel()
        {
            if (String.IsNullOrEmpty(newModel_TextBox.Text))
            {
                MessageBox.Show(Rm.Get("enter_model_name"), Rm.Get("error"), MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            LoadingOverlay.Show(this);
            var model = await ApiClient.DeviceModel.GetByName(newModel_TextBox.Text);
            if (model != null)
            {
                MessageBox.Show(Rm.Get("model_exists"), Rm.Get("error"), MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                var brand = await ApiClient.DeviceBrand.GetByName(brands_ComboBox.SelectedItem.ToString()!);
                var result = await ApiClient.DeviceModel.Post(brand.Id, newModel_TextBox.Text);
                if (result)
                {

                }
                else
                {
                    MessageBox.Show(Rm.Get("smth_went_wrong"), Rm.Get("error"), MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            LoadingOverlay.Remove(this);
        }

        private bool IsSomethingEmpty()
        {
            bool isComboBoxEmpty = brands_ComboBox.SelectedItem == null || models_ComboBox.SelectedItem == null;

            bool hasMalfunctions = malfunctions_StackPanel.Children
                .OfType<TextBox>()
                .Any(tb => !string.IsNullOrWhiteSpace(tb.Text));

            bool hasKits = kit_StackPanel.Children
                .OfType<TextBox>()
                .Any(tb => !string.IsNullOrWhiteSpace(tb.Text));

            return isComboBoxEmpty || !hasMalfunctions || !hasKits;
        }

        private void clientPhoneNumber_TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(clientPhoneNumber_TextBox.Text) && e.Text.Equals("8"))
            {
                e.Handled = true;
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width >= 1200 && main_Grid.ColumnDefinitions.Count < 2)
            {
                main_Grid.ColumnDefinitions.Clear();
                main_Grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                main_Grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                main_Grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                main_Grid.Children.Clear();
                whole_StackPanel.Children.Clear();

                main_Grid.Children.Add(first_StackPanel);

                var border = new Border
                {
                    BorderBrush = null,
                    BorderThickness = new Thickness(0),
                    Width = 2,
                    CornerRadius = new CornerRadius(3),
                    Background = new SolidColorBrush(Colors.DarkGray),
                    Margin = new Thickness(15, 0, 15, 0)
                };
                main_Grid.Children.Add(border);
                Grid.SetColumn(border, 1);

                main_Grid.Children.Add(second_StackPanel);
                Grid.SetColumn(second_StackPanel, 2);

            }
            else if (e.NewSize.Width < 1200 && main_Grid.ColumnDefinitions.Count > 2)
            {
                main_Grid.Children.Clear();
                main_Grid.ColumnDefinitions.Clear();
                //main_Grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                //var parent = VisualTreeHelper.GetParent(whole_StackPanel) as Panel;
                //if (parent != null)
                //{
                //    parent.Children.Remove(whole_StackPanel);
                //}
                whole_StackPanel.Children.Clear();
                whole_StackPanel.Children.Add(first_StackPanel);
                whole_StackPanel.Children.Add(second_StackPanel);


                main_Grid.Children.Add(whole_StackPanel);
                //Grid.SetColumn(whole_StackPanel, 0);
                //Grid.SetColumnSpan(whole_StackPanel, 1);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SettingsManager.SaveWindowProperties(this);
        }
    }
}
