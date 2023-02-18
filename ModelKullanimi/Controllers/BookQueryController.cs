

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ModelKullanimi.BookOperations.DeleteBook;
using ModelKullanimi.BookOperations.GetBookDetail.GetById;
using ModelKullanimi.BookOperations.GetBooks;
using ModelKullanimi.BookOperations.UpdateBook.UpdateWithPut;
using ModelKullanimi.DbOperations;
using ModelKullanimi.ViewModels.BookViewModels;
using ModelValidasyonu.BookOperations.UpdateBook.UpdateBookWithPatch;

namespace ModelKullanimi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BookQueryController : Controller
    {

        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookQueryController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new(_context, _mapper);
            List<BooksViewModel> result = query.Handle();

            if (result is null)
                return NotFound("Liste bulunamadı");

            return Ok(result);
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetBook([FromQuery] string id)
        {
            BookDetailViewModel result;

            try
            {
                GetBookByIdQuery query = new(_context, _mapper);
                query.BookId = Convert.ToInt16(id);
                result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);

        }

        [HttpPut]
        public IActionResult UpdateUserWithPut([FromBody] UpdateBookWithPutViewModel updateBook,
            [FromQuery] string id)
        {
            try
            {
                UpdateBookWithPutCommand command = new(_context);
                command.BookId = Convert.ToInt16(id);
                command.Model = updateBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Kitap güncellenmiştir");
        }

        [HttpPatch]
        public IActionResult UpdateUserWithPatch([FromBody] UpdateBookWithPatchViewModel updateBook,
            [FromQuery] string id)
        {
            try
            {
                UpdateBookWithPatchCommand command = new(_context);
                command.BookId = Convert.ToInt16(id);
                command.Model = updateBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Kitap güncellenmiştir");
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] string id)
        {
            try
            {
                DeleteBookCommand command = new(_context);
                command.BookId = Convert.ToInt16(id);
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Kitap silinmiştir");
        }
    }
}
