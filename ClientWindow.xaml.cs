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
using SerbRailway.Model;

namespace SerbRailway
{
    /// <summary>
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        private Client client;
        public ClientWindow(Client c)
        {
            client = c;
            var model = new ModelStateInitializer();
            InitializeComponent();
            this.contentControl.Content = "\n\n\tDobro došli u Srbija Voz - FTN Edition!";
            this.contentControl.FontSize = 24;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            if (focusedControl is DependencyObject)
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                HelpProvider.ShowHelp(str, this);
            }
        }

        private void Timetable_SearchView(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new ClientTimetable(client);
        }

        private void Ticket_Overview(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new ClientTicketView(client);
        }

        private void Line_Overview(object sender, RoutedEventArgs ar)
        {
            contentControl.Content = new ClientRoadlineView();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
            Hide();
        }
    }
}
