using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using BLL.Entities;
using BLL.Interfaces;
using BLL.LogFolder;
using BLL.MapFolder;
using Hangfire;
using Ninject;
using WebApplication3.Filters;
using WebApplication3.ViewModels;


namespace WebApplication3.Controllers
{
    [PostActionFilter]
    public class OrdersController : Controller
    {
        [Inject]
        public IOrderService ServiceOrder { get; set; }

        [Inject]
        public IBookService ServiceBook { get; set; }

        [Inject]
        public IReaderService ServiceReader { get; set; }

        [Inject]
        public ILogService ServiceLog { get; set; }

        [Inject]
        public IGenericMapper MyMap { get; set; }

        [Inject]
        public IMessage SendService { get; set; }

        // GET: Readers
        public ActionResult Index()
        {
            var role = User.IsInRole("admin");
            ViewBag.Head = role;
            var ordermodel = ServiceOrder.GetMany();
            var models = MyMap.Map<List<OrderEntity>, IEnumerable<OrderViewModel>>(ordermodel);
           return View(models);
        }

        // GET: Readers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderEntity order = ServiceOrder.Get((int) id);
            if (order == null)
            {
                return HttpNotFound();
            }
            var model = MyMap.Map<OrderEntity, OrderViewModel>(order);
            return View(model);
        }

        // GET: Readers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Readers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                var orderDto = MyMap.Map<OrderViewModel, OrderEntity>(order);
                var book = ServiceBook.GetBook(orderDto.BookName);
                var reader = ServiceReader.GetReader(orderDto.ReaderName);
                if (book == null || reader == null)
                {
                    ModelState.AddModelError("", "this book or user doesn't exist");
                    ServiceLog.Log("Info", null, "Order is not created");
                }
                else
                {
                    if (ServiceOrder.IsBookExist(book.Id))
                    {
                        ModelState.AddModelError("", "this book is taken");
                    }
                    else
                    {
                        ServiceOrder.Create(orderDto);
                        var message = "Dear ," + orderDto.ReaderName + ", you took the book \""
                                      + orderDto.BookName + "\".\nPlease, return book until " + orderDto.DateDelivery;
                        var mail = ServiceReader.GetReader(orderDto.ReaderName);
                        BackgroundJob.Enqueue(() => SendService.Send(mail.Email, "Registration", message));
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(order);
        }
       
        // GET: Readers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var orders = ServiceOrder.Get((int) id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            var test = MyMap.Map<OrderEntity, OrderViewModel>(orders);
            return View(test);
        }

        // POST: Readers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderViewModel orders)
        {
            if (ModelState.IsValid)
            {
                var test = MyMap.Map<OrderViewModel, OrderEntity>(orders);
                ServiceOrder.Update(test);
                return RedirectToAction("Index");
            }
            return View(orders);
        }

        // GET: Readers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var orders = ServiceOrder.Get((int) id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            var model = MyMap.Map<OrderEntity, OrderViewModel>(orders);
            return View(model);
        }

        public ActionResult BookSearch(int id)
        {
            var bookList = ServiceOrder.GetBooksWhithReaderId(id);
            return PartialView(bookList);
        }

        // POST: Readers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceOrder.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
