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
        private bool _let = false;
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
            if (_let)
            {
                LoadingOverlay.Show(this);
                UpdateTickets(await GetFormattedTickets());
                LoadingOverlay.Remove(this);
            }
        }

        private void UpdateTickets(List<Ticket> tickets)
        {
            tickets_DataGrid.ItemsSource = null;

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Номер", typeof(uint));
            dataTable.Columns.Add("Дата принятия", typeof(string));
            dataTable.Columns.Add("Марка", typeof(string));
            dataTable.Columns.Add("Модель", typeof(string));
            dataTable.Columns.Add("Имя клиента", typeof(string));
            dataTable.Columns.Add("Номер клиента", typeof(string));

            foreach (var ticket in tickets)
            {
                var timestamp = ticket.Timestamp;
                dataTable.Rows.Add(ticket.Guid,
                                   $"{timestamp.ToString("dd")}/{timestamp.ToString("MM")}/{timestamp.ToString("yyyy")}"
                                   //+ $"{timestamp.ToString("HH")}:{timestamp.ToString("mm")}"
                                   ,
                                   ticket.DeviceBrand.Name,
                                   ticket.DeviceModel.Name,
                                   ticket.ClientFullname,
                                   $"+7{ticket.ClientPhoneNumber}");
            }

            tickets_DataGrid.ItemsSource = dataTable.DefaultView;
        }

        private async Task<List<Ticket>> GetFormattedTickets()
        {
            var tickets = await ApiClient.Ticket.Get();
            var formattedTickets = new List<Ticket>();

            foreach (var ticket in tickets)
            {
                ticket.DeviceBrand = await ApiClient.DeviceBrand.GetById(ticket.DeviceBrandGuid);
                ticket.DeviceModel = await ApiClient.DeviceModel.GetById(ticket.DeviceModelGuid);
                formattedTickets.Add(ticket);
            }

            return formattedTickets.OrderByDescending(t => t.Guid).ToList();
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
            Process.Start("updater.exe");
            Application.Current.Shutdown();
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

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // pizda))))))
            LoadingOverlay.Show(this);
            await App.CheckConnection();
            _let = true;
            LoadingOverlay.Remove(this);
            Window_Activated(null, null);
            WindowManager.SetProperties(this);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SettingsManager.SaveWindowProperties(this);
        }
    }
}
