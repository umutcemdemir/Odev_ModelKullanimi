

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ModelKullanimi.BookOperations.CreateBook;
using ModelKullanimi.BookOperations.DeleteBook;
using ModelKullanimi.BookOperations.GetBookDetail.GetById;
using ModelKullanimi.BookOperations.GetBooks;
using ModelKullanimi.BookOperations.GetBooksFilter;
using ModelKullanimi.BookOperations.UpdateBook.UpdateWithPut;
using ModelKullanimi.DbOperations;
using ModelKullanimi.ViewModels.BookViewModels;
using ModelValidasyonu.BookOperations.UpdateBook.UpdateBookWithPatch;

namespace ModelKullanimi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BookBodyController : Controller
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookBodyController(BookStoreDbContext context, IMapper mapper)
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

        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            BookDetailViewModel result;

            try
            {
                GetBookByIdQuery query = new(_context, _mapper);
                query.BookId = id;
                result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }


        [HttpGet]
        [Route("GetByFilter")]
        public IActionResult GetByFilter([FromQuery] string? title = null, [FromQuery] string? genre = null,
            [FromQuery] string? pageCount = null, [FromQuery] string? publishYear = null)
        {
            //List<Book> books = _context.Books.OrderBy(x => x.Id).ToList();

            //if (books is null)
            //    return NotFound("Liste bulunamadı");


            //if (title is not null)
            //{
            //    books = books.Where(x => x.Title.ToLower().Contains(title.ToLower())).ToList();

            //    if (books.Count == 0)
            //        return NotFound("Liste bulunamadı");
            //}

            //if (genreId is not null)
            //{
            //    try
            //    {
            //        Convert.ToUInt16(genreId);
            //    }
            //    catch (FormatException)
            //    {
            //        return BadRequest("Lütfen tür ıd'si girin");
            //    }

            //    books = books.Where(x => x.GenreId.ToString().ToLower().Contains(genreId.ToString())).ToList();

            //    if (books.Count == 0)
            //        return NotFound("Liste bulunamadı");
            //}

            //if (pageCount is not null)
            //{
            //    try
            //    {
            //        Convert.ToUInt16(pageCount);
            //    }
            //    catch (FormatException)
            //    {
            //        return BadRequest("Lütfen sayfa sayısı girin");
            //    }

            //    books = books.Where(x => x.PageCount.ToString().ToLower().
            //                      Equals(pageCount)).ToList();

            //    if (books.Count == 0)
            //        return NotFound("Liste bulunamadı");
            //}


            //if (publishYear is not null)
            //{
            //    try
            //    {
            //        Convert.ToUInt16(publishYear);
            //    }
            //    catch (FormatException)
            //    {
            //        return BadRequest("Lütfen yayın yılı girin");
            //    }

            //    books = books.Where(x => x.PublishDate.Year.ToString().Equals(publishYear)).ToList();

            //    if (books.Count == 0)
            //        return NotFound("Liste bulunamadı");
            //}

            List<GetBooksByFilterViewModel> result;

            try
            {
                GetBooksByFilterQuery query = new(_context);
                query.BookTitle = title;
                query.BookGenre = genre;
                query.BookPageCount = pageCount;
                query.BookPublishYear = publishYear;
                result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookViewModel newBook)
        {
            CreateBookCommand book = new(_context, _mapper);

            try
            {
                book.Model = newBook;

                book.Handle();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return Created("~api/Book/GetBooks", newBook);

        }

        [HttpPut("{id}")]
        public IActionResult UpdateUserWithPut([FromBody] UpdateBookWithPutViewModel updateBook, int id)
        {
            try
            {
                UpdateBookWithPutCommand command = new(_context);
                command.BookId = id;
                command.Model = updateBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Kitap Güncellenmiştir");
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateUserWithPatch([FromBody] UpdateBookWithPatchViewModel updateBook, int id)
        {
            try
            {
                UpdateBookWithPatchCommand command = new(_context);
                command.BookId = id;
                command.Model = updateBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Kitap güncellenmiştir");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                DeleteBookCommand command = new(_context);
                command.BookId = id;
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
