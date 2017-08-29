using System;

namespace BLL.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public DateTime DateOrder { get; set; }
        public DateTime DateDelivery { get; set; }
        public string BookName { get; set; }
        public string ReaderName { get; set; }
        public int BookId { get; set; }
        public int ReaderId { get; set; }
    }
}
