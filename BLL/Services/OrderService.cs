using System.Collections.Generic;
using BLL.Entities;
using BLL.Interfaces;
using BLL.IRepo;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        
        private readonly IOrderRepository _repository;
        private readonly IBookRepository _bookRepository;
        private readonly IReaderRepository _readerRepository;
        
        public OrderService(
            IOrderRepository repository, IBookRepository bookRepository, IReaderRepository readerRepository)
        {
            _repository = repository;
            _bookRepository = bookRepository;
            _readerRepository = readerRepository;
        }

        public List<string> GetBooksWhithReaderId(int id)
        {
            return _repository.GetBooks(id);
        }

        public List<string> CountOfBooksOnWeek()
        {
            var books=_repository.CountOfBooksOnWeek();
            return books;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public OrderEntity Get(int id)
        {
            OrderEntity orderDb = _repository.Get(id);
            var bookName = _bookRepository.Get(orderDb.BookId).Name;
            var readerName = _readerRepository.Get(orderDb.ReaderId).Fname;
            orderDb.BookName = bookName;
            orderDb.ReaderName = readerName;
            return orderDb;
        }

        List<OrderEntity> IOrderService.GetMany()
        {
            var listOrder = _repository.GetMany();
            foreach (var listpart in listOrder)
            {
                var bookName = _bookRepository.Get(listpart.BookId).Name;
                var readerName = _readerRepository.Get(listpart.ReaderId).Fname;
                listpart.BookName = bookName;
                listpart.ReaderName = readerName;
            }
            return listOrder;
        }

        public void Update(OrderEntity model)
        {
            _repository.Update(model);
        }

        public bool IsBookExist(int id)
        {
            var result=_repository.IsBookExist(id);
            return result;
        }

        public void Create(OrderEntity model)
        {
            var order = new OrderEntity
            {
                Id = model.Id,
                DateDelivery = model.DateDelivery,
                DateOrder = model.DateOrder,
                BookId = _bookRepository.GetBook(model.BookName).Id,
                ReaderId = _readerRepository.GetReader(model.ReaderName).Id,
                BookName = model.BookName,
                ReaderName = model.ReaderName

            };
            if (order.BookId == -1)
            {
                return;
            }
            _repository.Create(order);
        }
    }
}
