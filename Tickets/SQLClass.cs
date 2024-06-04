using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.SQLite;

namespace Tickets
{
    public static class SQLClass
    {
        public static SQLiteConnection conn;
        public static List<string> Select(string Text)
        {
            List<string> results = new List<string>();

            SQLiteCommand command = new SQLiteCommand(Text, conn);
            DbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string currentDataType = reader.GetDataTypeName(i);
                    Console.WriteLine(currentDataType);
                    switch (currentDataType)
                    {
                        case "INTEGER":
                        case "INT":
                            {
                                results.Add(reader.GetInt64(i).ToString());
                            }
                            break;
                        case "VARCHAR(100)":
                        case "VARCHAR(50)":
                        case "VARCHAR(40)":
                        case "VARCHAR(30)":
                        case "VARCHAR(20)":
                        case "VARCHAR(5)":
                        case "VARCHAR(1)":
                        case "DATE":
                        case "TIME":
                            {
                                results.Add(reader.GetString(i));
                            }
                            break;
                        case "":
                            {
                                Console.WriteLine("NO COLUMN DATATYPE");
                            }
                            break;
                        default:
                            Console.WriteLine("UNKNOWN DATATYPE");
                            throw new Exception("UNKNOWN DATATYPE");
                    }
                }
            }

            reader.Close();
            command.Dispose();
            return results;
        }

        public static string SelectScalar(string Text)
        {
            string results;

            SQLiteCommand command = new SQLiteCommand(Text, conn);
            results = command.ExecuteScalar().ToString();
            command.Dispose();
            return results;
        }

        public static void Insert(string Text)
        {
            SQLiteCommand command = new SQLiteCommand(Text, conn);
            DbDataReader reader = command.ExecuteReader();
            reader.Close();
            command.Dispose();
        }
    }
}
