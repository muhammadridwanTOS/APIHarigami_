using API_Harigami.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API_Harigami.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
    [Authorize]
    public class GlobalFilterController : ControllerBase
    {
        readonly IConfiguration _config;
        public string? constr;

        GlobalFilter db = new GlobalFilter();
        public GlobalFilterController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public ActionResult Filter([FromBody] dynamic prm)
        {
            constr = _config.GetConnectionString("DefaultConnection");
            Response resp = new Response();

            try
            {
				List<GlobalFilter> data = JsonConvert.DeserializeObject<List<GlobalFilter>>(prm.ToString());
				resp = db.Filter(constr, data);
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

        [HttpPost]
        public ActionResult Fontstyle([FromBody] dynamic prm)
        {
            constr = _config.GetConnectionString("DefaultConnection");
            Response resp = new Response();

            try
            {
                List<GlobalFilter> data = JsonConvert.DeserializeObject<List<GlobalFilter>>(prm.ToString());
                resp = db.Fontstyle(constr, data);
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
