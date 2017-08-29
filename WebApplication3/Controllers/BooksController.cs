using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using BLL.Entities;
using BLL.Interfaces;
using BLL.LogFolder;
using BLL.MapFolder;
using Ninject;
using WebApplication3.Filters;
using WebApplication3.Hubs;
using WebApplication3.ViewModels;

namespace WebApplication3.Controllers
{
    [PostActionFilter]
    public class BooksController : Controller
    {
        [Inject]
        public IBookService Service { get; set; }
        [Inject]
        public IOrderService OrderService { get; set; }
        [Inject]
        public ILogService ServiceLog { get; set; }
        [Inject]
        public IGenericMapper MyMap { get; set; }
        // GET: Books
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Head = User.IsInRole("admin");
            var bookModel = Service.GetMany();
            var models = MyMap.Map<List<BookEntity>, IEnumerable<BookViewModel>>(bookModel);
            if (HttpContext.Request.UrlReferrer != null) ViewData["Ref"] = HttpContext.Request.UrlReferrer.AbsoluteUri;
            return View(models);
        }

        // GET: Books/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookEntity book = Service.Get((int)id);
            if (book == null)
            {
                return HttpNotFound();
            }
            var model = MyMap.Map<BookEntity, BookViewModel>(book);
            return View(model);
        }

        // GET: Books/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create(BookViewModel book)
        {
            if (ModelState.IsValid)
            {
                var bookDto = MyMap.Map<BookViewModel, BookEntity> (book);
                Service.Create(bookDto);
                int id = Service.GetBook(bookDto.Name).Id;
                AddBook(id);
                return RedirectToAction("Index");
            }
            return View(book);
        }

        private void AddBook(int id)
        {
            var context =
                Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.All.displayMessage(id);
        }

        private void RemoveBook(int id)
        {
            var context =
                Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.All.removeBook(id);
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var book = Service.Get((int)id);
            if (book == null)
            {
                return HttpNotFound();
            }
            var model = MyMap.Map<BookEntity, BookViewModel>(book);
            return View(model);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookViewModel book)
        {
            if (ModelState.IsValid)
            {
                var bookDto = MyMap.Map<BookViewModel, BookEntity>(book);
                Service.Update(bookDto);
                return RedirectToAction("Index");
            }
            return View(book);
            
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var book = Service.Get((int)id);
            if (book == null)
            {
                return HttpNotFound();
            }
            var test = MyMap.Map<BookEntity, BookViewModel>(book);
            return View(test);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var checkBook= OrderService.IsBookExist(id);
            if (checkBook == false)
            {
                Service.Delete(id);
                RemoveBook(id);
                return RedirectToAction("Index");
            }
            return Redirect("~/Content/RangeErrorPage.html");
        }

        [HttpGet]
        public ActionResult BookSearch()
        {
            var bookModel = Service.GetMany();
            var model = MyMap.Map<List<BookEntity>, List<BookViewModel> >(bookModel);
            return PartialView(model);
        }

    }
}
