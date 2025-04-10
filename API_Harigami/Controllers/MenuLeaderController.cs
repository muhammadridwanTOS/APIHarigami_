using API_Harigami.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace API_Harigami.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class MenuLeaderController : Controller
    {
        public readonly IConfiguration _config;
        public string? constr;

        MenuLeader db = new MenuLeader();
        public MenuLeaderController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public ActionResult GetList([FromBody] dynamic prm)
        {
            constr = _config.GetConnectionString("DefaultConnection");
            Response resp = new Response();

            try
            {
                List<dynamic> data = JsonConvert.DeserializeObject<List<dynamic>>(prm.ToString());
                resp = db.GetList(constr, data);
                if (resp.ID == "0")
                {
                    return Ok(resp);
                }
                else
                {
                    return BadRequest(resp);
                }
            }
            catch (Exception ex)
            {
                resp.ID = "1";
                resp.Message = ex.Message;
                resp.Contents = "";

                return BadRequest(resp);
            }
        } 
    }
}
