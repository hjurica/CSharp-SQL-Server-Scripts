using System;
using System.Windows.Forms;

public static class Config
{
    // Settings
    private static string folderName = @"E:\baze\backup";
    // backupFolder treba imati \\ na kraju
    private static string backupFolder = @"E:\baze\backup\";
    private static string dbServer = "localhost";
    private static string user = "METRO\\jurica";
    private static string pass = "123";


    
    public static string FolderName
    {
        get
        {
            return folderName;
        }
    }

    public static string BackupFolder
    {
        get
        {
            return backupFolder;
        }
    }

    public static string DbServer
    {
        get
        {
            return dbServer;
        }
    }

    public static string User
    {
        get
        {
            return user;
        }
    }

    public static string Password
    {
        get
        {
            return pass;
        }
    }

    public static string ConnectionString
    {
        get
        {
            return "Server=" + DbServer + ";Integrated Security=SSPI;User Id="+ User + ";Password=" + Password + ";"; ;
        }
    }
}
