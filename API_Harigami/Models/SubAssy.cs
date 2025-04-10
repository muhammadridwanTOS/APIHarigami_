using System.Data;
using System.Data.SqlClient;

namespace API_Harigami.Models
{
    public class SubAssy
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
                    string sql = "sp_Subassy_Tablet_Sel";

                    SqlCommand cmd = new(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ActionType", data[0].ActionType.ToString());
                    cmd.Parameters.AddWithValue("AreaId", data[0].AreaId.ToString());
                    cmd.Parameters.AddWithValue("PosID", data[0].PosID.ToString());
                    cmd.Parameters.AddWithValue("LiftNo", data[0].LiftNo.ToString());
                    cmd.Parameters.AddWithValue("Buffer", data[0].Buffer.ToString());
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
                resp.Message = "Error API SQL on Get List HrgmSubAssy !, Error Message = " + exsql.Message;
                resp.Contents = "";
            }
            catch (Exception ex)
            {
                resp.ID = "1";
                resp.Message = "Error API on Get List HrgmSubAssy !, Error Message = " + ex.Message;
                resp.Contents = "";
            }

            return resp;
        }
        public Response CRUD(string? constr, List<dynamic> data)
        {
            Response resp = new Response();
            try
            {
                /*==============================================================
                 Delete All data shop detail where shopcode = @shopcode
                 ==============================================================*/
                DataTable dt = new DataTable();
                using (SqlConnection con = new(constr))
                {
                    con.Open();
                    string sql = "sp_Subassy_Tablet_Next";

                    SqlCommand cmd = new(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("ActionType", data[0].ActionType.ToString());
                    cmd.Parameters.AddWithValue("UserID", data[0].UserID.ToString());
                    cmd.Parameters.AddWithValue("AreaId", data[0].AreaId.ToString());
                    cmd.Parameters.AddWithValue("PosID", data[0].PosID.ToString());
                    cmd.Parameters.AddWithValue("ColPos", data[0].ColPos.ToString());
                    cmd.Parameters.AddWithValue("LiftNoCurrent", data[0].LiftNoCurrent.ToString());
                    cmd.Parameters.AddWithValue("Buffer", data[0].buffer.ToString());

                    SqlDataAdapter da = new(cmd);
                    da.Fill(dt);
                    cmd.Dispose();
                    con.Close();
                }


                //===================================================
                // Success response
                //===================================================
                resp.ID = dt.Rows[0]["ID"].ToString();
                resp.Message = dt.Rows[0]["Msg"].ToString();
                resp.Contents = "";
            }
            catch (SqlException exsql)
            {
                resp.ID = "1";
                resp.Message = "Error API SQL on Update Subassy !, Error Message = " + exsql.Message;
                resp.Contents = "";
            }
            catch (Exception ex)
            {
                resp.ID = "1";
                resp.Message = "Error API on Update Subassy !, Error Message = " + ex.Message;
                resp.Contents = "";
            }

            return resp;
        }

        public Response JUMPLIFTING(string? constr, List<dynamic> data)
        {
            Response resp = new Response();
            try
            {
                /*==============================================================
                 Delete All data shop detail where shopcode = @shopcode
                 ==============================================================*/
                DataTable dt = new DataTable();
                using (SqlConnection con = new(constr))
                {
                    con.Open();
                    string sql = "sp_Subassy_Tablet_Jump";

                    SqlCommand cmd = new(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("ActionType", data[0].ActionType.ToString());
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
                resp.ID = dt.Rows[0]["ID"].ToString();
                resp.Message = dt.Rows[0]["Msg"].ToString();
                resp.Contents = "";
            }
            catch (SqlException exsql)
            {
                resp.ID = "1";
                resp.Message = "Error API SQL on Update Subassy !, Error Message = " + exsql.Message;
                resp.Contents = "";
            }
            catch (Exception ex)
            {
                resp.ID = "1";
                resp.Message = "Error API on Update Subassy !, Error Message = " + ex.Message;
                resp.Contents = "";
            }

            return resp;
        }


    }
}
