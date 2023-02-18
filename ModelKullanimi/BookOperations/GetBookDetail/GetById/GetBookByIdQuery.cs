

using AutoMapper;
using ModelKullanimi.DbOperations;
using ModelKullanimi.Models;
using ModelKullanimi.ViewModels.BookViewModels;

namespace ModelKullanimi.BookOperations.GetBookDetail.GetById
{
    public class GetBookByIdQuery
    {
        public int BookId { get; set; }


        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBookByIdQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            Book? book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);

            if (book is null)
                throw new InvalidOperationException("Kitap bulunamadı");

            //BookDetailViewModel bookDetailViewModel = new()
            //{
            //    Title = book.Title,
            //    Genre = ((GenreEnum)book.GenreId).ToString(),
            //    PageCount = book.PageCount,
            //    PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy")
            //};

            BookDetailViewModel bookDetailViewModel = _mapper.Map<BookDetailViewModel>(book);


            return bookDetailViewModel;
        }
    }
}
