using SerbRailway.Model;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace SerbRailway
{
    /// <summary>
    /// Interaction logic for ClientRoadlineView.xaml
    /// </summary>
    public partial class ClientRoadlineView : UserControl
    {
        private DataTable table = new DataTable();
        private List<RoadLine> Roadlines = Timetable.Roads.Keys.ToList();
        public ClientRoadlineView()
        {
            InitializeComponent();
            DataContext = this;
            FillTable();
        }

        private void FillTable()
        {
            table.Rows.Clear();
            table.Columns.Clear();

            table.Columns.Add("Linija");
            table.Columns.Add("Od");
            table.Columns.Add("Do");
            table.Columns.Add("Voz");
            table.Columns.Add("Aktivni dani");
            table.Columns.Add("Vreme polaska (h)");
            table.Columns.Add("Dužina putovanja (h)");
            table.Columns.Add("Vreme dolaska (h)");

            foreach (RoadLine rl in Roadlines)
            {
                DataRow dr = table.NewRow();
                dr["Linija"] = rl.LineNumber;
                dr["Od"] = rl.Origin.Name;
                dr["Do"] = rl.Destination.Name;
                dr["Voz"] = rl.Train.Name;
                dr["Aktivni dani"] = ActiveDaysToString(rl.TravelDays);
                dr["Vreme polaska (h)"] = rl.TravelStartHour;
                dr["Dužina putovanja (h)"] = rl.ETA;
                dr["Vreme dolaska (h)"] = rl.TravelStartHour + rl.ETA;
                table.Rows.Add(dr);
            }

            dataGrid.ItemsSource = table.DefaultView;
            dataGrid.Items.Refresh();
            dataGrid.IsReadOnly = true;
        }

        private string ActiveDaysToString(List<int> TravelDays)
        {
            string retval = "";
            Dictionary<int, string> dayToStringMap = new Dictionary<int, string>();
            dayToStringMap.Add(0, "NED");
            dayToStringMap.Add(1, "PON");
            dayToStringMap.Add(2, "UTO");
            dayToStringMap.Add(3, "SRE");
            dayToStringMap.Add(4, "ČET");
            dayToStringMap.Add(5, "PET");
            dayToStringMap.Add(6, "SUB");

            foreach (int dayofweek in TravelDays)
            {
                retval += dayToStringMap[dayofweek] + " ";
            }
            retval.TrimEnd();
            return retval;
        }

    }
}
