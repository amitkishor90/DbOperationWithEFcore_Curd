using DbOperationWithEFcore_Curd.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DbOperationWithEFcore_Curd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(AppDbContext appContext) : ControllerBase
    {
        public async Task<IActionResult> AddNewBook([FromForm] Book book)
        {
             
            appContext.Books.Add(book);
            await appContext.SaveChangesAsync();
            return Ok(book);
        }
    }
}
