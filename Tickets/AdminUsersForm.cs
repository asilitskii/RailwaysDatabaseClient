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
    public partial class AdminUsersForm : UserControl
    {
        public AdminUsersForm()
        {
            InitializeComponent();
            if (MainForm.pages.Count > MainForm.pagePos + 1)
                MainForm.pages.RemoveRange(MainForm.pagePos + 1, MainForm.pages.Count - MainForm.pagePos - 1);
            MainForm.pages.Add(this);
            MainForm.pagePos++;

            List<string> users = SQLClass.Select("SELECT Users.Login, Name, COUNT(Orders.CityFrom) FROM Users LEFT JOIN Orders ON Users.Login = Orders.Login GROUP BY Users.Login, Name");
            for (int i = 0; i < users.Count; i+=3)
            {
                string[] row = new string[3];
                row[0] = users[i];
                row[1] = users[i + 1];
                row[2] = users[i + 2];
                dataGridView1.Rows.Add(row);
            }
        
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
