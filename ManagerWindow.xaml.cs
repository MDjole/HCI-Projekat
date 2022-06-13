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
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>

    public partial class ManagerWindow : Window
    {

        public ManagerWindow()
        {
            var model = new ModelStateInitializer();
            
            InitializeComponent();
            this.contentControl.Content = new ManagerWelcomeContent();
            this.innerGrid.Height = Constant.ScreenHeight-100;
            this.Schedule.Height = this.innerGrid.Height / 8;
            this.Trains.Height = this.innerGrid.Height / 8;
            this.Lines.Height = this.innerGrid.Height / 8;
            this.NetworkLines.Height = this.innerGrid.Height / 8;
            this.Reports.Height = this.innerGrid.Height / 8;
            this.logout.Height = this.innerGrid.Height / 8;
            this.FontSize = 24;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var windows = Application.Current.Windows;
            IInputElement focusedControl = FocusManager.GetFocusedElement(windows[3]);
            if (focusedControl is DependencyObject)
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                HelpProvider.ShowHelp(str, this);
            }
        }

        private void Schedule_Click(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new ManagerScheduleContent();
        }

        private void Lines_Click(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new ManagerLinesContent();
        }

        private void Trains_Click(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new ManagerTrainsContent();
        }

        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new ManagerReportsContent();
        }

        private void NetworkLines_Click(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new ManagerNetworkLineShow();
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
    public struct Constant
    {
        public static double ScreenWidth = System.Windows.SystemParameters.WorkArea.Width;
        public static double ScreenHeight = System.Windows.SystemParameters.WorkArea.Height;
    }
}
