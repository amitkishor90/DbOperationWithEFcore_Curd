using DbOperationWithEFcore_Curd.Data;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetCurrencybyiD([FromRoute] int Id)
        {
            var GetCurrencybyiDresult = await appContext.CurrencyTypes.FindAsync(Id);
            return Ok(GetCurrencybyiDresult);
        }


        [HttpGet("{name}")]
        public async Task<IActionResult> GetCurrencybyName([FromRoute] string name)
        {
            var GetCurrencybynameresult = await appContext.CurrencyTypes.
                                        Where(x => x.Title == name).FirstOrDefaultAsync();
            if (GetCurrencybynameresult != null)
            {
                return Ok(GetCurrencybynameresult);
            }
            return Ok(GetCurrencybynameresult);
        }
    }
}
