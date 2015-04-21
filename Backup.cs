//css_include Config.cs
//css_reference dlls\Microsoft.SqlServer.ConnectionInfo.dll
//css_reference dlls\Microsoft.SqlServer.Management.Sdk.Sfc.dll
//css_reference dlls\Microsoft.SqlServer.Smo.dll
//css_reference dlls\Microsoft.SqlServer.SmoExtended.dll
using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Microsoft.SqlServer.Management.Smo;


class Backup
{
    static public void Main(string[] args)
    {
        SqlConnection connection = new SqlConnection(Config.ConnectionString);
        SqlCommand cmd = new SqlCommand("", connection);


        string [] databases = null;

        try
        {
            connection.Open();

            Console.WriteLine("Connected to server " + Config.DbServer + ".");
            Console.WriteLine("==========================================");


            databases = System.IO.File.ReadAllLines("databases.txt");

            Server server = new Server(Config.DbServer);

            foreach (var db in databases)
            {
                if (!db.StartsWith("-"))
                {
                    Microsoft.SqlServer.Management.Smo.Backup backup = new Microsoft.SqlServer.Management.Smo.Backup();
                    backup.Action = BackupActionType.Database;
                    backup.Database = db;
                    backup.Devices.AddDevice(Config.BackupFolder + db + ".bak", DeviceType.File);
                    backup.BackupSetName = db + " database Backup";
                    backup.BackupSetDescription = db + " database - Full Backup";
                    backup.Initialize = true;
                    //backup.SqlBackupAsync(server);
                    backup.SqlBackup(server);

                    Console.WriteLine("Database " + db + " backup created.");
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
