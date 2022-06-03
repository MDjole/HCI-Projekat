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
        }
    }
}
