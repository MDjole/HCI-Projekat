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

namespace SerbRailway
{
    /// <summary>
    /// Interaction logic for ManagerTrainsContent.xaml
    /// </summary>
    public partial class ManagerTrainsContent : UserControl
    {

        private DataTable trainTable = new DataTable();
        private static List<Train> AllTrains = Train.AllTrains;

        public ManagerTrainsContent()
        {
            InitializeComponent();
            DataContext = this;
            dataGrid.FontSize = 24;
            FillTable();
            trainTable.RowChanged += OnRowChanged;
            DescriptionMessage.Text = "Ovo je pregled vozova.\n" +
                "Da biste izbrisali voz, obrisite njegovo ime i pritisnite enter.\n" +
                "Da biste promenili ime voza kliknite na njega i promenite mu ime.\n" +
                "Da biste dodali novi voz, unesite novo ime u prazan red na dnu.";
            
        }

        private void FillTable()
        {
            
            trainTable.Clear();
            trainTable.Columns.Clear();

            trainTable.Columns.Add("Vozovi");
            trainTable.Columns.Add("Id");

            foreach (Train t in Train.AllTrains)
            {
                DataRow dr = trainTable.NewRow();
                dr["Vozovi"] = t.Name;
                dr["Id"] = t.GetId();
                trainTable.Rows.Add(dr);
            }
            trainTable.Columns[1].ReadOnly = true;
            dataGrid.ItemsSource = trainTable.DefaultView;
            dataGrid.Items.Refresh();
        }

        private void AddTrain()
        {
            Train newTrain = new Train();
            // get last (newest) train
            var a = trainTable.Rows[trainTable.Rows.Count - 1];
            if (a == null || a["Vozovi"] == DBNull.Value)
            {
                try
                {
                    trainTable.Rows.Remove(a);
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
            var b = (string)a["Vozovi"];
            trainTable.Columns[1].ReadOnly = false;
            a["Id"] = newTrain.GetId();
            trainTable.Columns[1].ReadOnly = true;
            newTrain.Name = b;
            // validity check
            if (Train.TrainNameExists(newTrain))
            {
                errormessage.Text = "Voz sa unetim imenom već postoji!";
                trainTable.Rows.Remove(a);
                dataGrid.Items.Refresh();
                return;
            }
            // add to db, and reset the error message post every successful action
            Train.AllTrains.Add(newTrain);
            errormessage.Text = "";
        }
        private void AttemptToDelete(Train t, DataRow changedRow)
        {
            if (Timetable.IsTrainOperational(t))
            // Cannot delete train that still works somewhere
            // Remove them from all roads first
            {
                changedRow["Vozovi"] = t.Name;
                errormessage.Text = "Voz jos uvek radi na nekoj liniji.";
                return;
            }
            Train.AllTrains.Remove(t);
            trainTable.Rows.Remove(changedRow);
            dataGrid.Items.Refresh();
            errormessage.Text = "";
        }
        protected void OnRowChanged(object sender, DataRowChangeEventArgs args)
        {
            if (args.Action == DataRowAction.Add)
            {
                AddTrain();
            } else if (args.Action == DataRowAction.Change)
            {
                // Extract row data from event
                DataRow changedRow = args.Row;
                int train_id = Int32.Parse((string)changedRow["Id"]);
                foreach (Train t in Train.AllTrains)
                {
                    // find the train in DB
                    if (t.GetId() == train_id)
                    {
                        string newname = (string)changedRow["Vozovi"];
                        
                        if (newname == "") // ATTEMPT AT DELETION
                        {
                            AttemptToDelete(t, changedRow);
                        } else
                        {
                            // Update the name in DB
                            t.Name = newname;
                            errormessage.Text = "";
                        }
                        
                    }
                }
            }
                
        } 

    }
}
