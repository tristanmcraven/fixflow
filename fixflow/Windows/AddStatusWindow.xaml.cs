using fixflow.Model;
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
    /// Interaction logic for AddStatusWindow.xaml
    /// </summary>
    public partial class AddStatusWindow : Window
    {
        private Ticket _ticket;
        public AddStatusWindow(Ticket ticket)
        {
            InitializeComponent();
            _ticket = ticket;
        }

        private async void addStatus_Button_Click(object sender, RoutedEventArgs e)
        {
            var status = await ApiClient.Status.GetByName(statuses_ComboBox.SelectedItem.ToString());
            var result = await ApiClient.TicketStatus.Post(_ticket.Guid, status.Guid);
            if (result)
            {
                this.Close();
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadingOverlay.Show(this);
            await UpdateStatuses();
            LoadingOverlay.Remove(this);
        }

        private async Task UpdateStatuses()
        {
            statuses_ComboBox.Items.Clear();
            var statuses = await ApiClient.Status.Get();
            foreach (var stat in statuses)
            {
                statuses_ComboBox.Items.Add(stat.Name);
            }
            statuses_ComboBox.SelectedIndex = 0;
            statuses_ComboBox.IsDropDownOpen = true;
        }

    }
}
