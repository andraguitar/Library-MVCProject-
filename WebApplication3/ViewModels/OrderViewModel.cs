using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOrder { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateDelivery { get; set; }
        public string BookName { get; set; }
        public string ReaderName { get; set; }
    }
}