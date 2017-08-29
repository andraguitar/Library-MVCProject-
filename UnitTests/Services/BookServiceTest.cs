using System.Collections.Generic;
using System.Linq;
using BLL.IRepo;
using BLL.Services;
using DAL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApplication3.ViewModels;

namespace UnitTests.Services
{
    [TestClass()]
    public class BookServiceTest
    {
        public  List<Books> books = new List<Books>
        {
            new Books() { Name = "dfgd", Author = "dfgd", Id = 1, Cost = 5.6M },
            new Books() { Name = "dfsgd", Author = "sgsgd", Id = 2, Cost = 5.6M },
            new Books() { Name = "hgdh", Author = "dhgh", Id = 3, Cost = 5.6M }
        };
        
        //[TestMethod()]
        //public void CreateTest()
        //{
        //    BookViewModel booksModel = new BookViewModel { Name = "sdfds", Id = 8, Author = "dffs", Cost = 3.3M };
        //    var mock = new Mock<IRepository<Books>>();
        //    var bookService = new BookService(mock.Object);
        //    //bookService.Update(booksModel);
        //    //mock.Verify(lw => lw.Update(It.IsAny<Books>()));
            
        //}

        //[TestMethod()]
        //public void DeleteTest()
        //{
        //    var mock = new Mock<IRepository<Books>>();
        //    var bookService = new BookService(mock.Object);
        //    bookService.Delete(1);
        //    mock.Verify(lw => lw.Delete(It.IsAny<int>()));
        //}

        //[TestMethod()]
        //public void GetTest()
        //{
        //    var mock = new Mock<IRepository<Books>>();
        //    mock.Setup(mr => mr.Get(It.IsAny<int>())).Returns((int i) => books.Single(x => x.Id == i));
        //    var bookService = new BookService(mock.Object);
        //    var book = bookService.Get(3);
        //    Assert.IsNotNull(book);
        //    Assert.IsInstanceOfType(book, typeof(BookViewModel));
        //    Assert.AreEqual(book.Name, "hgdh");
        // }

        //[TestMethod()]
        //public void GetManyTest()
        //{
        //    var mock = new Mock<IRepository<Books>>();
        //    mock.Setup(m => m.GetMany()).Returns(books);
        //    var bookService = new BookService(mock.Object);
        //    List<BookViewModel> models = bookService.GetMany();
        //    Assert.AreEqual(3, models.Count); 
        //    Assert.AreEqual(models[0].Name, "dfgd");
        //}

        [TestMethod()]
        public void UpdateTest()
        {
        //   Assert.Fail();
        }
    }
}