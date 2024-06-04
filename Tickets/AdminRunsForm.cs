using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tickets
{
    public partial class AdminRunsForm : UserControl
    {
        public AdminRunsForm()
        {
            InitializeComponent();
            if (MainForm.pages.Count > MainForm.pagePos + 1)
                MainForm.pages.RemoveRange(MainForm.pagePos + 1, MainForm.pages.Count - MainForm.pagePos - 1);
            MainForm.pages.Add(this);
            MainForm.pagePos++;
        }

        private void AdminRunsForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*if (MessageBox.Show("Удалить предыдущие рейсы за эти даты и заменить новыми?", "Предупреждение", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            DateTime dt1 = dateTimePicker1.Value;
            DateTime dt2 = dateTimePicker2.Value;
            List<string> trains = SQLClass.Select("SELECT Id, Days FROM Trains");

            SQLClass.Insert("DELETE FROM Orders WHERE RunId IN (SELECT Id FROM Runs WHERE DT BETWEEN STR_TO_DATE('" + dt1.ToShortDateString() + "', '%d.%m.%Y')" +
                " AND STR_TO_DATE('" + dt2.ToShortDateString() + "', '%d.%m.%Y'))");

            SQLClass.Insert("DELETE FROM Runs WHERE DT BETWEEN STR_TO_DATE('" + dt1.ToShortDateString() + "', '%d.%m.%Y')" +
                " AND STR_TO_DATE('" + dt2.ToShortDateString() + "', '%d.%m.%Y')");

            while (dt1 < dt2)
            {
                int day = (int)dt1.DayOfWeek;
                if (day == 0) day = 7;//Воскресенье

                for (int i = 0; i < trains.Count; i += 2)
                {
                    //В этот день есть поезд
                    if (trains[i + 1].Contains(day.ToString()))
                    {
                        SQLClass.Insert("INSERT INTO Runs(TrainId, DT)" +
                            "VALUES (" + trains[i] + " , STR_TO_DATE('" + dt1.ToShortDateString() + "', '%d.%m.%Y'))");
                    }
                }

                dt1 = dt1.AddDays(1);
            }

            MessageBox.Show("Случилось");*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dt1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string dt2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            runsDGV.Rows.Clear();

            /*List<string> trains = SQLClass.Select(
                "SELECT Concat(Name, ' ('," +
                "(SELECT Name FROM Cities WHERE Id = Trains.CityFrom), ' - ', " +
                "(SELECT Name FROM Cities WHERE Id = Trains.CityTo), ')')" +
                "FROM Trains ORDER BY Name");
            Column2.Items.Clear();
            Column2.Items.AddRange(trains.ToArray());*/

            /*List<string> runs = SQLClass.Select(
                "SELECT Runs.Id, Trains.Places, Concat(Name, ' ('," +
                "(SELECT Name FROM Cities WHERE Id = Trains.CityFrom), ' - ', " +
                "(SELECT Name FROM Cities WHERE Id = Trains.CityTo), ')'), DT FROM Runs JOIN Trains ON Trains.Id = Runs.TrainId" +
                " WHERE DT BETWEEN STR_TO_DATE('" + dt1.ToShortDateString() + "', '%d.%m.%Y') AND STR_TO_DATE('" + dt2.ToShortDateString() + "', '%d.%m.%Y')" +
                " ORDER BY DT");*/

            List<string> trips = SQLClass.Select(
                "SELECT TRIP_ID, LOCOMOTIVE_ID, TRIP_DATE, r.ROUTE_NAME " +
                "FROM TRIPS t " +
                "JOIN ROUTES r ON t.ROUTE_ID = r.ROUTE_ID " +
                "WHERE TRIP_DATE BETWEEN '" + dt1 + "' AND '" + dt2 + "'"); ;

            for (int  i = 0; i < trips.Count; i += 4)
            {
                string[] row = new string[4];
                row[0] = trips[i];
                row[1] = trips[i + 1];
                row[2] = trips[i + 2];
                row[3] = trips[i + 3]; 
                /*String total = trips[i + 1];
                String booked = SQLClass.Select("SELECT COUNT(*) FROM Orders WHERE RunId = " + trips[i])[0];

                row[3] = booked + " / " + total;*/

                runsDGV.Rows.Add(row);
            }
        }

        private void runsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string runId = runsDGV.Rows[e.RowIndex].Cells[0].Value.ToString();
            string runInfo = runsDGV.Rows[e.RowIndex].Cells[1].Value.ToString();

            BookedForRun atf = new BookedForRun(runId, runInfo);
            atf.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
