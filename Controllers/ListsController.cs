using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestTaskEF.Models;
using TestTaskEF.Services;

namespace TestTaskEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListsController : ControllerBase
    {
        private ListHandler _listHandler;
        public ListsController(ListHandler listHandler)
        {
            _listHandler = listHandler;
        }
        [HttpGet]
        public async Task<ActionResult> Get() => await _listHandler.Get();
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id) => await _listHandler.Get(id);
        [HttpPost]
        public async Task<ActionResult> Post(ListModel model) => await _listHandler.Post(model);
        [HttpPut("{id}/{StampId}")]
        public async Task<ActionResult> Put(int id, int StampId) => await _listHandler.Put(id, StampId);
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(ListModel model) => await _listHandler.Put(model);
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) => await _listHandler.Delete(id);


    }
}
