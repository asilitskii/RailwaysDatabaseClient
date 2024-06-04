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
    public partial class AdminBookingForm : UserControl
    {
        public AdminBookingForm()
        {
            InitializeComponent();
            if (MainForm.pages.Count > MainForm.pagePos + 1)
                MainForm.pages.RemoveRange(MainForm.pagePos + 1, MainForm.pages.Count - MainForm.pagePos - 1);
            MainForm.pages.Add(this);
            MainForm.pagePos++;
            button1_Click(null, null);
        }

        private void AdminBookingForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime dt1 = dateTimePicker1.Value;
            DateTime dt2 = dateTimePicker2.Value;

            List<string> cities = SQLClass.Select("SELECT Name FROM Cities ORDER BY Name");
            Column5.Items.Clear();
            Column4.Items.Clear();
            Column4.Items.AddRange(cities.ToArray());
            Column5.Items.AddRange(cities.ToArray());

            List<string> orders =
                SQLClass.Select(
                    "SELECT Id, Login, RunId, RunId, CityFrom, CityTo, Place, Status" +
                    " FROM Orders WHERE RunId IN " +
                        "(SELECT Id FROM Runs WHERE DT BETWEEN STR_TO_DATE('" + dt1.ToShortDateString() + "', '%d.%m.%Y')" +
                        " AND STR_TO_DATE('" + dt2.ToShortDateString() + "', '%d.%m.%Y')) ORDER BY Orders.Id");


            for (int i = 0; i < orders.Count; i += 8)
            {
                string[] row = new string[8];
                for (int j = i; j < i + 8; j++)
                    row[j - i] = orders[j];

                //Города
                List<string> runData = SQLClass.Select(
                    "SELECT Trains.Name, DT FROM Runs JOIN Trains ON Trains.Id = Runs.TrainId WHERE Runs.Id=" + row[2]);
                row[2] = runData[0];
                row[3] = runData[1];
                row[4] = SQLClass.Select("SELECT Name FROM Cities WHERE Id = " + row[4])[0];
                row[5] = SQLClass.Select("SELECT Name FROM Cities WHERE Id = " + row[5])[0];

                ordersDGV.Rows.Add(row);
            }
        }
    }
}
