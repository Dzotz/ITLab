using ITLab.Classes.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace ITLabWeb.Controllers
{
    public class RowController : Controller
    {
        DatabaseManager inst = DatabaseManager.Instance;


        // GET: RowController/Create
        public ActionResult Create(string tabName)
        {
            ViewBag.tabName = tabName;
            return View();
        }

        // POST: RowController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string tabName, string values)
        {
            try
            {
                inst.AddRow(tabName, values);
                return RedirectToAction(nameof(Index), "Table", new { name = tabName});
            }
            catch
            {
                return View();
            }
        }

        // GET: RowController/Edit/5
        public ActionResult Edit(string tabName, int id)
        {
            ViewBag.tabName = tabName;
            ViewBag.id = id;
            return View();
        }

        // POST: RowController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string tabName, int rowId, string values)
        {
            try
            {
                inst.EditRow(tabName, values, rowId);
                return RedirectToAction(nameof(Index), "Table", new { name = tabName });
            }
            catch
            {
                return View();
            }
        }

        // GET: RowController/Delete/5
        public ActionResult Delete(string tabName, int id)
        {
            ViewBag.tabName = tabName;
            ViewBag.id = id;
            return View();
        }

        // POST: RowController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string tabName, int rowId)
        {
            try
            {
                inst.DeleteRow(tabName, rowId);
                return RedirectToAction(nameof(Index), "Table", new { name = tabName });
            }
            catch
            {
                return View();
            }
        }
    }
}
