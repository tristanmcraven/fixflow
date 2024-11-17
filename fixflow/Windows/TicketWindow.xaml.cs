using ABI.Windows.AI.MachineLearning;
using fixflow.Model;
using fixflow.Utility;
using System;
using System.Collections.Generic;
using System.Data;
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
            _ticket.TicketKits = await ApiClient.Ticket.GetKits(_ticket.Id);
            _ticket.TicketRepairs = await ApiClient.Ticket.GetRepairs(_ticket.Id);
            _ticket.TicketMalfunctions = await ApiClient.Ticket.GetMalfunctions(_ticket.Id);
            _ticket.TicketStatuses = await ApiClient.Ticket.GetStatuses(_ticket.Id);
        }

        private async Task UpdateTicketData()
        {
            clientName_TextBlock.Text = "ФИО: " + _ticket.ClientFullname;
            clientPhone_TextBlock.Text = "Телефон: " + _ticket.ClientPhoneNumber;


            malfunctions_ListBox.Items.Clear();
            foreach (var item in _ticket.TicketMalfunctions)
            {
                malfunctions_ListBox.Items.Add(item.Name);
            }

            parts_ListBox.Items.Clear();
            foreach (var item in _ticket.TicketKits)
            {
                parts_ListBox.Items.Add(item.Name);
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

        }

        private async void Window_Activated(object sender, EventArgs e)
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
    }
}
