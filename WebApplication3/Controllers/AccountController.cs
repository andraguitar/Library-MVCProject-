using System.Web.Mvc;
using System.Web.Security;
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
    public class AccountController : Controller
    {
        [Inject]
        public IAccountService Service { get; set; }
        [Inject]
        public ILogService ServiceLog { get; set; }
        [Inject]
        public IGenericMapper MyMap { get; set; }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                bool checkUser= Service.CheckUser(model.Name, model.Password);
                if (checkUser)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    ServiceLog.Log("Info", null, "authorization was successful");
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "User with this login or password doesn't exist");
            }
            return View(model);
        }
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }

       

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            var modelDto = MyMap.Map<RegisterViewModel, UserEntity>(model);
            bool checkName = Service.CheckUserName(modelDto);
            if (checkName == false)
            {
                Service.Create(modelDto);
                FormsAuthentication.SetAuthCookie(modelDto.Name, true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "User already exist");
            }
            return View(model);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}