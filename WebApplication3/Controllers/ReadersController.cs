using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using BLL.Entities;
using BLL.Interfaces;
using BLL.LogFolder;
using BLL.MapFolder;
using Ninject;
using WebApplication3.Filters;
using WebApplication3.ViewModels;

namespace WebApplication3.Controllers
{
    [PostActionFilter]
    public class ReadersController : Controller
    {
        [Inject]
        public IReaderService ServiceReader { get; set; }

        [Inject]
        public ILogService ServiceLog { get; set; }

        [Inject]
        public IGenericMapper MyMap { get; set; }

        // GET: Readers
        public ActionResult Index()
        {
            var role = User.IsInRole("admin");
            ViewBag.Head = role;
            var readermodel = ServiceReader.GetMany();
            var models = MyMap.Map<List<ReaderEntity>, IEnumerable<ReaderViewModel>>(readermodel);
            return View(models);
        }

        // GET: Readers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReaderEntity reader = ServiceReader.Get((int)id);
            if (reader == null)
            {
                return HttpNotFound();
            }
            var test = Mapper.Map<ReaderEntity, ReaderViewModel>(reader);
            return View(test);
        }

        // GET: Readers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Readers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReaderViewModel readers)
        {
            if (ModelState.IsValid)
            {
                var model = MyMap.Map<ReaderViewModel, ReaderEntity>(readers);
                ServiceReader.Create(model);
                return RedirectToAction("Index");
            }
            return View(readers);
        }

        // GET: Readers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var readers = ServiceReader.Get((int)id);
            if (readers == null)
            {
                return HttpNotFound();
            }
            var model = MyMap.Map<ReaderEntity, ReaderViewModel>(readers);

            return View(model);
        }

        // POST: Readers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ReaderViewModel readers)
        {
            if (ModelState.IsValid)
            {
                var model = MyMap.Map<ReaderViewModel, ReaderEntity>(readers);
                ServiceReader.Update(model);
                return RedirectToAction("Index");
            }
            return View(readers);
        }

        // GET: Readers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var readers = ServiceReader.Get((int)id);
            if (readers == null)
            {
                return HttpNotFound();
            }
            var model = MyMap.Map<ReaderEntity, ReaderViewModel>(readers);
            return View(model);
        }

        // POST: Readers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceReader.Delete(id);
            return RedirectToAction("Index");
        }    
    }
}
