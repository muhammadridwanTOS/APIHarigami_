using System.Data;
using System.Data.SqlClient;

namespace API_Harigami.Models
{
    public class SPSPos
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
                    string sql = "sp_SubAssy_Pos_List";

                    SqlCommand cmd = new(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ActionType", data[0].ActionType.ToString());

                    //===================================================
                    //For only get list HrgmSPSIDPos by @HrgmSPSIDPos
                    //===================================================
                    if (data[0].ActionType.ToString() == "1")
                    {
                        cmd.Parameters.AddWithValue("HrgmSPSIDPos", data[0].HrgmSPSIDPos.ToString());
                    }

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
                resp.Message = "Error API SQL on Get List SPS Pos!, Error Message = " + exsql.Message;
                resp.Contents = "";
            }
            catch (Exception ex)
            {
                resp.ID = "1";
                resp.Message = "Error API on Get List SPS Pos!, Error Message = " + ex.Message;
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
                    string sql = "sp_SubAssy_Pos_IUD";

                    SqlCommand cmd = new(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("ActionType", data[0].ActionType.ToString()); 
                    cmd.Parameters.AddWithValue("UserID", data[0].UserID.ToString());
                    cmd.Parameters.AddWithValue("HrgmSPSIDPos", data[0].HrgmSPSIDPos.ToString());
                    cmd.Parameters.AddWithValue("HrgmSPSIDArea", data[0].HrgmSPSIDArea.ToString()); 
                  
                    //===================================================
                    //For only Insert and Update
                    //===================================================
                    if (data[0].ActionType.ToString() != "2")
                    {
                        cmd.Parameters.AddWithValue("PosName", data[0].PosName.ToString()); 
                        cmd.Parameters.AddWithValue("PosDesc", data[0].PosDesc.ToString());  
                        cmd.Parameters.AddWithValue("Buffer", data[0].Buffer.ToString()); 
                        cmd.Parameters.AddWithValue("HrgmType", data[0].HrgmType.ToString()); 
                        cmd.Parameters.AddWithValue("IPController", data[0].IPController.ToString()); 
                    }

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
                resp.Message = "Error API SQL on Update SPS Pos!, Error Message = " + exsql.Message;
                resp.Contents = "";
            }
            catch (Exception ex)
            {
                resp.ID = "1";
                resp.Message = "Error API on Update SPS Pos!, Error Message = " + ex.Message;
                resp.Contents = "";
            }

            return resp;
        }

        public Response UploadValidation(string? constr, List<dynamic> data)
        {

            Response resp = new Response();
            string sql = "sp_SubAssy_Pos_UploadValidation";

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
                resp.Message = "Error API on Upload Validation SPS Pos!, Error Message = " + ex.Message;
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
                    string sql = "sp_SubAssy_Pos_Upload";

                    SqlCommand cmd = new(sql, con);
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
                resp.Message = "Error API SQL on upload SPS Pos !, Error Message = " + exsql.Message;
                resp.Contents = "";
            }
            catch (Exception ex)
            {
                resp.ID = "1";
                resp.Message = "Error API on upload SPS Pos !, Error Message = " + ex.Message;
                resp.Contents = "";
            }
            return resp;
        }
    }
}
