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
    /// Interaction logic for KitUserControl.xaml
    /// </summary>
    public partial class KitUserControl : UserControl
    {
        private TicketKit? _kit;
        private Ticket _ticket;
        private bool _isEditing;
        private bool _creating;
        public KitUserControl(TicketKit? kit, Ticket ticket, bool creating)
        {
            _kit = kit;
            _creating = creating;
            _ticket = ticket;
            DataContext = _kit;
            InitializeComponent();
            if (_creating) edit_Button_Click(null, null);
        }

        private void edit_Button_Click(object sender, RoutedEventArgs e)
        {
            _isEditing = true;
            kitName_TextBlock.Visibility = Visibility.Collapsed;
            kitName_TextBox.Text = _kit == null ? "" : _kit.Name;
            kitName_TextBox.Visibility = Visibility.Visible;
            edit_Button.Visibility = Visibility.Collapsed;
            actionButtons_StackPanel.Visibility = Visibility.Visible;
            kitName_TextBox.SelectionStart = kitName_TextBox.Text.Length;
            //kitName_TextBox.Focus();

            //если делать как сверху то хуй будет а не фокус
            Dispatcher.BeginInvoke(new Action(() =>
            {
                kitName_TextBox.Focus();
                kitName_TextBox.SelectionStart = kitName_TextBox.Text.Length;
            }), System.Windows.Threading.DispatcherPriority.Input);
        }

        private async void accept_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_creating)
            {
                if (String.IsNullOrWhiteSpace(kitName_TextBox.Text))
                {
                    var tw = WindowManager.Get<TicketWindow>();
                    tw.parts_ListBox.Items.RemoveAt(tw.parts_ListBox.Items.Count - 1);
                    return;
                }
                else
                {
                    await ApiClient.TicketKit.Post(_ticket.Id, kitName_TextBox.Text);
                    WindowManager.Get<TicketWindow>().Window_Activated(null, null);
                    return;
                }
            }
            if (kitName_TextBox.Text.Equals(_kit.Name))
                {reject_Button_Click(null, null);
                return;
            }
            if (String.IsNullOrWhiteSpace(kitName_TextBox.Text))
            {
                if (await ApiClient.TicketKit.Delete(_kit.Id))
                {
                    WindowManager.Get<TicketWindow>().Window_Activated(null, null);
                }
                return;
            }
            if (await ApiClient.TicketKit.Put(_kit.Id, kitName_TextBox.Text))
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
                tw.parts_ListBox.Items.RemoveAt(tw.parts_ListBox.Items.Count - 1);
                return;
            }
            _isEditing = false;
            kitName_TextBlock.Visibility = Visibility.Visible;
            kitName_TextBox.Visibility = Visibility.Collapsed;
            edit_Button.Visibility = Visibility.Visible;
            actionButtons_StackPanel.Visibility = Visibility.Collapsed;
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!_isEditing) edit_Button.Visibility = Visibility.Visible;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            edit_Button.Visibility = Visibility.Collapsed;
        }

        private void kitName_TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                accept_Button_Click(null, null);
            }
        }
    }
}
