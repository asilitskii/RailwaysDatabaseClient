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
    public partial class AdminEmployeesForm : UserControl
    {
        List<string> departments;
        List<string> crews;
        List<string> jobs;
        public AdminEmployeesForm()
        {
            InitializeComponent();
            if (MainForm.pages.Count > MainForm.pagePos + 1)
                MainForm.pages.RemoveRange(MainForm.pagePos + 1, MainForm.pages.Count - MainForm.pagePos - 1);
            MainForm.pages.Add(this);
            MainForm.pagePos++;

            departments = SQLClass.Select("SELECT DEPARTMENT_NAME FROM DEPARTMENTS ORDER BY DEPARTMENT_NAME");
            comboBox1.Items.AddRange(departments.ToArray());
            crews = SQLClass.Select("SELECT CREW_ID FROM CREWS ORDER BY CREW_ID");
            comboBox2.Items.AddRange(crews.ToArray());
            jobs = SQLClass.Select("SELECT JOB_NAME FROM JOBS ORDER BY JOB_NAME");
            comboBox3.Items.AddRange(jobs.ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            int x = 10;
            int y = 10;
            List<string> results = SQLClass.Select(
                "SELECT FIRST_NAME, LAST_NAME, DATE_OF_BIRTH " +
                "FROM EMPLOYEES ");
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
                lbl.Text = "Имя:" +
                    Environment.NewLine +
                    results[i] + 
                    Environment.NewLine +
                    "Фамилия:" +
                    Environment.NewLine + 
                    results[i + 1] + 
                    Environment.NewLine +
                    "Дата рождения:" +
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            int x = 10;
            int y = 10;
            List<string> results = SQLClass.Select(
                "SELECT CREW_ID, CREW_SPECIALIZATION_NAME " +
                "FROM CREWS " +
                "JOIN CREW_SPECIALIZATIONS USING (CREW_SPECIALIZATION_ID)");
            if (results.Count == 0)
            {
                Label lbl = new Label();
                lbl.Location = new Point(200, 200);
                lbl.Size = new Size(300, 300);
                lbl.Text = "Нет данных";
                panel1.Controls.Add(lbl);
            }
            for (int i = 0; i < results.Count; i += 2)
            {
                Label lbl = new Label();
                lbl.Location = new Point(x, y);
                lbl.Size = new Size(150, 180);
                lbl.Text = "Номер бригады:" +
                    Environment.NewLine +
                    results[i] +
                    Environment.NewLine +
                    "Специализация:" +
                    Environment.NewLine +
                    results[i + 1] +
                    Environment.NewLine;
                panel1.Controls.Add(lbl);
                x += 190;
                if (x + 150 > panel1.Width)
                {
                    x = 10;
                    y += 160;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Необходимо выбрать фильтр");
                return;
            }
            panel1.Controls.Clear();
            int x = 10;
            int y = 10;
            List<string> results = SQLClass.Select(
                "SELECT FIRST_NAME, LAST_NAME, DEPARTMENT_NAME, JOB_NAME " +
                "FROM EMPLOYEES E " +
                "JOIN DEPARTMENTS USING(DEPARTMENT_ID) " +
                "JOIN JOBS j ON j.JOB_ID = e.JOB_ID " +
                "WHERE DEPARTMENT_NAME = '" + comboBox1.Text + "'");
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
                lbl.Text = "Имя:" +
                    Environment.NewLine +
                    results[i] +
                    Environment.NewLine +
                    "Фамилия:" +
                    Environment.NewLine +
                    results[i + 1] +
                    Environment.NewLine +
                    "Отдел:" +
                    Environment.NewLine +
                    results[i + 2] +
                    Environment.NewLine +
                    "Работа:" +
                    Environment.NewLine +
                    results[i + 3];
                panel1.Controls.Add(lbl);
                x += 190;
                if (x + 150 > panel1.Width)
                {
                    x = 10;
                    y += 180;
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(comboBox2.Text == "")
            {
                MessageBox.Show("Необходимо выбрать фильтр");
                return;
            }
            panel1.Controls.Clear();
            int x = 10;
            int y = 10;
            List<string> results = SQLClass.Select(
                "SELECT FIRST_NAME, LAST_NAME, CREW_ID, JOB_NAME " +
                "FROM EMPLOYEES " +
                "JOIN CREWS USING(CREW_ID) " +
                "JOIN JOBS USING(JOB_ID) " +
                "WHERE CREW_ID = '" + comboBox2.Text + "'");
            if (results.Count == 0)
            {
                Label lbl = new Label();
                lbl.Location = new Point(200, 200);
                lbl.Size = new Size(300, 300);
                lbl.Text = "Нет данных";
                panel1.Controls.Add(lbl);
            }
            for (int i = 0; i < results.Count; i += 5)
            {
                Label lbl = new Label();
                lbl.Location = new Point(x, y);
                lbl.Size = new Size(150, 180);
                lbl.Text = "Имя:" +
                    Environment.NewLine +
                    results[i] +
                    Environment.NewLine +
                    "Фамилия:" +
                    Environment.NewLine +
                    results[i + 1] +
                    Environment.NewLine +
                    "Бригада:" +
                    Environment.NewLine +
                    results[i + 2] +
                    Environment.NewLine +
                    "Работа:" +
                    Environment.NewLine +
                    results[i + 3];
                panel1.Controls.Add(lbl);
                x += 190;
                if (x + 150 > panel1.Width)
                {
                    x = 10;
                    y += 180;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox3.Text == "")
            {
                MessageBox.Show("Необходимо выбрать фильтр");
                return;
            }
            panel1.Controls.Clear();
            int x = 10;
            int y = 10;
            List<string> results = SQLClass.Select(
                "SELECT FIRST_NAME, LAST_NAME, JOB_NAME " +
                "FROM EMPLOYEES " +
                "JOIN JOBS USING(JOB_ID) " +
                "WHERE JOB_NAME = '" + comboBox3.Text + "'");
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
                lbl.Size = new Size(150, 140);
                lbl.Text = "Имя:" +
                    Environment.NewLine +
                    results[i] +
                    Environment.NewLine +
                    "Фамилия:" +
                    Environment.NewLine +
                    results[i + 1] +
                    Environment.NewLine +
                    "Работа:" +
                    Environment.NewLine +
                    results[i + 2];
                panel1.Controls.Add(lbl);
                x += 150;
                if (x + 150 > panel1.Width)
                {
                    x = 10;
                    y += 150;
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void AdminEmployeesForm_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" || comboBox2.Text == "" || comboBox3.Text == "")
            {
                MessageBox.Show("Необходимо выбрать все фильтры");
                return;
            }
            panel1.Controls.Clear();
            int x = 10;
            int y = 10;
            List<string> results = SQLClass.Select(
                "SELECT FIRST_NAME, LAST_NAME, JOB_NAME, CREW_ID, DEPARTMENT_NAME " +
                "FROM EMPLOYEES " +
                "JOIN JOBS USING(JOB_ID) " +
                "JOIN DEPARTMENTS USING(DEPARTMENT_ID) " +
                "JOIN CREWS USING(CREW_ID) " +
                "WHERE JOB_NAME = '" + comboBox3.Text + "' " +
                "AND CREW_ID = " + comboBox2.Text + " " +
                "AND DEPARTMENT_NAME = '" + comboBox1.Text + "'");
            if (results.Count == 0)
            {
                Label lbl = new Label();
                lbl.Location = new Point(200, 200);
                lbl.Size = new Size(300, 300);
                lbl.Text = "Нет данных";
                panel1.Controls.Add(lbl);
            }
            for (int i = 0; i < results.Count; i += 5)
            {
                Label lbl = new Label();
                lbl.Location = new Point(x, y);
                lbl.Size = new Size(150, 240);
                lbl.Text = "Имя:" +
                    Environment.NewLine +
                    results[i] +
                    Environment.NewLine +
                    "Фамилия:" +
                    Environment.NewLine +
                    results[i + 1] +
                    Environment.NewLine +
                    "Работа:" +
                    Environment.NewLine +
                    results[i + 2] +
                    Environment.NewLine +
                    "Бригада:" +
                    Environment.NewLine +
                    results[i + 3] +
                    Environment.NewLine +
                    "Отдел:" +
                    Environment.NewLine +
                    results[i + 4];
                panel1.Controls.Add(lbl);
                x += 150;
                if (x + 150 > panel1.Width)
                {
                    x = 10;
                    y += 250;
                }
            }
        }
    }
}
