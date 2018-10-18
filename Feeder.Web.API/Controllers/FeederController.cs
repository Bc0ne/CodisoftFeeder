namespace Feeder.Web.API.Controllers
{
    using System.Threading.Tasks;
    using Feeder.Core;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/feeds")]
    [ApiController]
    public class FeederController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            FeederService service = new FeederService("http://feeds.feedburner.com/netCurryRecentArticles");
            await service.GetFeedsAsync();
            return Ok();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
