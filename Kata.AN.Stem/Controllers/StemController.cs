using Kata.AN.Stem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kata.AN.Stem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StemController : ControllerBase
    {
        private readonly IStemService _stemService;

        public StemController(IStemService stemService)
        {
            this._stemService = stemService;
        }

        [HttpGet(Name = "GetStem")]
        public IActionResult Get([FromQuery] string? stem)
        {
            if (!string.IsNullOrEmpty(stem))
            {
                var words = _stemService.GetWords(stem);
                return Ok(new WordResult { Data = words });
            }
            return BadRequest("The word is empty or null.");
        }
    }

    public class WordResult
    {
        public List<string> Data { get; set; }
        public WordResult()
        {
            this.Data = new List<string>();
        }
    }
}
