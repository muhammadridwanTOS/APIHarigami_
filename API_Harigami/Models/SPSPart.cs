using System.Data;
using System.Data.SqlClient;

namespace API_Harigami.Models
{
    public class SPSPart
    {
        public Response GetList(string? constr, string ActionType, string UserID, string HrgmSPSIDArea, string ModelCode, string Ktsk, string Sfx, string ColorCode, List<dynamic> data)
        {
            Response resp = new Response();
            DataTable dt = new DataTable();

            try
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                DataTable dtJSON = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json)!.Copy();

                using (SqlConnection con = new(constr))
                {
                    con.Open();
                    string sql = "sp_SubAssy_Part_List";

                    SqlCommand cmd = new(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ActionType", ActionType);
                    cmd.Parameters.AddWithValue("UserID", UserID);
                    cmd.Parameters.AddWithValue("HrgmSPSIDArea", HrgmSPSIDArea);
                    cmd.Parameters.AddWithValue("ModelCode", ModelCode);
                    cmd.Parameters.AddWithValue("Ktsk", Ktsk);
                    cmd.Parameters.AddWithValue("Sfx", Sfx);
                    cmd.Parameters.AddWithValue("ColorCode", ColorCode);
                    cmd.Parameters.AddWithValue("Table", dtJSON);

                     

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
                resp.Message = "Error API SQL on Get List HrgmMainlane Area Part!, Error Message = " + exsql.Message;
                resp.Contents = "";
            }
            catch (Exception ex)
            {
                resp.ID = "1";
                resp.Message = "Error API on Get List HrgmMainlane Area Part!, Error Message = " + ex.Message;
                resp.Contents = "";
            }

            return resp;
        }

         
        public Response InsertReplace(string? constr, string UserID , string HrgmSPSIDArea , string ModelCode, string Ktsk, string Sfx, string ColorCode, List<dynamic> data)
        {
            Response resp = new Response();

            try
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                DataTable dtJSON = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json)!.Copy();

                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                using (SqlConnection con = new(constr))
                {
                    con.Open();
                    string sql = "sp_SubAssy_Part_List";

                    SqlCommand cmd = new(sql, con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ActionType", "0"); 
                    cmd.Parameters.AddWithValue("UserID", UserID); 
                    cmd.Parameters.AddWithValue("HrgmSPSIDArea", HrgmSPSIDArea);  
                    cmd.Parameters.AddWithValue("ModelCode", ModelCode);
                    cmd.Parameters.AddWithValue("Ktsk", Ktsk);
                    cmd.Parameters.AddWithValue("Sfx", Sfx);
                    cmd.Parameters.AddWithValue("ColorCode", ColorCode);
                    cmd.Parameters.AddWithValue("Table", dtJSON);

                    SqlDataAdapter da = new(cmd);
                    da.Fill(ds);
                    cmd.Dispose();
                    con.Close();
                }

                resp.ID = ds.Tables[0].Rows[0]["ID"].ToString();
                resp.Message = ds.Tables[0].Rows[0]["Msg"].ToString();
                resp.Contents = "";
            }
            catch (SqlException exsql)
            {
                resp.ID = "1";
                resp.Message = "Error API SQL on SPS Part !, Error Message = " + exsql.Message;
                resp.Contents = "";
            }
            catch (Exception ex)
            {
                resp.ID = "1";
                resp.Message = "Error API on SPS Part Insert !, Error Message = " + ex.Message;
                resp.Contents = "";
            }
            return resp;
        }

        public Response UploadValidation(string? constr, List<dynamic> data)
        {

            Response resp = new Response();
            string sql = "sp_SubAssy_Part_UploadValidation";

            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection con = new(constr))
                {
                    con.Open();

                    SqlCommand cmd = new(sql, con);
                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("UserID", data[0].UserID.ToString());
                    cmd.Parameters.AddWithValue("TableHeader", data[0].TableHeader.ToString());

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
                resp.Message = "Error API SQL on " + sql + "!, Error Message = " + exsql.Message;
                resp.Contents = "";
            }
            catch (Exception ex)
            {
                resp.ID = "1";
                resp.Message = "Error API on Upload Validation SPS Part!, Error Message = " + ex.Message;
                resp.Contents = "";
            }

            return resp;
        }

        public Response Upload(string? constr, string UserID, List<dynamic> data)
        {
            Response resp = new Response();

            try
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                DataTable dtJSON = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json)!.Copy();

                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                using (SqlConnection con = new(constr))
                {
                    con.Open();
                    string sql = "sp_SubAssy_Part_Upload";

                    SqlCommand cmd = new(sql, con);
                    cmd.CommandTimeout = 180;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("UserID", UserID);
                    cmd.Parameters.AddWithValue("Table", dtJSON);

                    SqlDataAdapter da = new(cmd);
                    da.Fill(ds);
                    cmd.Dispose();
                    con.Close();
                }

                resp.ID = ds.Tables[0].Rows[0]["ID"].ToString();
                resp.Message = ds.Tables[0].Rows[0]["Msg"].ToString();
                resp.Contents = ds.Tables[1].AsEnumerable().Select(row => row.Table.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => row[col])).Select(dict => (dynamic)dict).ToList(); ;
            }
            catch (SqlException exsql)
            {
                resp.ID = "1";
                resp.Message = "Error API SQL on upload SPS Part !, Error Message = " + exsql.Message;
                resp.Contents = "";
            }
            catch (Exception ex)
            {
                resp.ID = "1";
                resp.Message = "Error API on upload SPS Part !, Error Message = " + ex.Message;
                resp.Contents = "";
            }
            return resp;
        }
    }
}
