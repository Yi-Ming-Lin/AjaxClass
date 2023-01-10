using AjaxClass.Models;
using Microsoft.AspNetCore.Mvc;

namespace AjaxClass.Views.Home
{
    public class fetchController : Controller
    {
        AjaxContext db = new AjaxContext();
        public IActionResult Index()
        {
            
            return View();
        }
        public IActionResult city()
        {
            return Json(db.Addresses.Select(x=>x.City).Distinct());
        }
        public IActionResult state(string city)
        {
            return Json(db.Addresses.Where(x => x.City == city).Select(x => x.SiteId).Distinct());
        }
        public IActionResult road(string state)
        {
            return Json(db.Addresses.Where(x => x.SiteId == state).Select(x => x.Road).Distinct());
        }
    }
}
