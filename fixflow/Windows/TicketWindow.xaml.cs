﻿using ABI.Windows.AI.MachineLearning;
using fixflow.Model;
using fixflow.UserControls;
using fixflow.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
using Windows.ApplicationModel.Appointments;

namespace fixflow.Windows
{
    /// <summary>
    /// Interaction logic for TicketWindow.xaml
    /// </summary>
    public partial class TicketWindow : Window
    {
        private uint _ticketId = 0;
        private Ticket _ticket;
        private TicketKit _ticketKit;
        private TicketStatus _ticketStatus;
        private TicketMalfunction _ticketMalfunction;
        private TicketRepair _ticketRepair;

        private bool _noteChanged = false;
        
        public TicketWindow(uint ticketId)
        {
            InitializeComponent();
            _ticketId = ticketId;
        }

        private void addStatus_Button_Click(object sender, RoutedEventArgs e)
        {
            var adw = new AddStatusWindow(_ticket)
            {
                Owner = this
            };
            adw.ShowDialog();
        }

        private void addRepair_Button_Click(object sender, RoutedEventArgs e)
        {
            var arw = new AddRepairWindow(_ticket)
            {
                Owner = this
            };
            arw.ShowDialog();
        }

        private void ticketNote_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _noteChanged = true;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private async Task GetTicketData()
        {
            _ticket = await ApiClient.Ticket.GetById(_ticketId);
            _ticket.DeviceBrand = await ApiClient.DeviceBrand.GetById(_ticket.DeviceBrandId);
            _ticket.DeviceModel = await ApiClient.DeviceModel.GetById(_ticket.DeviceModelId);
            _ticket.TicketKits = await ApiClient.Ticket.GetKits(_ticket.Id);
            _ticket.TicketRepairs = await ApiClient.Ticket.GetRepairs(_ticket.Id);
            _ticket.TicketMalfunctions = await ApiClient.Ticket.GetMalfunctions(_ticket.Id);
            _ticket.TicketStatuses = await ApiClient.Ticket.GetStatuses(_ticket.Id);
        }

        private async Task UpdateTicketData()
        {
            DataContext = _ticket;

            ticketNumber_TextBlock.Text = _ticket.Id.ToString();
            ticketCreationDate_TextBlock.Text = $"   ({_ticket.Timestamp.Day} {_ticket.Timestamp.ToString("MMM", new CultureInfo("ru-RU"))} {_ticket.Timestamp.Year}, {_ticket.Timestamp.ToString("dddd", new CultureInfo("ru-RU"))})";
  
             
            //clientName_TextBlock.Text = "ФИО: " + _ticket.ClientFullname;
            //clientPhone_TextBlock.Text = "Телефон: " + _ticket.ClientPhoneNumber;


            malfunctions_ListBox.Items.Clear();
            foreach (var item in _ticket.TicketMalfunctions)
            {
                malfunctions_ListBox.Items.Add(new MalfunctionUserControl(item));
            }

            parts_ListBox.Items.Clear();
            foreach (var item in _ticket.TicketKits)
            {
                parts_ListBox.Items.Add(new KitUserControl(item));
            }

            statuses_DataGrid.ItemsSource = null;
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Дата проставления", typeof(DateTime));
            dataTable.Columns.Add("Статус", typeof(string));
            foreach (var item in _ticket.TicketStatuses)
            {
                dataTable.Rows.Add(item.Timestamp, (await ApiClient.Status.GetById(item.StatusId)).Name);
            }
            statuses_DataGrid.ItemsSource = dataTable.DefaultView;

            repairs_DataGrid.ItemsSource = null;
            DataTable rDataTable = new DataTable();
            rDataTable.Columns.Add("Вид ремонта", typeof(string));
            rDataTable.Columns.Add("Цена", typeof(int));
            foreach (var item in _ticket.TicketRepairs)
            {
                rDataTable.Rows.Add(item.Repair, item.Price);
            }
            repairs_DataGrid.ItemsSource = rDataTable.DefaultView;

            if (_ticket.TicketRepairs.Count > 0)
            {
                int totalRepairsPrice = 0;
                foreach (var item in _ticket.TicketRepairs)
                {
                    totalRepairsPrice += (int)item.Price;
                }
                totalRepairsPrice_TextBlock.Text = "Сумма: " + totalRepairsPrice.ToString();
            }

            ticketNote_TextBox.Text = _ticket.Note;

            var brands = await ApiClient.DeviceBrand.Get();
            brands_ComboBox.Items.Clear();
            foreach (var brand in brands)
            {
                brands_ComboBox.Items.Add(brand.Name);
            }
            brands_ComboBox.SelectedItem = (await ApiClient.DeviceBrand.GetById(_ticket.DeviceBrandId)).Name;

            //var models = await ApiClient.DeviceModel.Get();
            //models_ComboBox.Items.Clear();
            //foreach (var model in await ApiClient.DeviceBrand.GetModelsByName(_ticket.DeviceBrand.Name))
            //{
            //    models_ComboBox.Items.Add(model.Name);
            //}
            //models_ComboBox.SelectedItem = (await ApiClient.DeviceModel.GetById(_ticket.DeviceModelId)).Name;

        }

        public async void Window_Activated(object sender, EventArgs e)
        {
            LoadingOverlay.Show(this);
            await GetTicketData();
            await UpdateTicketData();
            LoadingOverlay.Remove(this);
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_noteChanged)
            {
                await ApiClient.Ticket.Put(_ticket.Id, ticketNote_TextBox.Text);
            }
        }

        private void editClient_Button_Click(object sender, RoutedEventArgs e)
        {
            clientName_TextBlock.Visibility = Visibility.Collapsed;
            clientPhone_TextBlock.Visibility = Visibility.Collapsed;
            clientName_TextBox.Text = _ticket.ClientFullname;
            clientPhone_TextBox.Text = _ticket.ClientPhoneNumber;
            clientName_TextBox.Visibility = Visibility.Visible;
            clientPhone_TextBox.Visibility = Visibility.Visible;

            deviceBrand_TextBlock.Visibility = Visibility.Collapsed;
            deviceModel_TextBlock.Visibility = Visibility.Collapsed;
            brands_ComboBox.Visibility = Visibility.Visible;
            models_Grid.Visibility = Visibility.Visible;

            editClient_Button.Visibility = Visibility.Collapsed;
            actionButtons_StackPanel.Visibility = Visibility.Visible;

        }

        private async void accept_Button_Click(object sender, RoutedEventArgs e)
        {
            if (clientName_TextBox.Text.Equals(_ticket.ClientFullname) &&
                clientPhone_TextBox.Text.Equals(_ticket.ClientPhoneNumber) &&
                brands_ComboBox.SelectedItem.ToString().Equals(_ticket.DeviceBrand.Name) &&
                models_ComboBox.SelectedItem.ToString().Equals(_ticket.DeviceModel.Name))
            {
                reject_Button_Click(null, null);
                return;
            }
            if (models_ComboBox.SelectedItem == null)
            {
                reject_Button_Click(null, null);
                return;
            }
            var nameResult = await ApiClient.Ticket.ChangeClientName(_ticket.Id, clientName_TextBox.Text);
            var phoneResult = await ApiClient.Ticket.ChangeClientPhone(_ticket.Id, clientPhone_TextBox.Text);
            var deviceBrandResult = await ApiClient.Ticket.ChangeDeviceBrand(_ticket.Id, (await ApiClient.DeviceBrand.GetByName(brands_ComboBox.SelectedItem.ToString())).Id);
            var deviceModelResult = await ApiClient.Ticket.ChangeDeviceModel(_ticket.Id, (await ApiClient.DeviceModel.GetByName(models_ComboBox.SelectedItem.ToString())).Id);
            if (nameResult && phoneResult && deviceBrandResult && deviceModelResult)
            {
                reject_Button_Click(null, null);
                Window_Activated(null, null);
            }
        }

        private void reject_Button_Click(object sender, RoutedEventArgs e)
        {
            clientName_TextBlock.Visibility = Visibility.Visible;
            clientPhone_TextBlock.Visibility = Visibility.Visible;
            clientName_TextBox.Text = _ticket.ClientFullname;
            clientPhone_TextBox.Text = _ticket.ClientPhoneNumber;
            clientName_TextBox.Visibility = Visibility.Collapsed;
            clientPhone_TextBox.Visibility = Visibility.Collapsed;

            deviceBrand_TextBlock.Visibility = Visibility.Visible;
            deviceModel_TextBlock.Visibility = Visibility.Visible;
            brands_ComboBox.Visibility = Visibility.Collapsed;
            models_Grid.Visibility = Visibility.Collapsed;

            editClient_Button.Visibility = Visibility.Visible;
            actionButtons_StackPanel.Visibility = Visibility.Collapsed;
        }

        private void repairs_DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            // Create a new DataGridTemplateColumn to replace the auto-generated one
            var templateColumn = new DataGridTemplateColumn
            {
                Header = e.Column.Header, // Preserve the original header
                CellTemplate = CreateCellTemplate(e.PropertyName) // Create a TextBlock template for wrapping
            };

            // Replace the auto-generated column
            e.Column = templateColumn;
        }

        private DataTemplate CreateCellTemplate(string bindingPath)
        {
            // Dynamically create a DataTemplate for a TextBlock with text wrapping
            var textBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
            textBlockFactory.SetBinding(TextBlock.TextProperty, new Binding(bindingPath));
            textBlockFactory.SetValue(TextBlock.TextWrappingProperty, TextWrapping.Wrap);

            var dataTemplate = new DataTemplate
            {
                VisualTree = textBlockFactory
            };

            return dataTemplate;
        }

        private async void brands_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (brands_ComboBox.SelectedItem != null)
            {
                models_ComboBox.IsEnabled = true;
                models_ComboBox.Items.Clear();
                var models = await ApiClient.DeviceBrand.GetModelsByName(brands_ComboBox.SelectedItem.ToString());
                if (models == null)
                {
                    models_ComboBox.IsEnabled = false;
                    return;
                }
                foreach (var model in models)
                {
                    models_ComboBox.Items.Add(model.Name);
                }
                models_ComboBox.SelectedIndex = 0;
            }
        }

        private void addNewModel_Button_Click(object sender, RoutedEventArgs e)
        {
            models_ComboBox.Visibility = Visibility.Collapsed;
            newModel_TextBox.Visibility = Visibility.Visible;
            newModel_TextBox.Focus();
            addNewModel_Button.Visibility = Visibility.Collapsed;
            confirmNewModel_Button.Visibility = Visibility.Visible;
        }

        private async void confirmNewModel_Button_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(newModel_TextBox.Text))
            {
                HideNewDeviceModelControls();
                return;
            }
            var result = await ApiClient.DeviceModel.Post((await ApiClient.DeviceBrand.GetByName(brands_ComboBox.SelectedItem.ToString())).Id, newModel_TextBox.Text);
            if (result)
            {
                HideNewDeviceModelControls();
                brands_ComboBox_SelectionChanged(null, null);
            }
        }

        private void newModel_TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                confirmNewModel_Button_Click(null, null);
            }
        }

        private void HideNewDeviceModelControls()
        {
            models_ComboBox.Visibility = Visibility.Visible;
            newModel_TextBox.Visibility = Visibility.Collapsed;
            addNewModel_Button.Visibility = Visibility.Visible;
            confirmNewModel_Button.Visibility = Visibility.Collapsed;
        }
    }
}
