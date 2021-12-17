using Aio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
public class AioDLL
{
    private static string Conf_TOKEN = ConfigurationManager.AppSettings["APIToken"];
    private static string Conf_CON_STRING = ConfigurationManager.AppSettings["APIDb"];
    
    private static string sql_token_no = "AIO-TOKEN-SQL-2021-KEY";
    private static string sql_con_str = "AioSqlDb_1st";
    



    static SqlDatabase _SqlDbClient = new SqlDatabase();

    #region Sql_DB_Method
    public async static Task<DataTable> GetAsDataTableSqlDB(string sql, object[] _parameters = null)
    {
        return await _SqlDbClient.DbReadAsDataTable(GetConnectionString(sql_con_str), _query: sql, _parameters: _parameters);
    }
    public async static Task<string> PostAsSqlListSqlDB( List<string> sqlList)
    {
        return await _SqlDbClient.DbWriteAsCommandList(GetConnectionString(sql_con_str), _command_list: sqlList);
    }
    public async static Task<string> PostAsSqlSqlDB(string sql)
    {
        return await _SqlDbClient.DbWriteAsCommand(GetConnectionString(sql_con_str), _command: sql);
    }
    //public static DataTable GetAsDataTableSqlDB(string _con, string sql, object[] _parameters = null)
    //{
    //    return _SqlDbClient.DbReadAsDataTable(GetConnectionString(_con), _query: sql, _parameters: _parameters);
    //}
    //public static string PostAsSqlListSqlDB(string _con, List<string> sqlList)
    //{
    //    return _SqlDbClient.DbWriteAsCommandList(GetConnectionString(_con), _command_list: sqlList);
    //}
    #endregion



    private static string GetConnectionString(string _c)
    {
        return ConfigurationManager.ConnectionStrings[_c].ConnectionString;
    }



    //string sql_1 = "SELECT TOKENNUMBER,AIOKEY from AIOTOKEN WHERE TOKENNUMBER=:p1";
    //OracleParameter[] parameters = new OracleParameter[] {
    //         new OracleParameter("p1","AIO TOKEN") };
    //var _data = AioOraDbEF.SqlSelectToListModel<AIOTOKEN>(WebConfConStr, sql_1, parameters);



}