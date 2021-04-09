using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestTaskEF.Services;

namespace TestTaskEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StartController : ControllerBase
    {
        private ListHandler _listHandler;
        public StartController(ListHandler listHandler)
        {
            _listHandler = listHandler;
        }
        //Запуск основной задачи
        [HttpGet]
        public async Task<ActionResult> Start()
        {
            await _listHandler.ToGoDept(2);
            return new JsonResult(await _listHandler.Get(1));
        }
    }
}
