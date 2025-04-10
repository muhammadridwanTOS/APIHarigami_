using System.Data;
using System.Data.SqlClient;

namespace API_Harigami.Models
{
    public class MenuLeader
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
                    string sql = "sp_Menuleader_Sel";

                    SqlCommand cmd = new(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ActionType", data[0].ActionType.ToString());
                    cmd.Parameters.AddWithValue("AreaID", data[0].AreaID.ToString());
                    cmd.Parameters.AddWithValue("PosID", data[0].PosID.ToString());
                    cmd.Parameters.AddWithValue("ModelCode", data[0].ModelCode.ToString());
                    cmd.Parameters.AddWithValue("Color", data[0].Color.ToString());
                    cmd.Parameters.AddWithValue("Sfx", data[0].Sfx.ToString());
                    cmd.Parameters.AddWithValue("VIN", data[0].VIN.ToString());
                    cmd.Parameters.AddWithValue("Lifting", data[0].Lifting.ToString());
                    SqlDataAdapter da = new(cmd);
                    da.Fill(dt);
                    cmd.Dispose();
                    con.Close();
                } 
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

    }
}
