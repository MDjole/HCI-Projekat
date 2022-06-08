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
using SerbRailway.Model;

namespace SerbRailway
{
    /// <summary>
    /// Interaction logic for ManagerReportsContent.xaml
    /// </summary>
    public partial class ManagerReportsContent : UserControl
    {
        private readonly bool ComponentsInitialized = false;
        private DataTable dataTable = new DataTable();
        public ManagerReportsContent()
        {
            InitializeComponent();
            DataContext = this;
            ComponentsInitialized = true;
        }

        private void MonthlyReport()
        {
            // FILL TABLE WITH TICKETS SOLD THIS MONTH
            dataTable.Rows.Clear();
            dataTable.Columns.Clear();

            dataTable.Columns.Add("Redni broj");
            dataTable.Columns.Add("Linija");
            dataTable.Columns.Add("Voz");
            dataTable.Columns.Add("Od");
            dataTable.Columns.Add("Do");
            dataTable.Columns.Add("Ime i prezime kupca");
            dataTable.Columns.Add("Datum kupovine");
            dataTable.Columns.Add("Datum putovanja");

            int count = 0;
            foreach (Ticket t in Ticket.AllTickets)
            {
                int thismonth = DateTime.Now.Month;
                int thisyear = DateTime.Now.Year;
                if (t.DateSold.Month == thismonth 
                    && t.DateSold.Year == thisyear
                    && t.Status == Status.BOUGHT)
                {
                    count++;
                    DataRow dr = dataTable.NewRow();
                    dr["Redni broj"] = count;
                    dr["Linija"] = t.Line.LineNumber;
                    dr["Voz"] = t.Line.Train.Name;
                    dr["Od"] = t.Line.Origin.Name;
                    dr["Do"] = t.Line.Destination.Name;
                    dr["Ime i prezime kupca"] = t.Owner.Name + " " + t.Owner.Surname;
                    dr["Datum kupovine"] = t.DateSold;
                    dr["Datum putovanja"] = t.TravelDate;
                    dataTable.Rows.Add(dr);
                }
            }
            
            if (count == 0)
            {
                dataTable.Columns.Clear();
                dataTable.Columns.Add("NEMA PODATAKA ZA PRIKAZ");
                DataRow dr = dataTable.NewRow();
                dr["NEMA PODATAKA ZA PRIKAZ"] = "NEMA PODATAKA ZA PRIKAZ";
                dataTable.Rows.Add(dr);
            }


            dataGrid.ItemsSource = dataTable.DefaultView;
            dataGrid.Items.Refresh();
        }
        private void SelectedTravelReport()
        {
            // FILL TABLE WITH TICKETS SOLD ON SELECTED TRAVEL
            dataTable.Rows.Clear();
            dataTable.Columns.Clear();

            dataTable.Columns.Add("Redni broj");
            dataTable.Columns.Add("Linija");
            dataTable.Columns.Add("Voz");
            dataTable.Columns.Add("Od");
            dataTable.Columns.Add("Do");
            dataTable.Columns.Add("Ime i prezime kupca");
            dataTable.Columns.Add("Datum kupovine");

            int count = 0;
            foreach (Ticket t in Ticket.AllTickets)
            {
                DateTime date = DateTime.ParseExact(TravelDate.Text, "MM/dd/yyyy", null);
                if (t.Line.LineNumber == (int)RoadLineNumbers.SelectedItem
                    && t.Line.TravelDays.Contains((int)date.DayOfWeek)
                    && t.Status == Status.BOUGHT)
                {
                    count++;
                    DataRow dr = dataTable.NewRow();
                    dr["Redni broj"] = count;
                    dr["Linija"] = t.Line.LineNumber;
                    dr["Voz"] = t.Line.Train.Name;
                    dr["Od"] = t.Line.Origin.Name;
                    dr["Do"] = t.Line.Destination.Name;
                    dr["Ime i prezime kupca"] = t.Owner.Name + " " + t.Owner.Surname;
                    dr["Datum kupovine"] = t.DateSold;
                    dataTable.Rows.Add(dr);
                }    
            }
            if (count == 0)
            {
                dataTable.Columns.Clear();
                dataTable.Columns.Add("NEMA PODATAKA ZA PRIKAZ");
                DataRow dr = dataTable.NewRow();
                dr["NEMA PODATAKA ZA PRIKAZ"] = "NEMA PODATAKA ZA PRIKAZ";
                dataTable.Rows.Add(dr);
            }
            dataGrid.ItemsSource = dataTable.DefaultView;
            dataGrid.Items.Refresh();
        }
        private void ShowReport(object sender, RoutedEventArgs args)
        {
            if (Options.SelectedIndex == 0)
            {
                errormessage.Text = "";
                MonthlyReport();
            } else
            {
                if (!CheckInputParms())
                {
                    errormessage.Text = "Morate odabrati datum i liniju!";
                    return;
                }
                errormessage.Text = "";
                SelectedTravelReport();
            }
        }
        private bool CheckInputParms()
        {
            if (TravelDate.SelectedDate == null && 
                RoadLineNumbers.SelectedItem == null)
            {
                return false;
            }
            return true;
        }

        private void ShowAdditionalComponents(object sender, SelectionChangedEventArgs args)
        {
            ComboBox cb = (ComboBox)sender;
            if (ComponentsInitialized)
            {
                if (cb.SelectedIndex == 0)
                {
                    LineLbl.Visibility = Visibility.Collapsed;
                    RoadLineNumbers.Visibility = Visibility.Collapsed;
                    DateLbl.Visibility = Visibility.Collapsed;
                    TravelDate.Visibility = Visibility.Collapsed;
                }
                else
                {
                    LineLbl.Visibility = Visibility.Visible;
                    RoadLineNumbers.Visibility = Visibility.Visible;
                    DateLbl.Visibility = Visibility.Visible;
                    TravelDate.Visibility = Visibility.Visible;
                }
            }
            
        }
    }
}
