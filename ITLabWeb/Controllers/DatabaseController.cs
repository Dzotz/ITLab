using ITLab.Classes.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITLabWeb.Controllers
{
    public class DatabaseController : Controller
    {
        // GET: DatabaseController
        public ActionResult Index()
        {
            if (DatabaseManager.Instance.Database == null)
            {
                ViewBag.errMsg = "No DB";
                ViewBag.Database = null;
            }
            else
            {
                ViewBag.errMsg = DatabaseManager.Instance.Database.Name;
                ViewBag.Database = DatabaseManager.Instance.Database;
            }
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: DatabaseController/Create/name
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string name)
        {
            if(name == null)
            {
                return View();  
            }
            try
            {
                DatabaseManager.Instance.CreateDatabase(name);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: DatabaseController/AddTable/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTable(string name)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DatabaseController/Delete
        public ActionResult Delete()
        {
            return View();
        }

        // POST: DatabaseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string name)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
