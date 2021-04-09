using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestTaskEF.Models;
using TestTaskEF.Services;

namespace TestTaskEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StampsController : ControllerBase
    {
        private StampHandler _stampHandler;
        public StampsController(StampHandler stampHandler)
        {
            _stampHandler = stampHandler;
        }

        [HttpGet]
        public async Task<ActionResult> Get() => await _stampHandler.Get();
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id) => await _stampHandler.Get(id);
        [HttpPost]
        public async Task<ActionResult> Post(StampModel model) => await _stampHandler.Post(model);
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, StampModel model) => await _stampHandler.Put(id, model);
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) => await _stampHandler.Delete(id);
    }
}
