using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestTaskEF.Services;

namespace TestTaskEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResetController : ControllerBase
    {
        private ListHandler _listHandler;
        public ResetController(ListHandler listHandler)
        {
            _listHandler = listHandler;
        }
        //Сброс данных 
        [HttpGet]
        public async Task<ActionResult> Reset() => await _listHandler.Reset();

    }
}
