using AjaxClass.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AjaxClass.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult JsonRead()
        {
            return View();
        }
        public IActionResult Index()
        {
            return RedirectToAction("JsonRead");
        }
        public IActionResult memberList()
        {
            var db = new AjaxContext();
            var q = db.Members.ToList();
            return View(q);
        }
        public IActionResult ACreate()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult ACreate(bool Submit, string? Name, string? Email, int? Age)
        {
            var db = new AjaxContext();
            if (Submit)
            {
                db.Members.Add(new Member
                {
                    Name = Name,
                    Email = Email,
                    Age = Age
                });
                db.SaveChanges();
                return RedirectToAction("memberList");
            }
            if (string.IsNullOrEmpty(Name)) { return Json("錯誤:姓名未填寫"); }
            if (string.IsNullOrEmpty(Email)) { return Json("錯誤:信箱未填寫"); }
            if (!(bool)Submit ||Submit==null)
            {
                var q = db.Members.FirstOrDefault(m => m.Name == Name);
                if (q != null) { return Json("錯誤: 姓名已被使用"); }

                q = db.Members.FirstOrDefault(m => m.Email == Email);
                if (q != null) { return Json("錯誤: 信箱已被使用"); }
                return Json("資料可以使用");
            }
            return View();
        }
        public IActionResult Privacy()
        {
            var db = new AjaxContext();
            var q = (from c in db.Addresses
                    select c).Take(100);
            return View(q.ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}