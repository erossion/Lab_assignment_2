using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lab_10.Data;
using Lab_10.Models;
using Lab_10.ViewModels;
using PagedList;

namespace Lab_10.Controllers
{
    public class TasksController : Controller
    {
        private Lab_10Context db = new Lab_10Context();

        // GET: Tasks
        public ActionResult Index(string team, string status , string search, string sortBy, int? page)
        {
            TaskIndexViewModel viewModel = new TaskIndexViewModel();
            var tasks = db.Tasks.Include(t => t.Status).Include(t => t.Team);
            if (!String.IsNullOrEmpty(search))
            {
                tasks = tasks.Where(t => t.Name.Contains(search) || t.Description.Contains(search) || t.Team.Name.Contains(search) || t.Status.Name.Contains(search));
                viewModel.Search = search;
            }
            if (!String.IsNullOrEmpty(sortBy)) { }
            viewModel.CatsWithCount = from matchingTasks in tasks
                                      where
                                      matchingTasks.TeamID != null
                                      group matchingTasks by
                                      matchingTasks.Team.Name into
                                      catGroup
                                      select new TeamWithCount()
                                      {
                                          TeamName = catGroup.Key,
                                          TaskCount = catGroup.Count()
                                      };
            //var teams = tasks.OrderBy(p => p.Team.Name).Select(p=> p.Team.Name).Distinct();
            if (!String.IsNullOrEmpty(team))
            {
                tasks = tasks.Where(t => t.Team.Name == team);
                viewModel.Team = team;
            }
            switch (sortBy)
            {
                case "team_lowest":
                    tasks = tasks.OrderBy(p => p.Team.Name);
                    break;
                case "team_highest":
                    tasks = tasks.OrderByDescending(p => p.Team.Name);
                    break;
                case "status_lowest":
                    tasks = tasks.OrderBy(p => p.StatusID);
                    break;
                case "status_highest":
                    tasks = tasks.OrderByDescending(p => p.StatusID);
                    break;
                default:
                    tasks = tasks.OrderBy(p => p.Name);
                    break;
            }
            viewModel.SortBy = sortBy;
            viewModel.Sorts = new Dictionary<string, string>
             {
                 {"Team low to high", "team_lowest" },
                 {"Team high to low", "team_highest" },
                 {"Status low to high", "status_lowest" },
                 {"Status high to low", "status_highest" }
             };
            const int PageItems = 20;
            int currentPage = (page ?? 1);
            viewModel.Tasks = tasks.ToPagedList(currentPage, PageItems);
            return View(viewModel);
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tasks tasks = db.Tasks.Find(id);
            if (tasks == null)
            {
                return HttpNotFound();
            }
            return View(tasks);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            ViewBag.StatusID = new SelectList(db.Status, "ID", "Name");
            ViewBag.TeamID = new SelectList(db.Teams, "ID", "Name");
            return View();
        }

        // POST: Tasks/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,StatusID,TeamID")] Tasks tasks)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(tasks);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StatusID = new SelectList(db.Status, "ID", "Name", tasks.StatusID);
            ViewBag.TeamID = new SelectList(db.Teams, "ID", "Name", tasks.TeamID);
            return View(tasks);
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tasks tasks = db.Tasks.Find(id);
            if (tasks == null)
            {
                return HttpNotFound();
            }
            ViewBag.StatusID = new SelectList(db.Status, "ID", "Name", tasks.StatusID);
            ViewBag.TeamID = new SelectList(db.Teams, "ID", "Name", tasks.TeamID);
            return View(tasks);
        }

        // POST: Tasks/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,StatusID,TeamID")] Tasks tasks)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tasks).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StatusID = new SelectList(db.Status, "ID", "Name", tasks.StatusID);
            ViewBag.TeamID = new SelectList(db.Teams, "ID", "Name", tasks.TeamID);
            return View(tasks);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tasks tasks = db.Tasks.Find(id);
            if (tasks == null)
            {
                return HttpNotFound();
            }
            return View(tasks);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tasks tasks = db.Tasks.Find(id);
            db.Tasks.Remove(tasks);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
