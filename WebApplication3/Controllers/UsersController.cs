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
    [Authorize]
    public class UsersController : Controller
    {
        [Inject]
        public IUserService ServiceUser { get; set; }

        [Inject]
        public ILogService ServiceLog { get; set; }

        [Inject]
        public IGenericMapper MyMap { get; set; }

        [Inject]
        public IMessage SendService { get; set; }

        // GET: Users
        public ActionResult Index()
        {
            var role = User.IsInRole("admin");
            ViewBag.Head = role;
            var usermodels = ServiceUser.GetMany();
            var viewModels = MyMap.Map<List<UserEntity>, IEnumerable<UserViewModel>>(usermodels);
            
            return View(viewModels);
        }

        //GET: Users/Details/5
        public ActionResult Details(int id)
        {
            UserEntity user = ServiceUser.Get(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var viewModel = MyMap.Map<UserEntity, UserViewModel>(user);
            
            return View(viewModel);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }
        
        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var checkModel = MyMap.Map<UserViewModel, UserEntity>(user);
                bool checkName = ServiceUser.CheckUserName(checkModel);
                if (checkName == false)
                {
                    ServiceUser.Create(checkModel);
                    var message = "Dear, " + checkModel.Name + ",you are registered.\n"
                                  + "Your password: " + checkModel.Password + "\nYour login: " + checkModel.Name;
                    BackgroundJob.Enqueue(() => SendService.Send(checkModel.Email, "Registration", message)); 

                    return RedirectToAction("Index", "Users");
                }
            }
            ModelState.AddModelError("", "User already exist");

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            var user = ServiceUser.Get(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var viewModel = MyMap.Map<UserEntity, UserViewModel>(user);

            return View(viewModel);
        }

        // POST: Users/Edit/5
        [HttpPost]
        public ActionResult Edit(UserViewModel user)
        {
            var viewModel = MyMap.Map<UserViewModel, UserEntity>(user);
            var countOfNames = ServiceUser.CountOfNames(viewModel);
            if (countOfNames == 0)
            {
                ServiceUser.Update(viewModel); 
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "User already exist");

            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var users = ServiceUser.Get((int)id);
            if (users == null)
            {
                return HttpNotFound();
            }
            var viewModel = MyMap.Map<UserEntity, UserViewModel>(users);

            return View(viewModel);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceUser.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
