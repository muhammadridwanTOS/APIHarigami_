using System.Data;
using System.Data.SqlClient;

namespace API_Harigami.Models
{
    public class Mainlane
    {
        public Response GetList(string? constr, List<dynamic> data)
        {
            Response resp = new Response();
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new(constr))
                {
                    con.Open();
                    string sql = "sp_MainLane_Tablet_Sel";

                    SqlCommand cmd = new(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("AreaId", data[0].AreaId.ToString());
                    cmd.Parameters.AddWithValue("PosID", data[0].PosID.ToString());
                    cmd.Parameters.AddWithValue("LiftNo", data[0].LiftNo.ToString());
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
                resp.Message = "Error API SQL on Get List HrgmMainlane !, Error Message = " + exsql.Message;
                resp.Contents = "";
            }
            catch (Exception ex)
            {
                resp.ID = "1";
                resp.Message = "Error API on Get List HrgmMainlane !, Error Message = " + ex.Message;
                resp.Contents = "";
            }

            return resp;
        }

        public Response GetListOPCLifting(string? constr, List<dynamic> data)
        {
            Response resp = new Response();
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new(constr))
                {
                    con.Open();
                    string sql = "sp_MainLane_Tablet_GetOpcLifting";

                    SqlCommand cmd = new(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("AreaId", data[0].AreaId.ToString());
                    cmd.Parameters.AddWithValue("PosID", data[0].PosID.ToString()); 
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
                resp.Message = "Error API SQL on Get List HrgmMainlane !, Error Message = " + exsql.Message;
                resp.Contents = "";
            }
            catch (Exception ex)
            {
                resp.ID = "1";
                resp.Message = "Error API on Get List HrgmMainlane !, Error Message = " + ex.Message;
                resp.Contents = "";
            }

            return resp;
        }

    }
}
