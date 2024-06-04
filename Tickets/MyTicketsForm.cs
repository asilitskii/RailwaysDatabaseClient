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
    public partial class MyTicketsForm : UserControl
    {
        public MyTicketsForm()
        {
            InitializeComponent();
            if (MainForm.pages.Count > MainForm.pagePos + 1)
                MainForm.pages.RemoveRange(MainForm.pagePos + 1, MainForm.pages.Count - MainForm.pagePos - 1);
            MainForm.pages.Add(this);
            MainForm.pagePos++;

            string userId = SQLClass.Select("SELECT USER_ID FROM USERS WHERE LOGIN = '" + Program.Login + "'")[0];

            List<string> myTickets = SQLClass.Select(
                "SELECT t.TRIP_ID, cp.CARRIAGE_ID, cp.PLACE_ID, tr.TRIP_DATE, r.ROUTE_NAME " +
                "FROM TICKETS t " +
                "JOIN CARRIAGE_PLACES cp ON t.CARRIAGE_PLACE_ID = cp.CARRIAGE_PLACE_ID " +
                "JOIN TRIPS tr ON t.TRIP_ID = tr.TRIP_ID " +
                "JOIN ROUTES r ON r.ROUTE_ID = tr.ROUTE_ID " +
                "WHERE t.USER_ID = " + userId);
            int x = 10;
            int y = 10;
            for (int i = 0; i < 1; i += 1)
            {
                Label lbl = new Label();
                lbl.Text = "Рейс" + "           " + "Вагон" + "            " + "Место" + "                " + "Дата" + "                  " + "Маршрут";
                lbl.Location = new Point(x, y);
                lbl.Size = new Size(400, 20);
                lbl.Font = new Font("Arial", 11);

                Controls.Add(lbl);

                y += 10;

            }
            x = 10;
            y = 40;
            for (int i = 0; i < myTickets.Count; i += 5)
            {
                Label lbl = new Label();
                lbl.Text = myTickets[i] + "                  " + myTickets[i+1] + "                   " + myTickets[i+2] + "                   " + myTickets[i+3] + "                   " + myTickets[i+4];
                lbl.Location = new Point(x, y);
                lbl.Size = new Size(400, 160);
                lbl.Font = new Font("Arial", 11);

                Controls.Add(lbl);

                y += 10;
                
            }
            if(myTickets.Count == 0)
            {
                Label lbl = new Label();
                lbl.Location = new Point(200, 150);
                lbl.Size = new Size(400, 160);
                lbl.Text = "У вас нет купленных билетов";
                Controls.Add(lbl);
            }
        }
    }
}
