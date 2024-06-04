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
    public partial class AdminLocomotivesForm : UserControl
    {
        public AdminLocomotivesForm()
        {
            InitializeComponent();
            if (MainForm.pages.Count > MainForm.pagePos + 1)
                MainForm.pages.RemoveRange(MainForm.pagePos + 1, MainForm.pages.Count - MainForm.pagePos - 1);
            MainForm.pages.Add(this);
            MainForm.pagePos++;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            int x = 10;
            int y = 10;
            List<string> results = SQLClass.Select(
                "SELECT CREW_ID, BASE_STATION_ID, DATE_OF_CONSTRUCTION " +
                "FROM LOCOMOTIVES ");
            if (results.Count == 0)
            {
                Label lbl = new Label();
                lbl.Location = new Point(200, 200);
                lbl.Size = new Size(300, 300);
                lbl.Text = "Нет данных";
                panel1.Controls.Add(lbl);
            }
            for (int i = 0; i < results.Count; i += 3)
            {
                Label lbl = new Label();
                lbl.Location = new Point(x, y);
                lbl.Size = new Size(150, 150);
                lbl.Text = "CREW: " +
                    Environment.NewLine +
                    results[i] +
                    Environment.NewLine +
                    "Станция " +
                    Environment.NewLine +
                    results[i + 1] +
                    Environment.NewLine +
                    "Дата создания:" +
                    Environment.NewLine +
                    results[i + 2];
                panel1.Controls.Add(lbl);
                x += 180;
                if (x + 150 > panel1.Width)
                {
                    x = 10;
                    y += 150;
                }
            }
        }
    

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            int x = 10;
            int y = 10;
            List<string> results = SQLClass.Select(
                "SELECT CARRIAGE_ID, CARRIAGE_TYPE_NAME, CAPACITY, BASE_PLACE_COST FROM " +
                "CARRIAGES JOIN CARRIAGE_TYPES using (carriage_type_id)");
            if (results.Count == 0)
            {
                Label lbl = new Label();
                lbl.Location = new Point(200, 200);
                lbl.Size = new Size(300, 300);
                lbl.Text = "Нет данных";
                panel1.Controls.Add(lbl);
            }
            for (int i = 0; i < results.Count; i += 4)
            {
                Label lbl = new Label();
                lbl.Location = new Point(x, y);
                lbl.Size = new Size(150, 180);
                lbl.Text = "Номер вагона: " +
                    Environment.NewLine +
                    results[i] +
                    Environment.NewLine +
                    "Тип вагона " +
                    Environment.NewLine +
                    results[i + 1] +
                    Environment.NewLine +
                    "Вместимость:" +
                    Environment.NewLine +
                    results[i + 2] +
                     Environment.NewLine +
                    "Базовая стоимость:" +
                    Environment.NewLine +
                    results[i + 3];
                panel1.Controls.Add(lbl);
                x += 180;
                if (x + 150 > panel1.Width)
                {
                    x = 10;
                    y += 190;
                }
            }

        }

        private void AdminLocomotivesForm_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            CarriageBindForm fr = new CarriageBindForm();
            fr.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            int x = 10;
            int y = 10;
            List<string> results = SQLClass.Select(
                "SELECT TRIP_ID, ROUTE_NAME, TRIP_DATE FROM " +
                "TRIPS " +
                "JOIN ROUTES using (ROUTE_ID)");
            if (results.Count == 0)
            {
                Label lbl = new Label();
                lbl.Location = new Point(200, 200);
                lbl.Size = new Size(300, 300);
                lbl.Text = "Нет данных";
                panel1.Controls.Add(lbl);
            }
            for (int i = 0; i < results.Count; i += 3)
            {
                Label lbl = new Label();
                lbl.Location = new Point(x, y);
                lbl.Size = new Size(150, 180);
                lbl.Text = "Номер вагона: " +
                    Environment.NewLine +
                    results[i] +
                    Environment.NewLine +
                    "Тип вагона " +
                    Environment.NewLine +
                    results[i + 1] +
                    Environment.NewLine +
                    "Вместимость:" +
                    Environment.NewLine +
                    results[i + 2];
                panel1.Controls.Add(lbl);
                x += 180;
                if (x + 150 > panel1.Width)
                {
                    x = 10;
                    y += 190;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            QueryConsoleForm qcf = new QueryConsoleForm();
            qcf.ShowDialog();
        }
    }
}
