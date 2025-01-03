using fixflow.Model;
using fixflow.UserControls;
using fixflow.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
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
        private bool _allowedToClose = false;
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
                UpdateTickets(await GetFormattedTickets(await GetAllTickets()));
                LoadingOverlay.Remove(this);
            }
        }

        private void UpdateTickets(List<Ticket> tickets)
        {
            tickets_StackPanel.Children.Clear();

            int numericId = tickets.Count;

            foreach (var ticket in tickets)
            {
                var status = ticket.TicketStatuses.OrderByDescending(x => x.Timestamp).First().Status;
                tickets_StackPanel.Children.Add(new TicketUserControl(numericId--, ticket, status));
            }

            if (tickets.Count > 0)
            {
                //tickets_DataGrid.Visibility = Visibility.Collapsed;
                tickets_StackPanel.Visibility = Visibility.Visible;
                noTickets_Grid.Visibility = Visibility.Collapsed;
            }
            //tickets_DataGrid.ItemsSource = null;

            //DataTable dataTable = new DataTable();
            //dataTable.Columns.Add("Номер", typeof(uint));
            //dataTable.Columns.Add("Дата принятия", typeof(string));
            //dataTable.Columns.Add("Марка", typeof(string));
            //dataTable.Columns.Add("Модель", typeof(string));
            //dataTable.Columns.Add("Имя клиента", typeof(string));
            //dataTable.Columns.Add("Номер клиента", typeof(string));
            //dataTable.Columns.Add("Guid", typeof(Guid));

            //int numericId = tickets.Count;

            //foreach (var ticket in tickets)
            //{
            //    var timestamp = ticket.Timestamp;

            //    dataTable.Rows.Add(
            //        numericId--, 
            //        $"{timestamp:dd}/{timestamp:MM}/{timestamp:yyyy}",
            //        ticket.DeviceBrand.Name,
            //        ticket.DeviceModel.Name,
            //        ticket.ClientFullname,
            //        $"+7{ticket.ClientPhoneNumber}"
            //        , ticket.Guid
            //    );
            //}

            //tickets_DataGrid.ItemsSource = dataTable.DefaultView;

            //if (tickets_DataGrid.Columns.Count > 0)
            //{
            //    var guidColumn = tickets_DataGrid.Columns
            //        .FirstOrDefault(c => c.Header.ToString() == "Guid");

            //    if (guidColumn != null)
            //    {
            //        guidColumn.Visibility = Visibility.Collapsed;
            //    }
            //}


            //else if (tickets.Count == 0) 
            //{
            //    tickets_DataGrid.Visibility = Visibility.Collapsed;
            //    noTickets_Grid.Visibility = Visibility.Visible;
            //}
        }


        private async Task<List<Ticket>> GetFormattedTickets(List<Ticket> tickets)
        {
            var formattedTickets = new List<Ticket>();

            foreach (var ticket in tickets)
            {
                ticket.DeviceBrand = await ApiClient.DeviceBrand.GetById(ticket.DeviceBrandGuid);
                ticket.DeviceModel = await ApiClient.DeviceModel.GetById(ticket.DeviceModelGuid);
                ticket.DeviceType = await ApiClient.DeviceType.GetById(ticket.DeviceTypeGuid);
                ticket.TicketStatuses = await ApiClient.Ticket.GetStatuses(ticket.Guid);
                foreach (var ticketStatus in ticket.TicketStatuses)
                {
                    ticketStatus.Status = await ApiClient.Status.GetById(ticketStatus.StatusGuid);
                }
                formattedTickets.Add(ticket);
            }

            return formattedTickets.OrderByDescending(t => t.Timestamp).ToList();
        }

        private async Task<List<Ticket>> GetAllTickets()
        {
            return await ApiClient.Ticket.Get();
        }

        private void tickets_DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (tickets_DataGrid.SelectedItem is DataRowView rowView)
            {
                if (e.ChangedButton == MouseButton.Left)
                {
                    Guid ticketId = (Guid)rowView["Guid"];
                    uint id = (uint)rowView["Номер"];
                    var tw = new TicketWindow(ticketId, (int)id)
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
            if (App.OfflineMode)
            {
                lower_Grid.Visibility = Visibility.Visible;
                offlineMode_StackPanel.Visibility = Visibility.Visible;
                var timestamp = App.Backup.Timestamp;
                lastSyncTime_TextBlock.Text = $"(последняя синхронизация: {timestamp:dd}.{timestamp:MM}.{timestamp:yyyy} {timestamp:HH}:{timestamp:mm})";
            }
            else
            {
                await ApiClient.Sync.SyncData(BackupManager.GetExistingBackup());
                if (App.HasInternetConnection && !App.OfflineMode) await BackupManager.CreateBackup();
            }
            Window_Activated(null, null);
            WindowManager.SetProperties(this);
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            if (App.OfflineMode)
            {
                BackupManager.SaveBackup(App.Backup);
            }
            else
            {
                Debug.WriteLine("Starting backup");
                var zxc = await BackupManager.CreateBackup();
                Debug.WriteLine("Backup Complete");
            }

            SettingsManager.SaveWindowProperties(this);

            e.Cancel = false;
        }

        private void tickets_DataGrid_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            contextMenu_Popup.IsOpen = false;
            contextMenu_Popup.IsOpen = true;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            contextMenu_Popup.IsOpen = false;
        }

        private async void deleteTickets_ListBoxItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            contextMenu_Popup.IsOpen = false;
            var selectedItems = tickets_DataGrid.SelectedItems;
            if (selectedItems.Count <= 0)
            {
                return;
            }

            var selectedGuids = selectedItems
                .Cast<DataRowView>()
                .Select(row => (Guid)row["Guid"])
                .ToList();

            if (selectedItems.Count == 1)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить выбранный тикет?",
                                    "Подтверждение",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    await ApiClient.Ticket.Delete(selectedGuids.First());
                }
            }
            else if (selectedItems.Count > 1)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить выбранные тикеты?",
                                    "Подтверждение",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    foreach (var guid in selectedGuids)
                    {
                        await ApiClient.Ticket.Delete(guid);
                    }
                }
            }
            contextMenu_Popup.IsOpen = false;
            Window_Activated(null, null);
        }

        private async void search_Button_Click(object sender, RoutedEventArgs e)
        {
            tickets_StackPanel.Children.Clear();
            if (advancedSearch_Grid.Visibility == Visibility.Collapsed && String.IsNullOrWhiteSpace(search_TextBox.Text))
            {
                UpdateTickets(await GetFormattedTickets(await GetAllTickets()));
                return;
            }
            if (advancedSearch_Grid.Visibility == Visibility.Collapsed)
            {
                UpdateTickets(await GetFormattedTickets(await ApiClient.Ticket.Search(search_TextBox.Text)));
            }
        }

        private void advancedSearch_Button_Click(object sender, RoutedEventArgs e)
        {
            if (App.OfflineMode)
            {
                MessageBox.Show("Недоступно в оффлайн-режиме", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var grid = advancedSearch_Grid;
            if (grid.Visibility == Visibility.Collapsed) grid.Visibility = Visibility.Visible;
            else grid.Visibility = Visibility.Collapsed;
        }
    }
}
