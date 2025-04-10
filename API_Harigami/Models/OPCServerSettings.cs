using System.Data;
using System.Data.SqlClient;

namespace API_Harigami.Models
{
    public class OPCServerSettings
    {
        public Response getOPCSettings(string? constr)
        {
            Response resp = new Response();
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new(constr))
                {
                    con.Open();
                    string sql = "sp_cls_OPCServerSettings_Sel";

                    SqlCommand cmd = new(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new(cmd);
                    da.Fill(dt);
                    cmd.Dispose();
                    con.Close();
                }


                //===================================================
                // Success response
                //===================================================
                resp.ID = "0";
                resp.Message = "Success";
                resp.Contents = dt.AsEnumerable().Select(row => row.Table.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => row[col])).Select(dict => (dynamic)dict).ToList();
            }
            catch (SqlException exsql)
            {
                resp.ID = "1";
                resp.Message = "Error API SQL on Get OPC Settings!, Error Message = " + exsql.Message;
                resp.Contents = "";
            }
            catch (Exception ex)
            {
                resp.ID = "1";
                resp.Message = "Error API on Get OPC Settings !, Error Message = " + ex.Message;
                resp.Contents = "";
            }

            return resp;
        }
    }
}
