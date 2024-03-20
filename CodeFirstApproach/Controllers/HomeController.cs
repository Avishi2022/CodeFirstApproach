using CodeFirstApproach.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CodeFirstApproach.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentDBContext studentDB;

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        public HomeController(StudentDBContext studentDB)
        {
            this.studentDB = studentDB;
        }
        public IActionResult Index()
        {
            var stdData = studentDB.Students.ToList();
            return View(stdData);
        }
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Student std)
        {
            if (ModelState.IsValid)
            {
                studentDB.Students.Add(std);
                studentDB.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(std);
        }

        public IActionResult Edit(int? id)
        {

            if (id == null || studentDB.Students == null)
            {
                return NotFound();
            }
            var stdData = studentDB.Students.Find(id);
            if (stdData == null)
            {
                return NotFound();
            }
            return View(stdData);
        }

        [HttpPost]
        public IActionResult Edit(int? id, Student std)
        {
            if (id != std.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                studentDB.Students.Update(std);
                studentDB.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || studentDB.Students == null)
            {
                return NotFound();
            }
            var stdData = studentDB.Students.FirstOrDefault(x => x.Id == id);
            if (stdData == null)
            {
                return NotFound();
            }
            return View(stdData);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int? id)
        {

            var stdData = studentDB.Students.Find(id);
            if (stdData != null)
            {
                studentDB.Students.Remove(stdData);
            }
            studentDB.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
