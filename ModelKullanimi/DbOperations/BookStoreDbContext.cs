using Microsoft.EntityFrameworkCore;
using ModelKullanimi.Models;

namespace ModelKullanimi.DbOperations
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
