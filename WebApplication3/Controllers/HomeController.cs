using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Entities;
using Dapper;
using Nemiro.OAuth;
using WebApplication3.Filters;

namespace WebApplication3.Controllers
{
    [PostActionFilter]
    public class HomeController : Controller
    {
        private readonly IDbConnection _db;

        public HomeController()
        {
           
        }

        public HomeController(IDbConnection db)
        {
            _db = db;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExternalLogin(string provider)
        {
            // build the return address
            string returnUrl = Url.Action("ExternalLoginResult", "Home", null, null, Request.Url.Host);
            // redirect user into external site for authorization
            return Redirect(OAuthWeb.GetAuthorizationUrl(provider, returnUrl));
        }
        public ActionResult ExternalLoginResult()
        {
            var result = OAuthWeb.VerifyAuthorization();
            var user = result.UserInfo;
            var newUser = _db.Query<UserEntity>("SELECT * FROM Users Where Id=(Select UserId From UsersExternal WHERE ExternalId = @UserId)", new { user.UserId }).FirstOrDefault();
            if (newUser == null)
            {
                var sqlQuery = "INSERT INTO Users (Email, Name, RoleId) VALUES(@Email, @FirstName, 2)";
                _db.Execute(sqlQuery, user);
                sqlQuery = "INSERT INTO UsersExternal(UserId, ExternalId, Provider) VALUES((SELECT Id From Users Where Name=@FirstName),@UserId, 'vk')";
                _db.Execute(sqlQuery, user);
                FormsAuthentication.SetAuthCookie(user.FirstName, true);
                return RedirectToAction("Index", "Home");
            }
            FormsAuthentication.SetAuthCookie(newUser.Name, true);
            return RedirectToAction("Index", "Home");
        }
    }
}