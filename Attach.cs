//css_include Config.cs
using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.IO;


class Attach
{
    static public void Main(string[] args)
    {
        SqlConnection connection = new SqlConnection(Config.ConnectionString);
        SqlCommand cmd = new SqlCommand("", connection);

        string [] filePaths = Directory.GetFiles(Config.FolderName, "*.mdf");

        try
        {
            connection.Open();

            Console.WriteLine("Connected to server " + Config.DbServer + ".");
            Console.WriteLine("==========================================");
             
            foreach (var file in filePaths)
            {
                string name = Path.GetFileNameWithoutExtension(file).Replace(".", "_");

                cmd.CommandText = "SELECT COUNT(*) FROM master.dbo.sysdatabases WHERE name='" + name + "'";
                int exist = (int)cmd.ExecuteScalar();

                if (exist < 1)
                {
                    cmd.CommandText = "EXEC sp_attach_db " + name + ", '" + file + "'";

                    cmd.ExecuteNonQuery();

                    Console.WriteLine("Database " + name + " attached successfully.");
                }
                else
                {
                    Console.WriteLine("Database " + name + " already exist on server.");
                }
            }


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
