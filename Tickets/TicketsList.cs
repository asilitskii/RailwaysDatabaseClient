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
    public partial class TicketsList : UserControl
    {
        public TicketsList()
        {
            InitializeComponent();

            if (MainForm.pages.Count > MainForm.pagePos + 1)
                MainForm.pages.RemoveRange(MainForm.pagePos + 1, MainForm.pages.Count - MainForm.pagePos - 1);
            MainForm.pages.Add(this);
            MainForm.pagePos++;

            List<string> cities = SQLClass.Select(
                "SELECT STATION_NAME FROM STATIONS ORDER BY STATION_NAME");
            comboBox1.Items.AddRange(cities.ToArray());
            comboBox2.Items.AddRange(cities.ToArray());
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Выберите пункт отправления");
                return;
            }
            else if (comboBox2.Text == "")
            {
                MessageBox.Show("Выберите пункт назначения");
                return;
            }

            string dateFrom = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string dateTo = dateTimePicker2.Value.AddDays(1).ToString("yyyy-MM-dd");

            string CityFrom =
                SQLClass.Select("SELECT STATION_ID FROM STATIONS WHERE STATION_NAME = '" + comboBox1.Text + "'")[0];
            string CityTo =
                SQLClass.Select("SELECT STATION_ID FROM STATIONS WHERE STATION_NAME = '" + comboBox2.Text + "'")[0];

            List<string> trains = SQLClass.Select(
                "SELECT DISTINCT tr.trip_id, tr.trip_date, " +
                "re1.departure_time, re2.arrival_time " +
                "FROM trips tr " +
                "JOIN routes r ON tr.route_id = r.route_id " +
                "JOIN route_elements re1 ON r.route_id = re1.route_id " +
                "JOIN route_elements re2 ON r.route_id = re2.route_id " +
                "WHERE re1.station_id = " + CityFrom + "  AND re2.station_id = " + CityTo + " " +
                "AND re1.arrival_time < re2.arrival_time " +
                "AND tr.trip_date BETWEEN '" + dateFrom + "' AND '" + dateTo + "'"); ;

            Image img = Image.FromFile("../../Pictures/TrainBtn.png");
            int x = 10;
            int y = 10;
            TrainsPanel.Controls.Clear();
            for (int i = 0; i < trains.Count; i += 4)
            {
                Label lbl = new Label();
                lbl.Text =
                    Environment.NewLine +
                    Environment.NewLine +
                    Environment.NewLine +
                    "  " + "Поезд № " + trains[i] + Environment.NewLine +
                    "  " + "Отправление: " + Environment.NewLine +
                    "  " + trains[i + 2] + " " + trains[i + 1] + Environment.NewLine +
                    "  " + "Прибытие: " + Environment.NewLine +
                    "  " + trains[i + 3] + " " + trains[i + 1];
                lbl.Location = new Point(x, y);
                lbl.Size = new Size(200, 160);
                lbl.Font = new Font("Arial", 11);
                lbl.Tag = trains[i];
                lbl.Image = img;
                lbl.Click += new EventHandler(TrainClick);
                TrainsPanel.Controls.Add(lbl);

                x += 220;
                if (x + 200 > Width)
                {
                    x = 10;
                    y += 180;
                }
            }
            if (trains.Count == 0)
            {
                Label lbl = new Label();
                lbl.Location = new Point(200, 150);
                lbl.Size = new Size(400, 160);
                lbl.Text = "Не найдено рейсов по вашим критериям";
                TrainsPanel.Controls.Add(lbl);
            }
        }

        private void TrainClick(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            string CityFrom = SQLClass.Select("SELECT STATION_ID FROM STATIONS" +
                " WHERE STATION_NAME = '" + comboBox1.Text + "'")[0];
            string CityTo = SQLClass.Select("SELECT STATION_ID FROM STATIONS" +
                " WHERE STATION_NAME = '" + comboBox2.Text + "'")[0];

            OrderCarriageForm of = new OrderCarriageForm(lbl.Tag.ToString(), CityFrom, CityTo);
            MainForm.mainPanel.Controls.Clear();
            MainForm.mainPanel.Controls.Add(of);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Program.Login == "")
            {
                MessageBox.Show("Вы не вошли в систему!");
                return;
            }
            MyTicketsForm mtf = new MyTicketsForm();
            MainForm.mainPanel.Controls.Clear();
            MainForm.mainPanel.Controls.Add(mtf);
        }
    }
}

