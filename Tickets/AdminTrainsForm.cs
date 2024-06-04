using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Tickets
{
    public partial class AdminTrainsForm : UserControl
    {
        public AdminTrainsForm()
        {
            InitializeComponent();
            Refresh(null, null);
            if (MainForm.pages.Count > MainForm.pagePos + 1)
                MainForm.pages.RemoveRange(MainForm.pagePos + 1, MainForm.pages.Count - MainForm.pagePos - 1);
            MainForm.pages.Add(this);
            MainForm.pagePos++;
        }

        private void AdminTrainsForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddTrain atf = new AddTrain();
            atf.Show();
        }

        private void Refresh(object sender, EventArgs e)
        {
            trainsDGV.Rows.Clear();
            List<string> cities = SQLClass.Select("SELECT STATION_NAME FROM STATIONS ORDER BY STATION_NAME");
            Column3.Items.Clear();
            Column4.Items.Clear();
            Column3.Items.AddRange(cities.ToArray());
            Column4.Items.AddRange(cities.ToArray());

            List<string> trains =
                SQLClass.Select(
                    "SELECT TRIP_ID, (SELECT ROUTE_NAME FROM ROUTES r WHERE ROUTE_ID = r.ROUTE_ID), " +
                    "CityFrom, CityTo, Days, Places FROM Trains ORDER BY Id");


            for (int i = 0; i < trains.Count; i += 6)
            {
                string[] row = new string[6];
                for (int j = i; j < i + 6; j++)
                    row[j - i] = trains[j];

                //Города
                row[2] = SQLClass.Select("SELECT Name FROM Cities WHERE Id = " + row[2])[0];
                row[3] = SQLClass.Select("SELECT Name FROM Cities WHERE Id = " + row[3])[0];

                trainsDGV.Rows.Add(row);
            }
        }

        private void trainsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                AddTrain at = new AddTrain(trainsDGV.Rows[e.RowIndex].Cells[0].Value.ToString());
                MainForm.mainPanel.Controls.Clear();
                MainForm.mainPanel.Controls.Add(at);
            }
        }
    }
}
