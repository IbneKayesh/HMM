using System;

public static class AioData
{
    public static string PasswordHashKey = "2017Nov;KayesH";
    public static DateTime thisDT = DateTime.Now;
    public static int yyyy = thisDT.Year;
    public static string yyMM = thisDT.ToString("yy") + thisDT.ToString("MM");

    public static string ApplicationName = "Hostel/House Rent and Meal Management";
    public static string DevicesId = Environment.MachineName;
    public static string UserId = Environment.UserName;
    public static string UserName = "";
    public static DateTime LoginDateTime;
    public static string NetworkConnection = "";
    public static string ModuleName = "";

    public static int CenterId = 0;
    public static string CenterName = "";

    public static DateTime NowDateTime;
    
}