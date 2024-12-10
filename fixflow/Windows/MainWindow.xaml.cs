using fixflow.Model;
using fixflow.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void addTicket_Button_Click(object sender, RoutedEventArgs e)
        {
            var atw = new AddTicketWindow()
            {
                Owner = this,
            };
            atw.ShowDialog();
        }

        private async void Window_Activated(object sender, EventArgs e)
        {
            LoadingOverlay.Show(this);
            UpdateTickets(await GetFormattedTickets());
            LoadingOverlay.Remove(this);
        }

        private void UpdateTickets(List<Ticket> tickets)
        {
            tickets_DataGrid.ItemsSource = null;

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Номер", typeof(uint));
            dataTable.Columns.Add("Дата принятия", typeof(DateTime));
            dataTable.Columns.Add("Марка", typeof(string));
            dataTable.Columns.Add("Модель", typeof(string));
            dataTable.Columns.Add("Имя клиента", typeof(string));
            dataTable.Columns.Add("Номер клиента", typeof(string));

            foreach (var ticket in tickets)
            {
                dataTable.Rows.Add(ticket.Id,
                                   ticket.Timestamp,
                                   ticket.DeviceBrand.Name,
                                   ticket.DeviceModel.Name,
                                   ticket.ClientFullname,
                                   ticket.ClientPhoneNumber);
            }

            tickets_DataGrid.ItemsSource = dataTable.DefaultView;
        }

        private async Task<List<Ticket>> GetFormattedTickets()
        {
            var tickets = await ApiClient.Ticket.Get();
            var formattedTickets = new List<Ticket>();

            foreach (var ticket in tickets)
            {
                ticket.DeviceBrand = await ApiClient.DeviceBrand.GetById(ticket.DeviceBrandId);
                ticket.DeviceModel = await ApiClient.DeviceModel.GetById(ticket.DeviceModelId);
                formattedTickets.Add(ticket);
            }

            return formattedTickets;
        }

        private void tickets_DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (tickets_DataGrid.SelectedItem is DataRowView rowView)
            {
                if (e.ChangedButton == MouseButton.Left)
                {
                    uint ticketId = (uint)rowView["Номер"];
                    var tw = new TicketWindow(ticketId)
                    {
                        Owner = this
                    };
                    tw.ShowDialog();
                }
                
            }
        }

        private void settings_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            new SettingsWindow() { Owner = this }.ShowDialog();
        }

        private void updateApp_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void newVersion_TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = App.VisitUrl,
                UseShellExecute = true
            });
        }

        private void newVersion_TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void newVersion_TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = null;
        }
    }
}
