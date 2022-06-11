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
    /// Interaction logic for ManagerNetworkLineShow.xaml
    /// </summary>

    public class ListItem
    {
        public double Xpos { get; set; }
        public double Ypos { get; set; }
        public string Icon { get; set; }
        public string Text { get; set; }
        public int Index { get; set; }
    }

    public partial class ManagerNetworkLineShow : UserControl
    {
        private string Icon = "../../Data/station_30x30.png";
        private List<Station> Stations = new List<Station>();
        protected bool m_IsDraging = false;
        protected Point m_DragStartPoint;

        private List<Station> draggedStations = new List<Station>();
        public ManagerNetworkLineShow()
        {
            Stations = Station.AllStations;
            InitializeComponent();
            foreach (Station s in Stations)
            {
                allStations.Items.Add(
                    new ListItem 
                    {   Icon = this.Icon, 
                        Text = s.Name, 
                        Xpos = s.Xpos, 
                        Ypos = s.Ypos,
                        Index = Stations.IndexOf(s)
                    });
            }
        }

        private void listview_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            m_DragStartPoint = e.GetPosition(null);
        }

        private void listview_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            var lv = sender as ListView;

            if (lv.SelectedItem == null) return;

            if (e.LeftButton == MouseButtonState.Pressed && !m_IsDraging)
            {
                Point position = e.GetPosition(null);

                if (Math.Abs(position.X - m_DragStartPoint.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(position.Y - m_DragStartPoint.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    m_IsDraging = true;
                    DataObject data = new DataObject(typeof(ListItem), lv.SelectedItem);
                    DragDropEffects de = DragDrop.DoDragDrop(lv, data, DragDropEffects.Copy);
                    m_IsDraging = false;
                }

            }
        }


        private void Canvas_PreviewDrop(object sender, DragEventArgs e)
        {
            var data = e.Data;
            //Point p = e.GetPosition(Cnv);

            if (data.GetDataPresent(typeof(ListItem)))
            {
                var info = data.GetData(typeof(ListItem)) as ListItem;

                Image image = new Image();
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(info.Icon, UriKind.Relative);
                bitmapImage.EndInit();
                image.Source = bitmapImage;

                Cnv.Children.Add(image);
                Canvas.SetLeft(image, info.Xpos);
                Canvas.SetTop(image, info.Ypos);


                draggedStations.Add(Stations[info.Index]);
                ConnectStations();
            
            }
        }
        private void ConnectStations()
        { 
            if (draggedStations.Count <= 1)
            {
                return;
            }
           
            Station latestDraggedStation = draggedStations.Last();
            foreach (Station s in draggedStations)
            {
                if (Timetable.AreStationsConnected(latestDraggedStation, s))
                {
                    Line l = new Line();
                    l.X1 = s.Xpos+15; // plus polovina sirine/duzine ikonice
                    l.Y1 = s.Ypos+15;
                    l.X2 = latestDraggedStation.Xpos+15;
                    l.Y2 = latestDraggedStation.Ypos+15;
                    l.StrokeThickness = 4;
                    l.Stroke = Brushes.Red;
                    Cnv.Children.Add(l);
                }
            }


            
        }
    }
}
