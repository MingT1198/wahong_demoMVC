using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using wahong_demoMVC.DB;
using wahong_demoMVC.Models;
using wahong_demoMVC.VModels;

namespace wahong_demoMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly userDB _db;

        public HomeController(ILogger<HomeController> logger, userDB db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index(int? id)
        {
            UserVModel vmodel = new();
            if (id != null)
            {
                vmodel.User = _db.DB.SingleOrDefault(x => x.Id == id);
            }
            vmodel.Users = _db.DB;
            return View(vmodel);
        }

        [HttpPost]
        public IActionResult Index(UserModel user)
        {
            var _user = _db.DB.SingleOrDefault(x => x.Id == user.Id);

            if (_user == null)
            {
                int id = 0;
                if (_db.DB.Count > 0)
                {
                    id = _db.DB.Max(x => x.Id);
                }
                user.Id = ++id;

                _db.DB.Add(user);
            }
            else
            {
                _user.Age = user.Age;
                _user.Birthday = user.Birthday;
                _user.UserName = user.UserName;
            }
            return Redirect("~/home/Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var user = _db.DB.SingleOrDefault(u => u.Id == id);
            if (user != null)
            {
                _db.DB.Remove(user);
            }
            return Redirect("~/home/Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}