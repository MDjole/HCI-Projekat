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
//using Windows.UI.Xaml.Controls;
using MaterialDesignThemes;
using System.Collections.ObjectModel;

namespace SerbRailway
{
    /// <summary>
    /// Interaction logic for ManagerLinesContent.xaml
    /// </summary>
    public partial class ManagerLinesContent : System.Windows.Controls.UserControl
    {

        private DataTable linesTable = new DataTable();
        private static Dictionary<RoadLine, List<DateTime>> Roads = Timetable.Roads;
        public ObservableCollection<string> StationNames { get; set; }

        public ObservableCollection<string> TrainNames { get; set; }

        public int selectedRow = -1;
        private RoadLine selectedRoadLine = null;

        public bool isInsertingNewLine;
        public bool isUpdatingLine;
        public bool isDeletingLine;


        public ObservableCollection<Day> Days1 { get; set; }

        public ObservableCollection<Day> Days2 { get; set; }

        Point startPoint = new Point();

        public ManagerLinesContent()
        {

            InitializeComponent();
            DataContext = this;
            dataGrid.FontSize = 18;
            FillTable();
            InicializeStationNames();
            InicializeTraingNames();
            //dataGrid.RowHeaderMouseClick += dataGrid_RowHeaderMouseClick;
            //dataGrid.ItemsSource = linesTable.DefaultView;
            InicializeDays();
            btn_Save.Visibility = Visibility.Hidden;
            btn_Cancel_Save.Visibility = Visibility.Hidden;

            btn_Save_Insert.Visibility = Visibility.Hidden;
            btn_Cancel_Save_Insert.Visibility = Visibility.Hidden; 

            isInsertingNewLine = false;
            isUpdatingLine = false;
            isDeletingLine = false;

            //lv_days1.Visibility = Visibility.Hidden;
            //lv_days2.Visibility = Visibility.Hidden;
            forma.Visibility = Visibility.Hidden;
        }

        private void InicializeDays()
        {
            //Day d1 = new Day("Ponedeljak", 1);
            //Day d2 = new Day("Utorak", 2);
            //Day d3 = new Day("Sreda", 3);
            //Day d4 = new Day("Četvrtak", 4);
            //Day d5 = new Day("Petak", 5);
            //Day d6 = new Day("Subota", 6);
            //Day d7 = new Day("Nedelja", 0);

            //List<Day> dayList1 = new List<Day>() { d1, d2, d3, d4, d5, d6, d7};

            Days1 = new ObservableCollection<Day>();

            //List<Day> dayList2 = new List<Day>() { d1};
            Days2 = new ObservableCollection<Day>();


        }

        private void InicializeStationNames()
        {
            List<string> lista = new List<string>();
            foreach (Station s in Station.AllStations)
                lista.Add(s.Name);
            StationNames = new ObservableCollection<string>(lista);

        }

        private void InicializeTraingNames()
        {
            List<string> lista = new List<string>();
            foreach (Train t in Train.AllTrains)
                lista.Add(t.Name);
            TrainNames = new ObservableCollection<string>(lista);
        }

        private void FillTable()
        {
            linesTable.Clear();
            linesTable.Columns.Clear();

            linesTable.Columns.Add("Broj linije");
            linesTable.Columns.Add("Polazna stanica");          // Origin.Name
            linesTable.Columns.Add("Dolazna stanica");          // Destination.Name
            linesTable.Columns.Add("Voz");                      // Train.Name
            linesTable.Columns.Add("Vreme polaska");            // TravelStartHour
            linesTable.Columns.Add("Predviđeno vreme dolaska"); // TravelStartHour + ETA
            linesTable.Columns.Add("Vreme putovanja");
            linesTable.Columns.Add("Dani putovanja");

            foreach (RoadLine rl in Roads.Keys)
            {
                DataRow dr = linesTable.NewRow();
                dr["Broj linije"] = rl.LineNumber;
                dr["Polazna stanica"] = rl.Origin.Name;
                dr["Dolazna stanica"] = rl.Destination.Name;
                dr["Voz"] = rl.Train.Name;
                dr["Vreme polaska"] = rl.TravelStartHour;
                dr["Predviđeno vreme dolaska"] = rl.TravelStartHour + rl.ETA;
                dr["Vreme putovanja"] = rl.ETA;

                string daniPutovanja = "";
                for (int i = 0; i < rl.TravelDays.Count; i++)
                {
                    int danP = rl.TravelDays[i];
                    if (1 == danP)
                        daniPutovanja += "PON ";
                    if (2 == danP)
                        daniPutovanja += "UTO ";
                    if (3 == danP)
                        daniPutovanja += "SRE ";
                    if (4 == danP)
                        daniPutovanja += "ČET ";
                    if (5 == danP)
                        daniPutovanja += "PET ";
                    if (6 == danP)
                        daniPutovanja += "SUB ";
                    if (0 == danP)
                        daniPutovanja += "NED ";
                }
                dr["Dani putovanja"] = daniPutovanja;


                linesTable.Rows.Add(dr);
            }
            linesTable.Columns[0].ReadOnly = true;
            linesTable.Columns[1].ReadOnly = true;
            linesTable.Columns[2].ReadOnly = true;
            linesTable.Columns[3].ReadOnly = true;
            linesTable.Columns[4].ReadOnly = true;
            linesTable.Columns[5].ReadOnly = true;
            linesTable.Columns[6].ReadOnly = true;
            linesTable.Columns[7].ReadOnly = true;
            dataGrid.ItemsSource = linesTable.DefaultView;
            dataGrid.Items.Refresh();
        }

        private void AddLine()
        {
            Train newTrain = new Train();
            // get last (newest) train
            var a = linesTable.Rows[linesTable.Rows.Count - 1];
            if (a == null)
            {
                try
                {
                    linesTable.Rows.Remove(a);
                    dataGrid.Items.Refresh();
                    return;
                }
                catch (Exception)
                {
                    dataGrid.Items.Refresh();
                    return;
                }

            }
            // set row data
            var b = (string)a[""];
            linesTable.Columns[1].ReadOnly = false;
            a["Id"] = newTrain.GetId();
            linesTable.Columns[1].ReadOnly = true;
            newTrain.Name = b;
            // validity check
            if (Train.TrainNameExists(newTrain))
            {
                errormessage.Text = "Voz sa unetim imenom već postoji!";
                linesTable.Rows.Remove(a);
                dataGrid.Items.Refresh();
                return;
            }
            // add to db, and reset the error message post every successful action
            Train.AllTrains.Add(newTrain);
            errormessage.Text = "";
        }

        private void ClearData()
        {
            txt_PolaznaStanica.Text = "";
            txt_DolaznaStanica.Text = "";
            txt_VozName.Text = "";
            tp_VremePolaska.Text = "";
            tp_VremeDolaska.Text = "";
            Days1.Clear();
            Days2.Clear();
        }

        private void btn_Insert_Click(object sender, EventArgs e)
        {
            isInsertingNewLine = true;
            if (!((isInsertingNewLine == true) && (isUpdatingLine == false && isDeletingLine == false)))
            {
                if (isUpdatingLine)
                {
                    System.Windows.MessageBox.Show("Molimo Vas da završite sa izmenom, pa da onda pređete na dodavanje nove linije!");
                    return;
                }
                    
                if (isDeletingLine)
                {
                    System.Windows.MessageBox.Show("Molimo Vas da završite sa brisanjem, pa da onda pređete na dodavanje nove linije!");
                    return;
                }
            }


            Day d1 = new Day("Ponedeljak", 1);
            Day d2 = new Day("Utorak", 2);
            Day d3 = new Day("Sreda", 3);
            Day d4 = new Day("Četvrtak", 4);
            Day d5 = new Day("Petak", 5);
            Day d6 = new Day("Subota", 6);
            Day d7 = new Day("Nedelja", 0);

            List<Day> dayList1 = new List<Day>() { d1, d2, d3, d4, d5, d6, d7 };

            Days1.Clear();
            Days1.Add(d1);
            Days1.Add(d2);
            Days1.Add(d3);
            Days1.Add(d4);
            Days1.Add(d5);
            Days1.Add(d6);
            Days1.Add(d7);
            
            Days2.Clear();

            forma.Visibility = Visibility.Visible;
            btn_Save_Insert.Visibility = Visibility.Visible;
            btn_Cancel_Save_Insert.Visibility = Visibility.Visible;

        }

        private void btn_Save_Insert_Click(object sender, EventArgs e)
        {
            string start_station = "";
            string end_station = "";
            string train_name = "";
            string time_of_departure = "";
            string time_of_arival = "";

            start_station = txt_PolaznaStanica.Text;
            end_station = txt_DolaznaStanica.Text;
            train_name = txt_VozName.Text;
            time_of_departure = tp_VremePolaska.Text;
            time_of_arival = tp_VremeDolaska.Text;

            if (start_station == "")
            {
                System.Windows.MessageBox.Show("Molimo Vas da unesete polaznu stanicu!");
                return;
            }
            if (end_station == "")
            {
                System.Windows.MessageBox.Show("Molimo Vas da unesete dolaznu stanicu!");
                return;
            }
            if (train_name == "")
            {
                System.Windows.MessageBox.Show("Molimo Vas da unesete Voz!");
                return;
            }
            if (time_of_departure == "")
            {
                System.Windows.MessageBox.Show("Molimo Vas da unesete Vreme polaska!");
                return;
            }
            if (time_of_arival == "")
            {
                System.Windows.MessageBox.Show("Molimo Vas da unesete Predviđeno vreme dolaska!");
                return;
            }


            int lineNumber = Timetable.getNewRoadLineID();
            
            Station origin = findStation(start_station);
            if (origin == null)
            {
                System.Windows.MessageBox.Show("Uneta polazna stanica ne postoji!");
                return;
            }
            
            Station destination = findStation(end_station);
            if (destination == null)
            {
                System.Windows.MessageBox.Show("Uneta dolazna stanica ne postoji!");
                return;
            }

            Train train = findTrain(train_name);
            if (train == null)
            {
                System.Windows.MessageBox.Show("Unet Voz ne postoji!");
                return;
            }

            List<int> l = new List<int>();
            foreach (Day d in Days2)
                l.Add(d.Number);
            l.Sort();
            if (l.Count == 0)
            {
                System.Windows.MessageBox.Show("Morate izbrati barem jedan Dan putovanja!");
                return;
            }
            if (l[0] == 0)
            {
                l.Remove(0);
                l.Add(0);
            }

            List<int> travelDays = l;

            TimeSpan travelStartHour = TimeSpan.Parse(time_of_departure);

            TimeSpan travelEndHour = TimeSpan.Parse(time_of_arival);
            TimeSpan eTA = travelEndHour - travelStartHour;

            RoadLine rl = new RoadLine(lineNumber, origin, 
                destination, train, travelDays, travelStartHour, eTA);

            Timetable.AddRoadline(rl);

            FillTable();

            System.Windows.MessageBox.Show("Uspešno ste dodali novu liniju.");
            isInsertingNewLine = false;
            forma.Visibility = Visibility.Hidden;
            btn_Save_Insert.Visibility = Visibility.Hidden;
            btn_Cancel_Save_Insert.Visibility = Visibility.Hidden;
            ClearData();
        }

        private void btn_Cancel_Save_Insert_Click(object sender, EventArgs e)
        {
            ClearData();
            forma.Visibility = Visibility.Hidden;
            btn_Save_Insert.Visibility = Visibility.Hidden;
            btn_Cancel_Save_Insert.Visibility=Visibility.Hidden;
            isInsertingNewLine = false;
        }

        private Station findStation(string stationName)
        {
            Station st = null;
            foreach(Station s in Station.AllStations)
            {
                if (s.Name == stationName)
                    st = s;
            }
            return st;
        }

        private Train findTrain(string trainName)
        {
            Train tr = null;
            foreach(Train t in Train.AllTrains)
            {
                if (t.Name == trainName)
                    tr = t;
            }
            return tr;
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            isUpdatingLine = true;
            if (!((isUpdatingLine == true) && (isInsertingNewLine == false && isDeletingLine == false)))
            {
                if (isInsertingNewLine)
                {
                    System.Windows.MessageBox.Show("Molimo Vas da završite sa dodavanjem nove linije, pa da onda pređete na izmenu linije!");
                    return;
                }

                if (isDeletingLine)
                {
                    System.Windows.MessageBox.Show("Molimo Vas da završite sa brisanjem, pa da onda pređete na izmenu linije!");
                    return;
                }
            }
            Days1.Clear();
            Days2.Clear();
            selectedRow = dataGrid.SelectedIndex;
            if (selectedRow != -1)
            {
                DataRow r = linesTable.Rows[selectedRow];
                int line_id = Convert.ToInt32(r.ItemArray[0]);
                foreach (RoadLine rl in Timetable.Roads.Keys)
                {
                    // find the line in DB
                    if (rl.LineNumber == line_id)
                    {
                        selectedRoadLine = rl;   
                        break;
                    }
                }
                if (selectedRoadLine != null)
                {
                    // Prebacivanje selektovane linije u polja forme
                    txt_PolaznaStanica.Text = selectedRoadLine.Origin.Name;
                    txt_DolaznaStanica.Text = selectedRoadLine.Destination.Name;
                    txt_VozName.Text = selectedRoadLine.Train.Name;
                    tp_VremePolaska.Text = selectedRoadLine.TravelStartHour.ToString();
                    tp_VremeDolaska.Text = (selectedRoadLine.TravelStartHour + selectedRoadLine.ETA).ToString();

                    set_days1_and_days2(selectedRoadLine.TravelDays);


                    dataGrid.Items.Refresh();
                    //lv_days1.Visibility = Visibility.Visible;
                    //lv_days2.Visibility = Visibility.Visible;
                    forma.Visibility = Visibility.Visible;
                    btn_Save.Visibility = Visibility.Visible;
                    btn_Cancel_Save.Visibility = Visibility.Visible;
                }
                
            }
            else
            {
                System.Windows.MessageBox.Show("Molimo Vas da izaberete liniju u tabeli!");
                isUpdatingLine = false;
            }
        }

        private void set_days1_and_days2(List<int> days_list)
        {
            if (days_list.Contains(1))
                Days2.Add(new Day("Ponedeljak", 1));
            else
                Days1.Add(new Day("Ponedeljak", 1));

            if (days_list.Contains(2))
                Days2.Add(new Day("Utorak", 2));
            else
                Days1.Add(new Day("Utorak", 2));

            if (days_list.Contains(3))
                Days2.Add(new Day("Sreda", 3));
            else
                Days1.Add(new Day("Sreda", 3));

            if (days_list.Contains(4))
                Days2.Add(new Day("Četvrtak", 4));
            else
                Days1.Add(new Day("Četvrtak", 4));

            if (days_list.Contains(5))
                Days2.Add(new Day("Petak", 5));
            else
                Days1.Add(new Day("Petak", 5));

            if (days_list.Contains(6))
                Days2.Add(new Day("Subota", 6));
            else
                Days1.Add(new Day("Subota", 6));

            if (days_list.Contains(0))
                Days2.Add(new Day("Nedelja", 0));
            else
                Days1.Add(new Day("Nedelja", 0));

            

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            // provera da li je promenjena polazna stanica, ako jeste provera da li ona postoji u DB
            // provera da li je promenjena dolazna stanica, ako jeste provera da li ona postoji u DB
            // provera da li je promenjen voz, ako jeste 
            if (selectedRoadLine.Origin.Name != txt_PolaznaStanica.Text)
            {
                string station = txt_PolaznaStanica.Text;
                bool exists = false;
                Station st = null;
                foreach(Station s in Station.AllStations)
                {
                    if(s.Name == station)
                    {
                        exists = true;
                        st = s;
                    }
                        
                }
                if(exists)
                {
                    selectedRoadLine.Origin = st;
                }
                else
                {
                    System.Windows.MessageBox.Show("Uneta polazna stanica ne postoji!");
                }
            }
            if (selectedRoadLine.Destination.Name != txt_DolaznaStanica.Text)
            {
                string station = txt_DolaznaStanica.Text;
                bool exists = false;
                Station st = null;
                foreach (Station s in Station.AllStations)
                {
                    if (s.Name == station)
                    {
                        exists = true;
                        st = s;
                    }

                }
                if (exists)
                {
                    selectedRoadLine.Destination = st;
                }
                else
                {
                    System.Windows.MessageBox.Show("Uneta dolazna stanica ne postoji!");
                }
            }
            if (selectedRoadLine.Train.Name != txt_VozName.Text)
            {
                string train = txt_VozName.Text;
                bool exists = false;
                Train tt = null;
                foreach (Train t in Train.AllTrains)
                {
                    if (t.Name == train)
                    {
                        exists = true;
                        tt = t;
                    }

                }
                if (exists)
                {
                    selectedRoadLine.Train = tt;
                }
                else
                {
                    System.Windows.MessageBox.Show("Unet voz ne postoji!");
                }
            }
            if (selectedRoadLine.TravelStartHour.ToString() != tp_VremePolaska.Text)
            {
                if (TimeSpan.Parse(tp_VremePolaska.Text) >= TimeSpan.Parse(tp_VremeDolaska.Text))
                    System.Windows.MessageBox.Show("Vreme polaska ne može biti manje od vremena dolaska!");
                else
                {
                    selectedRoadLine.TravelStartHour = TimeSpan.Parse(tp_VremePolaska.Text);
                }
            }
            if ((selectedRoadLine.TravelStartHour + selectedRoadLine.ETA).ToString() != tp_VremeDolaska.Text)
            {
                // promenjeno vreme dolaska
                // promeniti ETA u DB
                TimeSpan timeSpan = TimeSpan.Parse(tp_VremeDolaska.Text);
                if (TimeSpan.Parse(tp_VremePolaska.Text) >= timeSpan)
                    System.Windows.MessageBox.Show("Vreme polaska ne može biti manje od vremena dolaska!");
                else
                {
                    selectedRoadLine.ETA = timeSpan - selectedRoadLine.TravelStartHour;
                }
            }
            if (Days2.Count != 0)
            {
                List<int> l = new List<int>();
                foreach (Day d in Days2)
                    l.Add(d.Number);
                l.Sort();
                if (l[0] == 0)
                {
                    l.Remove(0);
                    l.Add(0);
                }
                selectedRoadLine.TravelDays = l;
            }
            else
            {
                System.Windows.MessageBox.Show("Morate odabrati dane putovanja!");
                return;
            }

            // azurirati DB
            for (int index = 0; index < Timetable.Roads.Count; index++)
            {
                var item = Timetable.Roads.ElementAt(index);
                var itemKey = item.Key;
                var itemValue = item.Value;
                if (itemKey.LineNumber == selectedRoadLine.LineNumber)
                {
                    itemKey = selectedRoadLine;
                    break;
                }                    
            }
            // azurirati tabelu
            FillTable();

            System.Windows.MessageBox.Show("Uspešno ste ažurirali liniju.");

            // ocistiti polja forme
            ClearData();


            btn_Save.Visibility = Visibility.Hidden;
            btn_Cancel_Save.Visibility = Visibility.Hidden;


            forma.Visibility = Visibility.Hidden;
            isUpdatingLine = false;
        }

        private void btn_Cancel_Save_Click(object sender, EventArgs e)
        {
            ClearData();
            forma.Visibility = Visibility.Hidden;
            btn_Save.Visibility = Visibility.Hidden;
            btn_Cancel_Save.Visibility = Visibility.Hidden;
            isUpdatingLine=false;
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            isDeletingLine = true;
            if (!((isDeletingLine == true) && (isInsertingNewLine == false && isUpdatingLine == false)))
            {
                if (isInsertingNewLine)
                {
                    System.Windows.MessageBox.Show("Molimo Vas da završite sa dodavanjem, pa da onda pređete na brisanje!");
                    return;
                }

                if (isUpdatingLine)
                {
                    System.Windows.MessageBox.Show("Molimo Vas da završite sa izmenom, pa da onda pređete na brisanje!");
                    return;
                }
            }
            this.selectedRow = dataGrid.SelectedIndex;
            if (selectedRow != -1)
            {
                DataRow r = linesTable.Rows[selectedRow];
                int line_id = Convert.ToInt32(r.ItemArray[0]);
                foreach (RoadLine rl in Timetable.Roads.Keys)
                {
                    // find the line in DB
                    if (rl.LineNumber == line_id)
                    {
                        Timetable.Roads.Remove(rl);
                        linesTable.Rows.Remove(r);
                        dataGrid.Items.Refresh();
                        System.Windows.MessageBox.Show("Linija je uspešno obrisana!");
                        //DisplayData();
                        ClearData();
                        break;
                    }
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Molimo Vas da izaberete liniju u tabeli!");
            }
            isDeletingLine = false;
        }




        // Drag&Drop

        private void ListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void ListView_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                // Get the dragged ListViewItem
                ListView listView = sender as ListView;
                ListViewItem listViewItem =
                    FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

                // Find the data behind the ListViewItem
                Day day = (Day)listView.ItemContainerGenerator.
                    ItemFromContainer(listViewItem);

                // Initialize the drag & drop operation
                DataObject dragData = new DataObject("myFormat", day);
                DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
            }
        }

        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void ListView_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void ListView_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                Day day = e.Data.GetData("myFormat") as Day;
                Days1.Remove(day);
                Days2.Add(day);
            }
        }

        private void ListView_Drop1(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                Day day = e.Data.GetData("myFormat") as Day;
                Days2.Remove(day);
                Days1.Add(day);
            }
        }


    }
}
