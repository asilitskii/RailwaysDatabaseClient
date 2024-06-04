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
    public partial class OrderCarriageForm : UserControl
    {
        string TripId;
        string CityFrom;
        string CityTo;
        public OrderCarriageForm(string tripId, string cityFrom, string cityTo)
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
            List<string> carriages = SQLClass.Select(
                "SELECT carriage_id FROM CARRIAGE_BINDING " +
                "WHERE trip_id = " + TripId);
            for (int i = 0; i < carriages.Count; i++)
            {
                Button btn = new Button();
                btn.Location = new Point(x, y);
                btn.Size = new Size(50, 30);
                btn.Text = carriages[i];
                btn.Tag = carriages[i];
                /*string available = SQLClass.Select("SELECT SUM(1 - availability) FROM PLACES_AVAILABILITY" +
                    " WHERE trip_id = " + TripId + " AND carriage_place_id = " + carriages[i]).ToString();
                btn.Enabled = (available != "0");*/
                btn.Click += new EventHandler(ChoosePlace);

                Controls.Add(btn);

                x += 100;
                if (x + 100 >= Width)
                {
                    x = 50;
                    y += 50;
                }
            }
        }

        void ChoosePlace(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            OrderPlaceForm of = new OrderPlaceForm(TripId, btn.Tag.ToString(), CityFrom, CityTo);
            MainForm.mainPanel.Controls.Clear();
            MainForm.mainPanel.Controls.Add(of);
        }

        private void OrderCarriageForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}