using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tickets
{
    public partial class BookedForRun : UserControl
    {
        public BookedForRun(string RunId, string runInfo)
        {
            InitializeComponent();
            if (MainForm.pages.Count > MainForm.pagePos + 1)
                MainForm.pages.RemoveRange(MainForm.pagePos + 1, MainForm.pages.Count - MainForm.pagePos - 1);
            MainForm.pages.Add(this);
            MainForm.pagePos++;

            label1.Text = "Пассажиры поезда " + runInfo;

            List<string> ordersData = SQLClass.Select(
                "SELECT Login, Place, CityFrom, CityTo FROM Orders WHERE RunId = " + RunId + " ORDER BY Place");

            int y = 0;
            for (int i = 0; i < ordersData.Count; i += 4)
            {
                string[] row = new string[4];
                row[0] = SQLClass.Select("SELECT Name FROM Users WHERE Login = '" + ordersData[i] + "'")[0];
                row[1] = ordersData[i + 1];
                row[2] = SQLClass.Select("SELECT Name FROM Cities WHERE Id = " + ordersData[i + 2])[0];
                row[3] = SQLClass.Select("SELECT Name FROM Cities WHERE Id = " + ordersData[i + 3])[0];

                dataGridView1.Rows.Add(row);

                y += 40;
            }
        }

        private void BookedForRun_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
