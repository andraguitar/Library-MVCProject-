using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BLL.Entities;

namespace WebApplication3.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Incorrect e-mail adress")]
        [Required(ErrorMessage = "Please, input Email")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Please, input your name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please, input your age")]
        [Range(typeof(int), "0", "150")]
        public int Age { get; set; }
        [DisplayName("Role")]
        public MyRoles Role { get; set; }     

    }
}