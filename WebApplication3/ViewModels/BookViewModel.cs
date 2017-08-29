using System.ComponentModel.DataAnnotations;

namespace WebApplication3.ViewModels
{
    public class BookViewModel 
    {
        [Required(ErrorMessage = "Please, input name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please, input author")]
        public string Author { get; set; }
        public int Id { get; set; }
        public decimal? Cost { get; set; }
    }
}