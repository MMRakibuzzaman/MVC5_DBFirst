using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TeamsController : Controller
    {
        private EviTestDbContext db = new EviTestDbContext();

        public ActionResult Index()
        {
            return View(db.Teams.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeamId,TeamName")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(team);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Team team = db.Teams.Find(id);

            var relatedEntries = db.PlayerEntries.Where(x => x.TeamId == id).ToList();
            if (relatedEntries.Count > 0)
            {
                db.PlayerEntries.RemoveRange(relatedEntries);
            }

            db.Teams.Remove(team);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
