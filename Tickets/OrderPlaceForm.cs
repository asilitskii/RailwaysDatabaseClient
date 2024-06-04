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
    public partial class OrderPlaceForm : UserControl
    {
        string TripId;
        string CarriagePlaceId;
        string CityFrom;
        string CityTo;
        public OrderPlaceForm(string tripId, string carriageId, string cityFrom, string cityTo)
        {
            InitializeComponent();
            if (MainForm.pages.Count > MainForm.pagePos + 1)
                MainForm.pages.RemoveRange(MainForm.pagePos + 1, MainForm.pages.Count - MainForm.pagePos - 1);
            MainForm.pages.Add(this);
            MainForm.pagePos++;

            TripId = tripId;
            CityFrom = cityFrom;
            CityTo = cityTo;

            int x = 50;
            int y = 80;
            /*List<string> trainData = SQLClass.Select("SELECT carriage_id FROM CARRIAGE_BINDING" +
                " WHERE trip_id = " + TripId);*/
            List<string> places = SQLClass.Select("SELECT place_id FROM CARRIAGE_PLACES " +
                " WHERE carriage_id = " + carriageId);
            for (int i = 0; i < places.Count; i++)
            {
                Button btn = new Button();
                btn.Location = new Point(x, y);
                btn.Size = new Size(50, 30);
                btn.Text = places[i];
                CarriagePlaceId = SQLClass.Select(
                    "SELECT carriage_place_id " +
                    "FROM CARRIAGE_PLACES " +
                    "WHERE carriage_id = " + carriageId + " " +
                    "AND place_id = " + places[i])[0];
                string available = SQLClass.SelectScalar(
                    "SELECT SUM(1-availability) FROM PLACES_AVAILABILITY " +
                    "WHERE carriage_place_id = " + CarriagePlaceId);
                btn.Enabled = (available == "0");
                btn.Tag = CarriagePlaceId;
                btn.Click += new EventHandler(MakeOrder);

                Controls.Add(btn);

                x += 100;
                if (x + 100 >= Width)
                {
                    x = 50;
                    y += 50;
                }
            }
        }

        void MakeOrder(object sender, EventArgs e)
        {
            if (Program.Login == "")
            {
                MessageBox.Show("Вы не вошли в систему!");
                return;
            }
            string userId = SQLClass.Select("SELECT USER_ID FROM USERS WHERE LOGIN = '" + Program.Login + "'")[0];
            Button btn = (Button)sender;
            try
            {
                SQLClass.Insert("INSERT INTO TICKETS(TRIP_ID, USER_ID, CARRIAGE_PLACE_ID, " +
                                "START_STATION_ID, END_STATION_ID, BAGGAGE, SELL_TIME)" +
                                " VALUES(" + 
                                TripId + ", " + userId + ", " +
                                btn.Tag.ToString() + ", " + CityFrom + ", " + CityTo + ", " +
                                "TRUE, CURRENT_TIMESTAMP)");
            }
            catch (Exception)
            {
                MessageBox.Show("У вас уже куплен этот билет");
                return;
            }
            MessageBox.Show("Билет успешно куплен");
            btn.Enabled = false;
        }

        private void OrderPlaceForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
