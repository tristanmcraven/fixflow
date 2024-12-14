using fixflow.Model;
using fixflow.Utility;
using fixflow.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace fixflow.UserControls
{
    /// <summary>
    /// Interaction logic for MalfunctionUserControl.xaml
    /// </summary>
    public partial class MalfunctionUserControl : UserControl
    {
        private TicketMalfunction? _malf;
        private Ticket _ticket;
        private bool _isEditing = false;
        private bool _creating;
        public MalfunctionUserControl(TicketMalfunction? malf, Ticket ticket, bool creating)
        {
            _malf = malf;
            _ticket = ticket;
            _creating = creating;
            DataContext = _malf;
            InitializeComponent();
            if (_creating) edit_Button_Click(null, null);
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!_isEditing) edit_Button.Visibility = Visibility.Visible;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            edit_Button.Visibility = Visibility.Collapsed;
        }

        private void malfName_TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                accept_Button_Click(null, null);
            }
        }

        private void edit_Button_Click(object sender, RoutedEventArgs e)
        {
            _isEditing = true;
            malfName_TextBlock.Visibility = Visibility.Collapsed;
            malfName_TextBox.Text = _malf == null ? "" : _malf.Name;
            malfName_TextBox.Visibility = Visibility.Visible;
            edit_Button.Visibility = Visibility.Collapsed;
            actionButtons_StackPanel.Visibility = Visibility.Visible;
            //malfName_TextBox.Focus();
            //malfName_TextBox.SelectionStart = malfName_TextBox.Text.Length;

            Dispatcher.BeginInvoke(new Action(() =>
            {
                malfName_TextBox.Focus();
                malfName_TextBox.SelectionStart = malfName_TextBox.Text.Length;
            }), System.Windows.Threading.DispatcherPriority.Input);
        }

        private async void accept_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_creating)
            {
                if (String.IsNullOrWhiteSpace(malfName_TextBox.Text))
                {
                    var tw = WindowManager.Get<TicketWindow>();
                    tw.malfunctions_ListBox.Items.RemoveAt(tw.malfunctions_ListBox.Items.Count - 1);
                    return;
                }
                else
                {
                    await ApiClient.TicketMalfunction.Post(_ticket.Guid, malfName_TextBox.Text);
                    WindowManager.Get<TicketWindow>().Window_Activated(null, null);
                    return;
                }
            }
            if (malfName_TextBox.Text.Equals(_malf.Name))
            {
                reject_Button_Click(null, null);
                return;
            }
            if (String.IsNullOrWhiteSpace(malfName_TextBox.Text))
            {
                if (await ApiClient.TicketMalfunction.Delete(_malf.Guid))
                {
                    WindowManager.Get<TicketWindow>().Window_Activated(null, null);
                }
                return;
            }
            if (await ApiClient.TicketMalfunction.Put(_malf.Guid, malfName_TextBox.Text))
            {
                reject_Button_Click(null, null);
                WindowManager.Get<TicketWindow>().Window_Activated(null, null);
            }
        }

        private void reject_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_creating)
            {
                var tw = WindowManager.Get<TicketWindow>();
                tw.malfunctions_ListBox.Items.RemoveAt(tw.malfunctions_ListBox.Items.Count - 1);
                return;
            }
            _isEditing = false;
            malfName_TextBlock.Visibility = Visibility.Visible;
            malfName_TextBox.Visibility = Visibility.Collapsed;
            edit_Button.Visibility = Visibility.Visible;
            actionButtons_StackPanel.Visibility = Visibility.Collapsed;
        }
    }
}
