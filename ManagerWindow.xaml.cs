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

        private Timetable Timetable;


        public ManagerWindow()
        {
            var model = new ModelStateInitializer();
            Timetable = model.Timetable;
            
            InitializeComponent();
            this.contentControl.Content = new ManagerWelcomeContent();
            this.innerGrid.Height = Constant.ScreenHeight-150;
            this.Schedule.Height = this.innerGrid.Height / 6;
            this.Trains.Height = this.innerGrid.Height / 6;
            this.Lines.Height = this.innerGrid.Height / 6; 
            this.Reports.Height = this.innerGrid.Height / 6;
            this.FontSize = 24;
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
    }
    public struct Constant
    {
        public static double ScreenWidth = System.Windows.SystemParameters.WorkArea.Width;
        public static double ScreenHeight = System.Windows.SystemParameters.WorkArea.Height;
    }
}
