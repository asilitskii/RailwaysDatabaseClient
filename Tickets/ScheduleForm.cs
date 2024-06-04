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
    public partial class ScheduleForm : UserControl
    {
        public ScheduleForm()
        {
            InitializeComponent();
            timer1.Stop();
            if (MainForm.pages.Count > MainForm.pagePos + 1)
                MainForm.pages.RemoveRange(MainForm.pagePos + 1, MainForm.pages.Count - MainForm.pagePos - 1);
            MainForm.pages.Add(this);
            MainForm.pagePos++;

            List<string> routes = SQLClass.Select(
                "SELECT DISTINCT ROUTE_NAME " +
                "FROM ROUTES");
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(routes.ToArray());
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            List<string> schedule = SQLClass.Select(
                "SELECT STATION_NAME, ARRIVAL_TIME, DEPARTURE_TIME " +
                "FROM ROUTE_ELEMENTS re " +
                "JOIN STATIONS s ON s.STATION_ID = re.STATION_ID " +
                "JOIN ROUTES r ON r.ROUTE_ID = re.ROUTE_ID " +
                "WHERE r.ROUTE_NAME = '" + comboBox1.Text + "'"); 

            for (int i = 0; i < schedule.Count; i += 3)
            {
                string[] row = new string[3];
                row[0] = schedule[i];
                row[1] = schedule[i + 1];
                row[2] = schedule[i + 2];

                dataGridView1.Rows.Add(row);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddTrain at = new AddTrain("", timer1);
            MainForm.mainPanel.Controls.Clear();
            MainForm.mainPanel.Controls.Add(at);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            List<string> routes = SQLClass.Select(
                "SELECT DISTINCT ROUTE_NAME " +
                "FROM ROUTES");
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(routes.ToArray());
            timer1.Stop();
        }
    }
}
