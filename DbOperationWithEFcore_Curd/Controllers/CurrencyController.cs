using Microsoft.AspNetCore.Mvc;
using DbOperationWithEFcore_Curd.Data;
using Microsoft.EntityFrameworkCore;

namespace DbOperationWithEFcore_Curd.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly AppDbContext appContext;
        public CurrencyController(AppDbContext appContext)
        {
           this.appContext = appContext;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetallCurrency()
        {
            var result = await appContext.CurrencyTypes.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCurrencybyiD(int Id)
        {
            var result = await appContext.CurrencyTypes.FindAsync(Id);
            return Ok(result);
        }
    }
}
