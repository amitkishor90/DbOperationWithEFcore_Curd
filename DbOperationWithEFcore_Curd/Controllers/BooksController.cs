using DbOperationWithEFcore_Curd.Data;
using DbOperationWithEFcore_Curd.UserDataModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbOperationWithEFcore_Curd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _appContext;

        public BooksController(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        [HttpPost("AddNewBook")]
        public async Task<IActionResult> AddNewBook([FromBody] BooksModel bookModel)
        {
            // Check if the LanguageId exists in the Languages table
            var languageExists = await _appContext.Languages.AnyAsync(l => l.Id == bookModel.LanguageId);

            if (!languageExists)
            {
                // If the LanguageId does not exist, return a 400 Bad Request response
                return BadRequest($"The LanguageId {bookModel.LanguageId} does not exist.");
            }
            
            // Map BooksModel to Book entity
            var book = new Book
            {
                Title = bookModel.Title,
                Descripation = bookModel.Descripation,
                NoOfPages = bookModel.NoOfPages,
                IsActive = bookModel.IsActive,
                CreateDate = bookModel.CreateDate,
                LanguageId = bookModel.LanguageId,  // Ensure valid LanguageId
                Country = bookModel.Country
            };

            // Add the book to the database and save
            _appContext.Books.Add(book);
            await _appContext.SaveChangesAsync();

            // Return the added book as the response
            return Ok(book);
        }

    }
}
