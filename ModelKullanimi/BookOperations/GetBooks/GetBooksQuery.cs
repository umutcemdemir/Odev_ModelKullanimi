
using AutoMapper;
using ModelKullanimi.DbOperations;
using ModelKullanimi.Models;
using ModelKullanimi.ViewModels.BookViewModels;

namespace ModelKullanimi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBooksQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<BooksViewModel> Handle()
        {
            List<Book> books = _dbContext.Books.OrderBy(x => x.Id).ToList();

            List<BooksViewModel> booksViewModel = _mapper.Map<List<BooksViewModel>>(books);

            //List<BooksViewModel> booksViewModel = new List<BooksViewModel>();

            //foreach (Book book in books)
            //{
            //    booksViewModel.Add(new BooksViewModel
            //    {
            //        Title = book.Title,
            //        Genre = ((GenreEnum)book.GenreId).ToString(),
            //        PageCount = book.PageCount,
            //        PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy")
            //    });
            //}

            return booksViewModel;
        }
    }
}
