
using AutoMapper;
using ModelKullanimi.DbOperations;
using ModelKullanimi.Models;
using ModelKullanimi.ViewModels.BookViewModels;

namespace ModelKullanimi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookViewModel Model { get; set; }

        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateBookCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public void Handle()
        {
            Book? book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title &&
                x.GenreId == Model.GenreId && x.PageCount == Model.PageCount &&
                x.PublishDate == Model.PublishDate);

            if (book is not null)
                throw new InvalidOperationException("Kitap Zaten Mevcut");

            //book = new Book()
            //{
            //    Title = Model.Title,
            //    GenreId = Model.GenreId,
            //    PageCount = Model.PageCount,
            //    PublishDate = Model.PublishDate
            //};

            book = _mapper.Map<Book>(Model);

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();

        }
    }
}
