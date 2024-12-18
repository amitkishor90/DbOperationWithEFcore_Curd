using System;
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

        [HttpPost("bulk")]
        public async Task<IActionResult> AddBooks([FromBody] List<BooksModel> model)
        {
            // Validate LanguageId and Country for all books before insertion
            foreach (var bookModel in model)
            {
                var languageExists = await _appContext.Languages.AnyAsync(l => l.Id == bookModel.LanguageId);
                if (!languageExists)
                {
                    return BadRequest($"The LanguageId {bookModel.LanguageId} does not exist for the book titled '{bookModel.Title}'.");
                }
            }
            var books = model.Select(bookModel => new Book
            {
                Title = bookModel.Title,
                Descripation = bookModel.Descripation,
                NoOfPages = bookModel.NoOfPages,
                IsActive = bookModel.IsActive,
                CreateDate = bookModel.CreateDate,
                LanguageId = bookModel.LanguageId,  // Ensure valid LanguageId
                Country = bookModel.Country  // Ensure valid Country
            }).ToList();
            _appContext.Books.AddRange(books);
            // Save changes and check the result
            var savedCount = await _appContext.SaveChangesAsync();

            if (savedCount > 0)
            {
                // If the count is greater than 0, data is saved successfully
                return Ok(new { Message = $"{savedCount} book(s) added successfully!", Books = books });
            }
            else
            {
                // If the count is 0, no data was saved
                return StatusCode(500, "Error: No data was saved to the database.");
            } 
        }


        [HttpPut("{bookId}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int bookId, [FromBody] Book model)
        {
            var book = _appContext.Books.FirstOrDefault(x => x.ID == bookId);
            if (book == null)
            {
                return NotFound();
            }

            book.Title = model.Title;
            book.Descripation = model.Descripation;
            book.NoOfPages = model.NoOfPages;

            await _appContext.SaveChangesAsync();

            return Ok(model);
        }
        [HttpPut("")]
        public async Task<IActionResult> UpdateBookWithSingleQuery([FromBody] Book model)
        {
            _appContext.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _appContext.SaveChangesAsync();

            return Ok(model);
        }

        [HttpPut("bulk")]
        public async Task<IActionResult> UpdateBookInBulk()
        {
            await _appContext.Books
                .Where(x => x.NoOfPages == 100)
                .ExecuteUpdateAsync(x => x
            .SetProperty(p => p.Descripation, p => p.Title + " This is book description 2")
            .SetProperty(p => p.Title, p => p.Title + " updated 2")
            //.SetProperty(p => p.NoOfPages, 100)
            );
            return Ok();
        }

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteBookByIdAsync([FromRoute] int bookId)
        {
            var book = new Book { ID = bookId };
            _appContext.Entry(book).State = EntityState.Deleted;
            await _appContext.SaveChangesAsync();

            //var book = await appDbContext.Books.FindAsync(bookId);

            //if (book == null)
            //{
            //    return NotFound();
            //}
            //appDbContext.Books.Remove(book);
            //await appDbContext.SaveChangesAsync();

            return Ok();
        }


        [HttpDelete("bulk")]
        public async Task<IActionResult> DeleteBooksinBulkAsync()
        {
            //var book = new Book { Id = bookId };
            //appDbContext.Entry(book).State = EntityState.Deleted;
            //await appDbContext.SaveChangesAsync();

            var books = await _appContext.Books.Where(x => x.ID < 8).ExecuteDeleteAsync();

            return Ok();
        }
    }
}
