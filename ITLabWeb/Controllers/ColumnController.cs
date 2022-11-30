using ITLab.Classes.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITLabWeb.Controllers
{
    public class ColumnController : Controller
    {
        DatabaseManager inst = DatabaseManager.Instance;
        // GET: ColumnController/Create
        public ActionResult Create(string tabName)
        {
            ViewBag.tabName = tabName;
            return View();
        }

        // POST: ColumnController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string name, string type, string tabName)
        {
            try
            {
                inst.AddColumn(tabName, name, type);
                return RedirectToAction(nameof(Index), "Table", new {name = tabName});
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CreateSet(string tabName)
        {
            ViewBag.tabName = tabName;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSet(string name, string values, string tabName)
        {
            try
            {
                inst.AddSetColumn(tabName, name, values);
                return RedirectToAction(nameof(Index), "Table", new { name = tabName });
            }
            catch
            {
                return View();
            }
        }


        // GET: ColumnController/Delete/5
        public ActionResult Delete(string tabName, string name)
        {
            ViewBag.tabName = tabName;
            ViewBag.name = name;
            return View();
        }

        // POST: ColumnController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string tabName, string name) {
            inst.DeleteColumn(tabName, inst.Database.GetTable(tabName).Columns.FindIndex(x => x.Name == name));
            return RedirectToAction(nameof(Index), "Table", new { name = tabName });
        }
    }
}
