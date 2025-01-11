using fixflow.Model;
using fixflow.Utility;
using fixflow.ViewModel;
using fixflow.Windows;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace fixflow.UserControls
{
    /// <summary>
    /// Interaction logic for TicketUserControl.xaml
    /// </summary>
    public partial class TicketUserControl : UserControl
    {
        //band-aid wcyd
        private int _numericId;
        private Ticket _ticket;
        private Status _status;

        private static Dictionary<string, Color> StatusColors = new()
        {
            { "Принят", Color.FromArgb(128, 221,228,235) },
            { "На согласовании", Color.FromArgb(128, 255,181,107) },
            { "Ожидание запчастей", Color.FromArgb(128, 255,224,138) },
            { "В ремонте", Color.FromArgb(128, 122,191,255) },
            { "Готово", Color.FromArgb(128, 136,212,152) },
            { "Выданные", Color.FromArgb(128, 163,168,207) },
            { "Без ремонта", Color.FromArgb(128, 229,136,143) },
        };
        public TicketUserControl(int numericId, Ticket ticket, Status ticketstatus)
        {
            InitializeComponent();
            this.DataContext = new TicketUserControlViewModel(numericId, ticket, ticketstatus);
            _ticket = ticket; 
            _status = ticketstatus;
            _numericId = numericId;
            SetTransparentBackground();
        }

        private Color GetStatusColor(string status)
        {
            return StatusColors.TryGetValue(status, out var color) ? color : Colors.Transparent;
        }

        private void SetTransparentBackground()
        {
            wrapper.Background = new SolidColorBrush(GetStatusColor((this.DataContext as TicketUserControlViewModel).Status.Name)); //there!!
        }

        private void SetBackground()
        {
            wrapper.Background = new SolidColorBrush(RemoveTransparency(GetStatusColor((this.DataContext as TicketUserControlViewModel).Status.Name)));
        }

        private Color RemoveTransparency(Color color)
        {
            return Color.FromRgb(color.R, color.G, color.B);
        }

        private void UserControl_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
            SetBackground();
        }

        private void UserControl_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Cursor = null;
            SetTransparentBackground();
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new TicketWindow(_ticket.Guid, _numericId)
            {
                Owner = WindowManager.Get<MainWindow>()
            }.ShowDialog();
        }

        private void UserControl_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mw = WindowManager.Get<MainWindow>();
            mw.SelectedTicket = _ticket;
            mw.contextMenu_Popup.IsOpen = false;
            mw.contextMenu_Popup.IsOpen = true;
        }
    }
}
