using System.Data;
using System.Data.SqlClient;

namespace API_Harigami.Models
{
    public class GlobalFilter
    {
        public string? Code { get; set; }
        public string? CodeDesc { get; set; }
        public string? exist { get; set; }
        public string? ActionType { get; set; }
        public string? Param { get; set; }
        public string? Param1 { get; set; }
        public string? Param2 { get; set; }
        public string? Param3 { get; set; }

        public Response Filter(string ? constr, List<GlobalFilter> data)
        {
            Response resp = new Response();
			DataTable dt = new DataTable();

			try
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    string sql = "sp_GlobalFilter";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ActionType", data[0].ActionType);
                    cmd.Parameters.AddWithValue("Param", data[0].Param);
                    cmd.Parameters.AddWithValue("Param1", data[0].Param1);
                    cmd.Parameters.AddWithValue("Param2", data[0].Param2);
                    cmd.Parameters.AddWithValue("Param3", data[0].Param3);

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
                resp.Message = "Error API SQL on global filter!, Error Message = " + exsql.Message;
                resp.Contents = "";
            }
            catch (Exception ex)
            {
                resp.ID = "1";
                resp.Message = "Error API on global filter!, Error Message = " + ex.Message;
                resp.Contents = "";
            }
            return resp;
        }


        public Response Fontstyle(string? constr, List<GlobalFilter> data)
        {
            Response resp = new Response();
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    string sql = "sp_GlobalFontstyle";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ActionType", data[0].ActionType); 
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
                resp.Message = "Error API SQL on global fontstyle!, Error Message = " + exsql.Message;
                resp.Contents = "";
            }
            catch (Exception ex)
            {
                resp.ID = "1";
                resp.Message = "Error API on global fontstyle!, Error Message = " + ex.Message;
                resp.Contents = "";
            }
            return resp;
        }
    }
}
