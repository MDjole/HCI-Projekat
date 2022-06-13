using SerbRailway.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Text.RegularExpressions;
using WPFCustomMessageBox;

namespace SerbRailway
{
    /// <summary>
    /// Interaction logic for ManagerScheduleContent.xaml
    /// </summary>
    public partial class ManagerScheduleContent : UserControl
    {

        private DataTable scheduleTable = new DataTable();
        private DataTable singleTable = new DataTable();
        private static List<Schedule> AllSchedules = Schedule.AllSchedules;
        private Schedule selected;
        private List<RoadLine> lines = Timetable.Roads.Keys.ToList();
        private List<int> lineNumbers = new List<int>();

        public ManagerScheduleContent()
        {
            InitializeComponent();
            foreach (RoadLine line in lines) { lineNumbers.Add(line.LineNumber); }
            DataContext = this;
            dataGrid.FontSize = 24;
            singleGrid.FontSize = 24;
            FillTable();
            DescriptionMessage.Text = 
                "Za dodavanje novog reda upišite\n" +
                "broj vozne linije u označeno polje\n" +
                "i pritisnite dugme Dodaj.\n" +
                "Za izmenu postojećeg reda kliknite\n" +
                "na željeni red i izmenite\n" +
                "vremena u donjoj tabeli.\n" +
                "Za brisanje reda kliknite na željeni\n" +
                "red i pritisnite dugme Obriši.\n";
        }


        private void FillTable()
        {

            scheduleTable.Clear();
            scheduleTable.Columns.Clear();

            scheduleTable.Columns.Add("Linija");
            scheduleTable.Columns.Add("Red vožnje");

            foreach (Schedule s in AllSchedules)
            {
                DataRow dr = scheduleTable.NewRow();
                dr["Linija"] = s.LineNumber;
                string red = "";
                foreach (string t in s.StartTimes) red += t + " ";
                dr["Red vožnje"] = red;
                scheduleTable.Rows.Add(dr);
            }
            scheduleTable.Columns[0].ReadOnly = true;
            scheduleTable.Columns[1].ReadOnly = true;
            dataGrid.ItemsSource = scheduleTable.DefaultView;
            dataGrid.Items.Refresh();
        }

        private void FillSingleTable()
        {
            singleTable.Clear();
            singleTable.Columns.Clear();

            singleTable.Columns.Add("Polazak");
            foreach (string time in selected.StartTimes)
            {
                DataRow dr = singleTable.NewRow();
                dr["Polazak"] = time;
                singleTable.Rows.Add(dr);
            }
            singleGrid.ItemsSource = singleTable.DefaultView;
            singleGrid.Items.Refresh();
        }
        private void AttemptToDelete(int i, DataRowView drv)
        {
            if (i != -1 && i < AllSchedules.Count())
            {
                Schedule s = AllSchedules[i];
                if (lineNumbers.Contains(s.LineNumber))
                {
                    errormessage.Text = "Voz jos uvek radi na nekoj liniji.";
                    return;
                }
                Schedule.AllSchedules.Remove(s);
                scheduleTable.Rows.Remove(drv.Row);
                dataGrid.Items.Refresh();
                errormessage.Text = "";
                
            }

        }

        private void AddNewSchedule(int ln)
        {
            Schedule s = new Schedule();
            s.LineNumber = ln;
            s.StartTimes = new List<string>();
            AllSchedules.Add(s);
            DataRow dr = scheduleTable.NewRow();
            dr["Linija"] = s.LineNumber;
            dr["Red vožnje"] = "";
            scheduleTable.Rows.Add(dr);
            scheduleTable.Columns[0].ReadOnly = true;
            scheduleTable.Columns[1].ReadOnly = true;
            dataGrid.ItemsSource = scheduleTable.DefaultView;
            dataGrid.Items.Refresh();
            errormessage.Text = "";
        }


        private void AddTimes(int ln)
        {
            int i = scheduleTable.Rows.Count;
            List<string> newTimes = new List<string>();
            foreach (DataRow row in singleTable.Rows)
            {
                string input = (string)row["Polazak"];
                
                Regex rgx = new Regex(Schedule.format);
                if (!rgx.IsMatch(input))
                {
                    errormessage.Text = "Uneto vreme \"" + input + "\" nije pravilnog formata (hh:mm).";
                    return;
                }
                else
                {
                    newTimes.Add(input);
                }
            }

            foreach(Schedule s in AllSchedules)
            {
                if (s.LineNumber == ln)
                {
                    s.StartTimes = newTimes;
                }
            }

            string a = "Linija = " + ln.ToString();
            
            string red = "";
            foreach (string t in newTimes) red += t + " ";
            //DataRow dr = scheduleTable.Rows[dataGrid.SelectedIndex];
            //dr["Red vožnje"] = red;
            FillTable();
            dataGrid.ItemsSource = scheduleTable.DefaultView;
            dataGrid.Items.Refresh();
            errormessage.Text = "";

        }


        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var s = dataGrid.SelectedIndex;

            if (s != -1 && s < AllSchedules.Count())
            {
                selected = AllSchedules[s];
                lineBox.Text = selected.LineNumber.ToString();
                FillSingleTable();
            }
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            int ln;
            string str = lineBox.Text;
            if (Int32.TryParse(str, out ln))
            {
                if (lineNumbers.Contains(ln))
                {
                    errormessage.Text = "Uneta linija već postoji!";
                }
                else
                {
                    MessageBoxResult res = CustomMessageBox.ShowYesNo("Da li ste sigurni?",
                        "Da li sigurno želite da dodate ovaj red vožnje?", "Da", "Ne");
                    if (res == MessageBoxResult.Yes)
                        lineNumbers.Add(ln);
                    AddNewSchedule(ln);
                    lineBox.Text = "";
                }
            } else
            {
                errormessage.Text = "Linija mora biti broj!";
            }

        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            int i = dataGrid.SelectedIndex;
            DataRowView a = (DataRowView)dataGrid.SelectedItems[0];
            if (a != null)
            {
                MessageBoxResult res = CustomMessageBox.ShowYesNo("Da li ste sigurni?",
                            "Da li sigurno želite da obrišete ovaj red vožnje?", "Da", "Ne");
                if (res == MessageBoxResult.Yes)
                    AttemptToDelete(i, a);
            }

        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            int ln;
            string str = lineBox.Text;
            if (Int32.TryParse(str, out ln))
            {
                if (!lineNumbers.Contains(ln))
                {
                    errormessage.Text = "Uneta linija ne postoji!";
                }
                else {
                    MessageBoxResult res = CustomMessageBox.ShowYesNo("Da li ste sigurni?",
                        "Da li sigurno želite da izmenite ovaj red vožnje?", "Da", "Ne");
                    if (res == MessageBoxResult.Yes)
                        AddTimes(ln);
                    else return;
                }
            }
            else
            {
                errormessage.Text = "Linija mora biti broj!";
            }
            
        }
    }
}

