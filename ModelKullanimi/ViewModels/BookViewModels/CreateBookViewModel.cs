using System.ComponentModel.DataAnnotations;

namespace ModelKullanimi.ViewModels.BookViewModels
{
    public class CreateBookViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public int GenreId { get; set; }
        [Required]
        public int PageCount { get; set; }
        [Required]
        public DateTime PublishDate { get; set; }
    }
}
