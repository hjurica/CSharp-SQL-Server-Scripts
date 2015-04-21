//css_include Config.cs
using System;
using System.Windows.Forms;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.IO;
using System.Collections;
using System.Collections.Generic;


class GetDatabases
{
    static public void Main(string[] args)
    {
        SqlConnection connection = new SqlConnection(Config.ConnectionString);
        SqlCommand cmd = new SqlCommand("", connection);

        try
        {
            connection.Open();

            Console.WriteLine("Connected to server " + Config.DbServer + ".");
            Console.WriteLine("==========================================");

            string[] file_databases = new string[]{};

            if(File.Exists("databases.txt"))
            {
                file_databases = File.ReadAllLines("databases.txt");
            }
    
            List<string> fileDatabases = new List<string>(file_databases);
            List<string> dbDatabases = new List<string>();

            TextWriter tw = new StreamWriter("databases.txt");

            cmd.CommandText = "SELECT name FROM master.dbo.sysdatabases WHERE name NOT IN ('master', 'model', 'msdb', 'tempdb')";

            using (IDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    if (fileDatabases.Contains(dr[0].ToString()) || fileDatabases.Contains("-" + dr[0].ToString()))
                    {
                        var item = fileDatabases.Find(i => i.Equals(dr[0].ToString()) || i.Equals("-" + dr[0].ToString()));
                        dbDatabases.Add(item);
                    }
                    else
                    {
                        dbDatabases.Add(dr[0].ToString());
                    }
                }
            }

            foreach (var db in dbDatabases)
            {
                tw.WriteLine(db);
                Console.WriteLine(db);
            }


            tw.Close();
            cmd.Dispose();
            connection.Close();
            connection.Dispose();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error!");
            Console.WriteLine(ex.ToString());
        }

        Console.WriteLine("==========================================");
		Console.ReadKey();
    }
}
