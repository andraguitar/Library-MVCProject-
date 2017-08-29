using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BLL.Entities;
using BLL.IRepo;
using Dapper;


namespace DALwhithDapper.DapperRepository
{
    public class OrderDapRepository : IOrderRepository
    {
        private readonly IDbConnection _db;

        public List<string> CountOfBooksOnWeek()
        {
            var books = _db.Query<string>("select Name from Books where Id=(Select BookId From Orders Where DateOrder BETWEEN DATEADD(D, -7, GETDATE()) AND GETDATE())").ToList();
            return books;
        }

        public OrderDapRepository(IDbConnection db)
        {
            _db = db;
        }

        public void Create(OrderEntity model)
        { 
            var sqlQuery = "INSERT INTO Orders (DateOrder, DateDelivery, BookId, ReaderId) VALUES(@DateOrder, @DateDelivery, @BookId, @ReaderId)";
            _db.Execute(sqlQuery, model);
        }

        public void Delete(int id)
        {
            var sqlQuery = "DELETE FROM Orders WHERE Id = @id";
            _db.Execute(sqlQuery, new { id });
        }

        public OrderEntity Get(int id)
        {
            var order = _db.Query<OrderEntity>("SELECT * FROM Orders WHERE Id = @id", new { id }).FirstOrDefault();

            return order;
        }

        public List<OrderEntity> GetMany()
        {
            var orders = _db.Query<OrderEntity>("SELECT * FROM Orders").ToList();

            return orders;
        }

        public List<string> GetBooks(int id)
        {
            var bookList = _db.Query<string>("SELECT Name FROM Books INNER " +
                                          "JOIN Orders ON Books.Id = Orders.BookId Where Orders.ReaderId = @Id", new { id }).ToList();

            return bookList;
        }

        public void Update(OrderEntity model)
        {
            var sqlQuery = "UPDATE Orders SET DateOrder = @DateOrder, DateDelivery = @DateDelivery, BookId = @BookId, ReaderId =@ReaderId WHERE Id = @Id";
            _db.Execute(sqlQuery, model);
        }

        public bool IsBookExist(int id)
        {
            var order = _db.Query<OrderEntity>("SELECT * FROM Orders WHERE BookId = @id", new { id }).FirstOrDefault();

            return order != null;
        }
    }
}
