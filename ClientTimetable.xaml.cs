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
    /// Interaction logic for ClientTimetable.xaml
    /// </summary>
    public partial class ClientTimetable : UserControl
    {
        private Client client;
        private List<Station> Stations = new List<Station>();
        private DataTable timeTableData = new DataTable();
        private Dictionary<RoadLine, List<DateTime>> timeTable = Timetable.Roads;
        public ClientTimetable(Client c)
        {
            client = c;
            Stations = Station.AllStations;

            InitializeComponent();
            DataContext = this;
            FromStation.ItemsSource = Stations;
            ToStation.ItemsSource = Stations;
        }

        private void FillTable(object sender, RoutedEventArgs args)
        {
            Station from = (Station) FromStation.SelectedItem;
            Station to = (Station) ToStation.SelectedItem;
            DateTime date = (DateTime) TravelDate.SelectedDate;
            if (date == null)
            {
                errormessage.Text = "Morate odabrati datum";
                return;
            }
            if (from != null && to != null)
            {
                if (from.Equals(to))
                {
                    errormessage.Text = "Stanice se ne smeju podudarati";
                    return;
                }
                SetupTable(from, to, date);
            } else
            {
                errormessage.Text = "Morate odabrati obe stanice";
                return;
            }
        }
        private void SetupTable(Station from, Station to, DateTime date)
        {
            dataGrid.IsReadOnly = false;
            timeTableData.Rows.Clear();
            timeTableData.Columns.Clear();
            timeTableData.Columns.Add("Linija");
            timeTableData.Columns.Add("Od");
            timeTableData.Columns.Add("Do");
            timeTableData.Columns.Add("Datum putovanja");
            timeTableData.Columns.Add("Voz");
            timeTableData.Columns.Add("Vreme polaska (h)");
            timeTableData.Columns.Add("Dužina putovanja (h)");
            timeTableData.Columns.Add("Vreme dolaska (h)");
            List<RoadLine> roadlines = Timetable.GetRoadLinesInDate(date);
            foreach (RoadLine rl in roadlines)
            {
                if (rl.Origin.Equals(from) && rl.Destination.Equals(to))
                {
                    DataRow dr = timeTableData.NewRow();
                    dr["Linija"] = rl.LineNumber;
                    dr["Od"] = from.Name;
                    dr["Do"] = to.Name;
                    dr["Datum putovanja"] = date;
                    dr["Voz"] = rl.Train.Name;
                    dr["Vreme polaska"] = rl.TravelStartHour;
                    dr["Vreme putovanja"] = rl.ETA;
                    dr["Vreme dolaska"] = rl.TravelStartHour + rl.ETA;
                    timeTableData.Rows.Add(dr);
                    
                }
            }
            
            dataGrid.ItemsSource = timeTableData.DefaultView;
            dataGrid.Items.Refresh();
            dataGrid.IsReadOnly = true;
        }

        private void ReserveTicket(object sender, RoutedEventArgs args)
        {
            DataRowView selectedRow = (DataRowView)dataGrid.SelectedItem;
            if (selectedRow == null)
            {
                errormessage2.Text = "Morate selektovati red u tabeli da biste kupili kartu.";
                return;
            }
            string[] rowData = (string[])selectedRow.Row.ItemArray;
            int lineNumber = Int32.Parse(rowData[0]);
            // CREATE TICKET
            // NOTE: DateBought not set!! ticket is reserved.
            Ticket newTicket = new Ticket
            {
                Owner = client,
                TravelDate = DateTime.Parse(rowData[3]),
                Status = Status.RESERVED,
                Line = Timetable.GetRoadlineByNumber(lineNumber)
            };

            Ticket.AllTickets.Add(newTicket);
            purchasesuccess.Text = "Karta uspešno rezervisana";
            errormessage2.Text = "";
            return;
        }

        private void BuyTicket(object sender, RoutedEventArgs args)
        {
            DataRowView selectedRow = (DataRowView)dataGrid.SelectedItem;
            if (selectedRow == null)
            {
                purchasesuccess.Text = "";
                errormessage2.Text = "Morate selektovati red u tabeli da biste kupili kartu.";
                return;
            }
            // EXTRACT TABLE ROW DATA
            object[] rowData = selectedRow.Row.ItemArray;
            int lineNumber = Int32.Parse((string)rowData[0]);
            // CREATE TICKET FROM TABLE ROW
            Ticket newTicket = new Ticket
            {
                DateSold = DateTime.Today,
                Owner = client,
                TravelDate = DateTime.Parse((string)rowData[3]),
                Status = Status.BOUGHT,
                Line = Timetable.GetRoadlineByNumber(lineNumber)
            };

            // ADD TO DB
            Ticket.AllTickets.Add(newTicket);

            purchasesuccess.Text = "Karta uspešno kupljena";

            // reset error msg
            errormessage2.Text = "";
            return;
        }
    }
}
