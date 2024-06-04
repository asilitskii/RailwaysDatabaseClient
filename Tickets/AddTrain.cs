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
    public partial class AddTrain : UserControl
    {
        Timer Timer = null;
        string trainId;
        int comboY = 0;
        List<string> stations = new List<string>();
        public AddTrain(string TrainId = "", Timer timer = null)
        {
            trainId = TrainId;
            Timer = timer;
            InitializeComponent();
            if (MainForm.pages.Count > MainForm.pagePos + 1)
                MainForm.pages.RemoveRange(MainForm.pagePos + 1, MainForm.pages.Count - MainForm.pagePos - 1);
            MainForm.pages.Add(this);
            MainForm.pagePos++;

            comboY = comboBox1.Location.Y + 50;
            stations = SQLClass.Select("SELECT STATION_NAME FROM STATIONS ORDER BY STATION_NAME");
            comboBox1.Items.AddRange(stations.ToArray());
            comboBox1.Tag = "STATION";
            comboBox2.Items.AddRange(SQLClass.Select("SELECT ROUTE_TYPE_NAME FROM ROUTE_TYPES").ToArray());
            comboBox2.Tag = "TYPE";
            dateTimePicker1.Tag = "ARRIVAL_TIME";
            dateTimePicker2.Tag = "DEPARTURE_TIME";
            /*if (trainId != "")
            {
                Text = "Изменить маршрут поезда";
                button3_Click(button3, null);

                List<string> trainData = 
                    SQLClass.Select("SELECT Id, Name, CityFrom, CityTo, Days, Places FROM Trains WHERE Id = " + trainId);

                *//*if (trainData.Count > 0)
                {
                    nameTextBox.Text = trainData[1];
                    placeTextBox.Text = trainData[5];
                    string[] days = trainData[4].Split(new string[] { ", " }, StringSplitOptions.None);
                    for (int i = 0; i < days.Length; i++)
                    {
                        int j = Convert.ToInt32(days[i]);
                        checkedListBox1.SetItemChecked(j - 1, true);                            
                    }
                    fromComboBox.Text = SQLClass.Select("SELECT Name FROM Cities WHERE Id = " + trainData[2])[0];
                    toComboBox.Text = SQLClass.Select("SELECT Name FROM Cities WHERE Id = " + trainData[3])[0];
                }*//*

                List<string> citiesData =
                    SQLClass.Select("SELECT Cities.Name, TimeStart FROM Routes JOIN Cities ON Routes.City = Cities.Id WHERE TrainId = " + trainId);

                for (int i = 0; i < citiesData.Count; i += 2)
                {
                    ComboBox cb1 = new ComboBox();
                    cb1.Items.AddRange(cities.ToArray());
                    cb1.Text = citiesData[i];
                    cb1.Location = new Point(comboBox1.Location.X, comboY);
                    cb1.Size = new Size(255, 37);

                    Button btn = new Button();
                    btn.Location = new Point(button2.Location.X, comboY);
                    btn.Size = new Size(75, 34);
                    btn.Text = "+";
                    btn.Click += new EventHandler(button2_Click);

                    Button btn3 = new Button();
                    btn3.Location = new Point(button3.Location.X, comboY);
                    btn3.Size = new Size(75, 34);
                    btn3.Text = "-";
                    btn3.Click += new EventHandler(button3_Click);

                    DateTimePicker dp1 = new DateTimePicker();
                    dp1.Format = DateTimePickerFormat.Time;
                    dp1.Location = new Point(dateTimePicker1.Location.X, comboY);
                    dp1.Size = new Size(131, 34);

                    Controls.Add(cb1);
                    Controls.Add(btn);
                    Controls.Add(dp1);
                    Controls.Add(btn3);

                    comboY += 50;
                }
            }*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ComboBox cb1 = new ComboBox();
            cb1.Items.AddRange(stations.ToArray());
            cb1.Location = new Point(comboBox1.Location.X, comboY);
            cb1.Tag = "STATION";
            cb1.Size = new Size(255, 37);

            Button btn = new Button();
            btn.Location = new Point(button2.Location.X, comboY);
            btn.Size = new Size(75, 34);
            btn.Text = "+";
            btn.Click += new EventHandler(button2_Click);

            Button btn3 = new Button();
            btn3.Location = new Point(button3.Location.X, comboY);
            btn3.Size = new Size(75, 34);
            btn3.Text = "-";
            btn3.Click += new EventHandler(button3_Click);

            DateTimePicker dp1 = new DateTimePicker();
            dp1.Format = DateTimePickerFormat.Time;
            dp1.Location = new Point(dateTimePicker1.Location.X, comboY);
            dp1.Tag = "ARRIVAL_TIME";
            dp1.Size = new Size(131, 34);

            DateTimePicker dp2 = new DateTimePicker();
            dp2.Format = DateTimePickerFormat.Time;
            dp2.Location = new Point(dateTimePicker2.Location.X, comboY);
            dp2.Tag = "DEPARTURE_TIME";
            dp2.Size = new Size(131, 34);

            Controls.Add(cb1);
            Controls.Add(btn);
            Controls.Add(btn3);
            Controls.Add(dp1);
            Controls.Add(dp2);

            comboY += 50;
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            
        }

        private void AddTrain_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int y = btn.Location.Y;
            //Город
            foreach (Control ctrl in Controls)
            {
                if (Math.Abs(ctrl.Location.Y - y) < 3 && ctrl.Location.X > 200)
                {
                    ctrl.Parent = null;
                }
                else if (ctrl.Location.Y > y + 10 && ctrl.Location.X > 200)
                {
                    ctrl.Location = new Point(ctrl.Location.X, ctrl.Location.Y - 50);
                }
            }

            comboY -= 50;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void fromComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void placeTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" || comboBox2.Text == "" || nameTextBox.Text == "")
            {
                MessageBox.Show("Не все данные введены!");
                return;
            }
            string routeTypeId = SQLClass.Select(
                "SELECT ROUTE_TYPE_ID FROM ROUTE_TYPES " +
                "WHERE ROUTE_TYPE_NAME = '" + comboBox2.Text + "'")[0];
            try
            {
                SQLClass.Insert("INSERT INTO ROUTES(ROUTE_NAME, ROUTE_TYPE_ID) " +
                                "VALUES('" + nameTextBox.Text + "', " + routeTypeId + ")");
            }
            catch (Exception)
            {
                MessageBox.Show("Такой маршрут уже существует!");
                return;
            }

            
            foreach (Control ctrl in Controls)
            {
                if (ctrl is ComboBox)
                {
                    if(ctrl.Tag.ToString() == "STATION")
                    {
                        string station = ctrl.Text;
                        string arrivalTime = "", departureTime = "";
                        //Время, когда поезд там
                        foreach (Control ctrl2 in Controls)
                        {
                            if (ctrl2 is DateTimePicker && Math.Abs(ctrl2.Location.Y - ctrl.Location.Y) < 15)
                            {
                                string cityTime = ((DateTimePicker)ctrl2).Value.ToLongTimeString();
                                if (ctrl2.Tag.ToString() == "ARRIVAL_TIME")
                                {
                                    arrivalTime = ctrl2.Text.ToString();
                                }
                                else if (ctrl2.Tag.ToString() == "DEPARTURE_TIME")
                                {
                                    departureTime = ctrl2.Text.ToString();
                                }
                            }
                        }
                        string stationId = SQLClass.Select("SELECT STATION_ID FROM STATIONS WHERE STATION_NAME = '" + station + "'")[0];
                        if (arrivalTime != "" && departureTime != "")
                        {
                            string routeId = SQLClass.Select("SELECT ROUTE_ID FROM ROUTES WHERE ROUTE_NAME = '" + nameTextBox.Text + "'")[0];
                            SQLClass.Insert("INSERT INTO ROUTE_ELEMENTS(STATION_ID, ROUTE_ID, ARRIVAL_TIME, DEPARTURE_TIME)" +
                                        " SELECT " + stationId + ", " + routeId + ", '" + arrivalTime + "', '" + departureTime + "'");
                            
                        }
                    }
                }
            }
            MessageBox.Show("Успешно сохранено");
            if (Timer != null)
            {
                Timer.Start();
            }
        }
    }
}
