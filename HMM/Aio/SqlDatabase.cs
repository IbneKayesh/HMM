using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Aio
{
    public class SqlDatabase
    {
        public async Task<string> DbWriteAsCommandList(string _conStr, List<string> _command_list)
        {
            string result = AppKeys.PostSuccess;

            await Task.Run(() =>
            {
                SqlConnection con = new SqlConnection(_conStr);
                SqlCommand cmd = new SqlCommand();
                SqlTransaction trn;
                con.Open();
                cmd.Connection = con;
                cmd.CommandTimeout = int.MaxValue;
                trn = con.BeginTransaction();
                cmd.Transaction = trn;
                try
                {
                    foreach (string command in _command_list)
                    {
                        cmd.CommandText = command;
                        cmd.ExecuteNonQuery();
                    }
                    trn.Commit();
                }
                catch (SqlException ex)
                {
                    trn.Rollback();
                    result = ex.Message;
                }
                finally
                {
                    con.Close();
                }
            });
            return result;
        }
        public async Task<string> DbWriteAsCommand(string _conStr, string _command)
        {
            string result = AppKeys.PostSuccess;
            await Task.Run(() =>
            {
                SqlConnection con = new SqlConnection(_conStr);
                SqlCommand cmd = new SqlCommand();
                SqlTransaction trn;
                con.Open();
                cmd.Connection = con;
                cmd.CommandTimeout = int.MaxValue;
                trn = con.BeginTransaction();
                cmd.Transaction = trn;
                try
                {
                    cmd.CommandText = _command;
                    cmd.ExecuteNonQuery();
                    trn.Commit();
                }
                catch (SqlException ex)
                {
                    trn.Rollback();
                    result = ex.Message;
                }
                finally
                {
                    con.Close();
                }
            });
            return result;
        }
        public async Task<DataTable> DbReadAsDataTable(string _conStr, string _query, object[] _parameters = null)
        {
            DataTable dt = new DataTable();
            await Task.Run(() =>
             {
                 DataSet ds = new DataSet();
                 SqlCommand cmd = new SqlCommand();
                 SqlDataAdapter da = new SqlDataAdapter();
                 SqlConnection con = new SqlConnection(_conStr);
                 try
                 {
                     cmd = new SqlCommand(_query, con);
                     if (_parameters != null)
                     {
                         cmd.Parameters.AddRange(_parameters);
                     }
                     cmd.CommandTimeout = int.MaxValue;
                     cmd.CommandType = CommandType.Text;
                     da.SelectCommand = cmd;
                     da.Fill(ds);
                     dt = ds.Tables[0];
                 }
                 catch (SqlException ex)
                 {
                     dt = new DataTable();
                     dt.Columns.Add(AppKeys.GetError, typeof(string));
                     dt.Rows.Add(ex.Message);
                 }
             });
            return dt;
        }
    }
}