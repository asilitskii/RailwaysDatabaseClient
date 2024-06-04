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
    public partial class CarriageBindForm : Form
    {
        public CarriageBindForm()
        {
            InitializeComponent();
        }

        private void CarriageBindForm_Load(object sender, EventArgs e)
        {
            List<string> trips = SQLClass.Select("SELECT TRIP_ID FROM TRIPS");
            comboBox1.Items.AddRange(trips.ToArray());
            List<string> carriage = SQLClass.Select("SELECT CARRIAGE_ID FROM CARRIAGES");
            comboBox2.Items.AddRange(carriage.ToArray());
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string trip = comboBox1.Text;
            string carriage = comboBox2.Text;
            try
            {
                SQLClass.Insert(
                "INSERT INTO CARRIAGE_BINDING(TRIP_ID,CARRIAGE_ID) " +
                "VALUES ('" + trip + "', '" + carriage + "')");
            } catch (Exception)
            {
                MessageBox.Show("Данный вагон уже назначен на рейс");
                return;
            }
            MessageBox.Show("Вагон успешно назначен на рейс");
        }
    }
}
