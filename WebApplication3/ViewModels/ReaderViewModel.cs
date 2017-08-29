using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication3.ViewModels
{
    public class ReaderViewModel
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Adress { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Incorrect e-mail adress")]
        [Required(ErrorMessage = "Please, input Email")]
        public string Email { get; set; }
        [RegularExpression(@"^\+\d{3}-\d{2}-\d{3}-\d{2}-\d{2}$", ErrorMessage = "Number must be have a number format +xxx-xx-xxx-xx-xx")]
        public string Telephone { get; set; }
        public decimal? CommonCost { get; set; }
    }
}