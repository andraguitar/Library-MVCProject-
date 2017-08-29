using System.ComponentModel.DataAnnotations;

namespace WebApplication3.ViewModels
{
    public class RegisterViewModel
    {
        public string Name { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Range(typeof(int), "0", "150")]
        public int Age { get; set; }
    }
}