using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TODO_assignment.Models;

namespace TODO_assignment.Controllers
{
    public class TaskItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TaskItems
        public ActionResult Index()
        {
            return View();
        }

        private IEnumerable<TaskItem> GetTaskItems()
        {
            var id = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(o => o.Id == id);

            return db.TaskList.ToList().Where(o => o.User != null && o.User == currentUser);
        }

        public ActionResult ReloadTasks()
        {
            var items = GetTaskItems();
            return PartialView("_TasksView", items);
        }

        // GET: TaskItems/Details/5
        public ActionResult History(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var histItems = db.History.Where(o => o.TaskID == id).OrderBy(o => o.TimeStamp);
            if (histItems == null)
            {
                return HttpNotFound();
            }
            return View(histItems);
        }

        // GET: TaskItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaskItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaskID,Title,Completed")] TaskItem taskItem)
        {
            if (ModelState.IsValid)
            {
                string userID = User.Identity.GetUserId();
                var currentUser = db.Users.FirstOrDefault(id => id.Id == userID);
                taskItem.User = currentUser;

                if (currentUser == null)
                    return HttpNotFound();

                taskItem.TaskID = Guid.NewGuid();
                db.TaskList.Add(taskItem);

                TaskHistory hist = new TaskHistory
                {
                    TaskID = taskItem.TaskID,
                    TimeStamp = DateTime.Now,
                    Description = "Created"
                };

                db.History.Add(hist);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taskItem); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AJAXCreate([Bind(Include = "TaskID,Title")] TaskItem taskItem)
        {
            if (ModelState.IsValid)
            {
                string userID = User.Identity.GetUserId();
                var currentUser = db.Users.FirstOrDefault(i => i.Id == userID);
                taskItem.User = currentUser;

                taskItem.TaskID = Guid.NewGuid();
                taskItem.Completed = false;
                TaskHistory hist = new TaskHistory
                {
                    HistID = Guid.NewGuid(),
                    TaskID = taskItem.TaskID,
                    TimeStamp = DateTime.Now,
                    TaskItem = taskItem,
                    Description = "Created"
                };


                db.History.Add(hist);
                db.TaskList.Add(taskItem);
                db.SaveChanges();
            }

            return PartialView("_TasksView", GetTaskItems());
        }

        // GET: TaskItems/Edit/5
        public ActionResult Edit(Guid? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskItem taskItem = db.TaskList.Find(id);
            if (taskItem == null)
            {
                return HttpNotFound();
            }

            string userID = User.Identity.GetUserId();
            var currentUser = db.Users.FirstOrDefault(i => i.Id == userID);

            if (taskItem.User != currentUser)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(taskItem);
        }

        // POST: TaskItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaskID,Title,Completed")] TaskItem taskItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskItem).State = EntityState.Modified;
                TaskHistory hist = new TaskHistory
                {
                    HistID = Guid.NewGuid(),
                    TaskID = taskItem.TaskID,
                    TimeStamp = DateTime.Now,
                    TaskItem = taskItem,
                    Description = "Task modified"
                };
                db.History.Add(hist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taskItem);
        }

        [HttpPost] 
        public ActionResult AJAXEdit(Guid? id, bool value)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskItem taskItem = db.TaskList.Find(id);
            if (taskItem == null)
            {
                return HttpNotFound();
            }

            taskItem.Completed = value;
            db.Entry(taskItem).State = EntityState.Modified;
            TaskHistory hist = new TaskHistory
            {
                HistID = Guid.NewGuid(),
                TaskID = taskItem.TaskID,
                TimeStamp = DateTime.Now,
                TaskItem = taskItem,
                Description = "Task modified"
            };

            db.History.Add(hist);
            db.SaveChanges();


            return PartialView("_TasksView", GetTaskItems());
        }

        // GET: TaskItems/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskItem taskItem = db.TaskList.Find(id);
            if (taskItem == null)
            {
                return HttpNotFound();
            }
            return View(taskItem);
        }

        // POST: TaskItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            TaskItem taskItem = db.TaskList.Find(id);
            db.TaskList.Remove(taskItem);
            var hist = db.History.Where(o => o.TaskID == taskItem.TaskID);
            foreach (var item in hist)
                db.History.Remove(item);
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
