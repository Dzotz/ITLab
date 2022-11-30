using ITLab.Classes.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITLabWeb.Controllers
{
    public class TableController : Controller
    {
        DatabaseManager inst = DatabaseManager.Instance;
        // GET: TableController
        public ActionResult Index(string name)
        {
            Table res = inst.Database.GetTable(name);
            if (res == null)
            {
                ViewBag.errMsg = "No Table";
                ViewBag.Table = new Table("");
            }
            else
            {
                ViewBag.errMsg = res.Name;
                ViewBag.Table = res;
            }
            return View();
        }

        public ActionResult MultiplyRes(string name1, string name2)
        {
            Table res = inst.Multiply(name1, name2);
            if (res == null)
            {
                ViewBag.errMsg = "No Table";
                ViewBag.Table = new Table("");
            }
            else
            {
                ViewBag.errMsg = res.Name;
                ViewBag.Table = res;
            }
            return View();
        }

        public ActionResult Multiply()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Multiply(string name1, string name2)
        {
            try
            {
                Table res = inst.Multiply(name1, name2);
                return RedirectToAction(nameof(MultiplyRes), new { name1=name1, name2=name2 });
            }
            catch
            {
                return View();
            }
        }

        // GET: TableController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TableController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TableController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string name)
        {
            try
            {
                inst.AddTable(name);
                return RedirectToAction(nameof(Index), "Database");
            }
            catch
            {
                return View();
            }
        }

        // GET: TableController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TableController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        public ActionResult Delete(string? name)
        {
            ViewBag.tabName = name;
            return View();
        }

        // POST: TableController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string name)
        {

            inst.DeleteTable(name);
             return RedirectToAction(nameof(Index), "Database");

            
        }
    }
}
