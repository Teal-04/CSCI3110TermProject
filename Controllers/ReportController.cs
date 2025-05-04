using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace CSCI3110_Term_Project.Controllers
{
    public class ReportController : Controller
    {
        // GET: /Report
        public IActionResult Index()
        {
            // enforce login
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
                return RedirectToAction("Login", "Account");

            // pass current userId into JS
            ViewBag.UserId = userId.Value;
            return View();
        }
    }
}
