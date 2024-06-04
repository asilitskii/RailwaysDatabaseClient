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
    public partial class RegisterForm : UserControl
    {
        public RegisterForm()
        {
            InitializeComponent();
            if (MainForm.pages.Count > MainForm.pagePos + 1)
                MainForm.pages.RemoveRange(MainForm.pagePos + 1, MainForm.pages.Count - MainForm.pagePos - 1);
            MainForm.pages.Add(this);
            MainForm.pagePos++;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string registered = SQLClass.SelectScalar(
                "SELECT COUNT(*) FROM USERS WHERE LOGIN = '" + loginTB.Text + "'");

            if (registered != "0")
            {
                MessageBox.Show("Вы уже зарегистрированы!");
                return;
            }
            string dateOfBirth = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            try
            {
                SQLClass.Insert("INSERT INTO USERS(LOGIN, FIRST_NAME, LAST_NAME, DATE_OF_BIRTH, PASSWORD) VALUES(" +
                "'" + loginTB.Text + "', '" + textBox1.Text + "', '" + 
                fioTB.Text + "', '" + dateOfBirth + "', '" + passTB.Text + "')");
            } catch (Exception)
            {
                MessageBox.Show("Такой пользователь уже зарегистрирован!");
                return;
            }

            MessageBox.Show("Теперь можно входить в систему");

            //Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
