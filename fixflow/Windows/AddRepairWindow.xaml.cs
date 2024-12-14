using fixflow.Model;
using fixflow.Utility;
using Microsoft.VisualBasic;
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
    /// Interaction logic for AddRepairWindow.xaml
    /// </summary>
    public partial class AddRepairWindow : Window
    {
        private Ticket _ticket;
        private List<string> _repairs;
        public AddRepairWindow(Ticket ticket)
        {
            InitializeComponent();
            _ticket = ticket;
            repairs_Asb.Focus();
            GetRepairs();
        }

        private async void addRepair_Button_Click(object sender, RoutedEventArgs e)
        {
            bool result = false;
            var intParse = int.TryParse(price_TextBox.Text, out int p);
            if (!intParse)
            {
                MessageBox.Show(Rm.Get("wrong_price"), Rm.Get("error"), MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (String.IsNullOrWhiteSpace(repairs_Asb.Text))
            {
                MessageBox.Show("Введите или выберите выполненную работу", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var repair = await ApiClient.Repair.GetByName(repairs_Asb.Text);
            if (repair == null)
            {
                var repairResult = await ApiClient.Repair.Post(repairs_Asb.Text);
                result = await ApiClient.TicketRepair.Post(_ticket.Guid, repairResult.Guid, p);
            }
            else
            {
                result = await ApiClient.TicketRepair.Post(_ticket.Guid, repair.Guid, p);
            }
            if (result)
            {
                this.Close();
            }
        }

        private void price_TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                addRepair_Button_Click(null, null);
            }
        }

        private void price_TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Helper.IsNumeric(e.Text))
            {
                e.Handled = true;
            }
        }

        private void repairs_Asb_TextChanged(object sender, TextChangedEventArgs e)
        {
            repairs_Asb.Suggestions = _repairs.Where(r => r.Contains(repairs_Asb.Text, StringComparison.OrdinalIgnoreCase));
        }

        private async void GetRepairs()
        {
            _repairs = (await ApiClient.Repair.Get()).Select(x => x.Name).ToList();
        }

        private void repairs_Asb_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab && repairs_Asb.SelectionStart.Equals(repairs_Asb.Text.Length))
            {
                price_TextBox.Focus();
            }
        }
    }
}
