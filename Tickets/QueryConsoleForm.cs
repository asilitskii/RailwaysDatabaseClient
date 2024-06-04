using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;
using System.Data.SQLite;

namespace Tickets
{
    public partial class QueryConsoleForm : Form
    {
        public QueryConsoleForm()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("Введите команду");
            }
            List<string> results = new List<string>();

            SQLiteCommand command = new SQLiteCommand(textBox1.Text, SQLClass.conn);
            DbDataReader reader = command.ExecuteReader();
            reader.Close();
            command.Dispose();
            MessageBox.Show("Выполнено");
        }
    }
}
