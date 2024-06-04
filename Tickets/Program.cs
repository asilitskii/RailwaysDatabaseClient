using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Tickets
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SQLClass.conn = new SQLiteConnection();
            SQLClass.conn.ConnectionString =
                @"Data Source=E:\Proga\Java\NSUBuffet\datasource.sqlite";
            SQLClass.conn.Open();
            Application.Run(new MainForm());

            SQLClass.conn.Close();
        }

        public static string Login = "";
    }
}

