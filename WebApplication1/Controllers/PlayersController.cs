using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;
using System.Data.Entity;

namespace WebApplication1.Controllers
{
    public class PlayersController : Controller
    {
        EviTestDbContext db = new EviTestDbContext();
        public ActionResult Index()
        {
            var pl = db.Players.Include(x => x.PlayerEntries.Select(p => p.Team)).OrderByDescending(x => x.PlayerId).ToList();
            return View(pl);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult AddNewTeam(int? id)
        {
            ViewBag.teams = new SelectList(db.Teams.ToList(), "TeamId", "TeamName", (id != null) ? id.ToString() : "");
            return PartialView("_addNewTeam");
        }
        [HttpPost]
        public ActionResult Create(PlayerVM playerVM, int[] teamId)
        {
            if (ModelState.IsValid)
            {
                Player player = new Player()
                {
                    PlayerName = playerVM.PlayerName,
                    BirthDate = playerVM.BirthDate,
                    Age = playerVM.Age,
                    IsCaptain = playerVM.IsCaptain,
                };

                HttpPostedFileBase file = playerVM.PictureFile;
                if (file != null)
                {
                    string filePath=Path.Combine("/Images/",DateTime.Now.Ticks.ToString()+Path.GetExtension(file.FileName));
                    file.SaveAs(Server.MapPath(filePath));
                    player.Picture = filePath;
                }

                foreach (var item in teamId)
                {
                    PlayerEntry playerEntry = new PlayerEntry()
                    {
                        Player = player,
                        PlayerId = player.PlayerId,
                        TeamId = item
                    };
                    db.PlayerEntries.Add(playerEntry);
                }
                db.SaveChanges();
                return PartialView("_success");
            }
            return PartialView("_error");
        }
        public ActionResult Edit(int? id)
        {
            Player player = db.Players.FirstOrDefault(x => x.PlayerId == id);
            var playerTeam = db.PlayerEntries.Where(x => x.PlayerId == id).ToList();
            PlayerVM playerVM = new PlayerVM()
            {
                PlayerId = player.PlayerId,
                PlayerName = player.PlayerName,
                BirthDate = player.BirthDate,
                Age = player.Age,
                Picture = player.Picture,
                IsCaptain = player.IsCaptain,
            };

            if (playerTeam.Count > 0)
            {
                foreach (var item in playerTeam)
                {
                    playerVM.TeamList.Add(item.TeamId);
                }
            }
            return View(playerVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PlayerVM playerVM, int[] teamId)
        {
            if (ModelState.IsValid)
            {
                Player player = new Player()
                {
                    PlayerId = playerVM.PlayerId,
                    PlayerName = playerVM.PlayerName,
                    BirthDate = playerVM.BirthDate,
                    Age = playerVM.Age,
                    Picture = playerVM.Picture,
                    IsCaptain = playerVM.IsCaptain,
                };

                HttpPostedFileBase file = playerVM.PictureFile;
                var oldPic = playerVM.Picture;

                if (file != null)
                {
                    string filePath = Path.Combine("/Images/", DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName));
                    file.SaveAs(Server.MapPath(filePath));
                    player.Picture = filePath;
                }
                else
                {
                    player.Picture = oldPic;
                }

                var teamEntry = db.PlayerEntries.Where(x => x.PlayerId == player.PlayerId).ToList();
                foreach (var playerEntry in teamEntry)
                {
                    db.PlayerEntries.Remove(playerEntry);
                }

                if (teamId != null)
                {
                    foreach (var item in teamId)
                    {
                        PlayerEntry playerEntry = new PlayerEntry()
                        {
                            PlayerId = player.PlayerId,
                            TeamId = item
                        };
                        db.PlayerEntries.Add(playerEntry);
                    }
                }

                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_success");
            }

            return PartialView("_error");
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Player player = db.Players.Find(id);

            if (!string.IsNullOrEmpty(player.Picture))
            {
                string fullPath = Server.MapPath(player.Picture);

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }

            var teamEntry = db.PlayerEntries.Where(x => x.PlayerId == player.PlayerId).ToList();
            foreach (var playerEntry in teamEntry)
            {
                db.PlayerEntries.Remove(playerEntry);
            }

            db.Players.Remove(player);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}