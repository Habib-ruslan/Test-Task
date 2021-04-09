using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestTaskEF.Models;
using TestTaskEF.Services;

namespace TestTaskEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeptsController : ControllerBase
    {
        private DeptHandler _deptHandler;
        public DeptsController(DeptHandler deptHandler)
        {
            _deptHandler = deptHandler;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id) => await _deptHandler.Get(id);
        [HttpGet]
        public async Task<ActionResult> Get() => await _deptHandler.Get();
        [HttpPost]
        public async Task<ActionResult> Post(DeptModel model) => await _deptHandler.Post(model);
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) => await _deptHandler.Delete(id);
    }
}
