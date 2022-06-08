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
    /// Interaction logic for ClientTicketView.xaml
    /// </summary>
    public partial class ClientTicketView : UserControl
    {

        private Client client;
        private List<Station> Stations = new List<Station>();
        private DataTable timeTableData = new DataTable();
        private Dictionary<RoadLine, List<DateTime>> timeTable = Timetable.Roads;

        public ClientTicketView(Client c)
        {
            client = c;
            Stations = Station.AllStations;

            InitializeComponent();
            SetupTable();
            DataContext = this;
        }
                

        private void SetupTable()
        {
            dataGrid.IsReadOnly = false;
            timeTableData.Rows.Clear();
            timeTableData.Columns.Clear();
            timeTableData.Columns.Add("Identifikator");
            timeTableData.Columns.Add("Status");
            timeTableData.Columns.Add("Datum kupovine");
            timeTableData.Columns.Add("Datum putovanja");
            timeTableData.Columns.Add("Polazna stanica");
            timeTableData.Columns.Add("Dolazna stanica");
            timeTableData.Columns.Add("Voz");
            List<Ticket> clientTickets = Ticket.GetAllClientTickets(client);
            foreach (Ticket t in clientTickets)
            {
                
                DataRow dr = timeTableData.NewRow();
                dr["Identifikator"] = t.Id;
                string status_ispis = "";
                if (t.Status == Status.RESERVED)
                    status_ispis = "Rezervisano";
                else if (t.Status == Status.BOUGHT)
                    status_ispis = "Kupljeno";
                dr["Status"] = status_ispis;
                dr["Datum kupovine"] = t.DateSold;
                dr["Datum putovanja"] = t.TravelDate;
                dr["Polazna stanica"] = t.Line.Origin;
                dr["Dolazna stanica"] = t.Line.Destination;
                dr["Voz"] = t.Line.Train.Name;
                timeTableData.Rows.Add(dr);
                                
            }

            dataGrid.ItemsSource = timeTableData.DefaultView;
            dataGrid.Items.Refresh();
            dataGrid.IsReadOnly = true;
        }

    }



}
