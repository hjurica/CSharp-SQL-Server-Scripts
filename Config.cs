using System;
using System.Windows.Forms;

public static class Config
{
    // Configuration
    
    // Path to Folder with .mdf files
    private static string folderName = @"C:\databasesFolder";
    // Path to Backup Folder - required '\' at the end of the path
    private static string backupFolder = @"C:\backupFolder\";
    // SQL Server Name
    private static string dbServer = "";
    // Username
    private static string user = "";
    // Password
    private static string pass = "";


    
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
            return "Server=" + DbServer + ";Integrated Security=SSPI;User Id="+ User + ";Password=" + Password + ";";
        }
    }
}
