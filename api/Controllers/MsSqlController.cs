using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class MsSqlController : ControllerBase
    {
        private readonly TodoListDBContext _dbContext;
        public MsSqlController(TodoListDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TodoListModel model)
        {
            if (model != null)
            {
                _dbContext.Add(model);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }

        public IActionResult Get()
        {
            return Ok(_dbContext.Todos);
        }
    }
}