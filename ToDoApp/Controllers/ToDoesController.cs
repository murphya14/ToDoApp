using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ToDoApp.Models;
using Microsoft.AspNet.Identity; //Created

namespace ToDoApp.Controllers
{
    public class ToDoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ToDoes
        
        public ActionResult Index()
        {            
            return View();   ///Only return the views for the User and it is Index.cshtml it automatically returns as it is the name of the action
        }

        private IEnumerable<ToDo> GetMyToDos() ///The point of this is you are only looking at the users Todos
        {
            string CurrentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == CurrentUserId);

            IEnumerable<ToDo> myToDoes = db.ToDos.ToList().Where(x => x.User == currentUser); ///made for % bar

            int completeCount = 0; 
            foreach(ToDo toDo in myToDoes) ///This initailizes the list 
            {
                if(toDo.IsDone)
                {
                    completeCount++;
                }
            }

            ViewBag.Percent = Math.Round(100f * ((float)completeCount / (float)myToDoes.Count()));

            return myToDoes;
        }

        public ActionResult BuildToDoTable()
        {
            return PartialView("_ToDoTable", GetMyToDos()); ///Only change here is directing the partial view
        }

        // GET: ToDoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDo toDo = db.ToDos.Find(id);
            if (toDo == null)
            {
                return HttpNotFound();
            }
            return View(toDo);
        }

        // GET: ToDoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,IsDone")] ToDo toDo)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId(); //created
                ApplicationUser currentUser = db.Users.FirstOrDefault //This is the query to the database
                    (x => x.Id == currentUserId); //lambda to query. So, it means where the User(object) Id = currentUser Id
                toDo.User = currentUser; //created
                db.ToDos.Add(toDo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(toDo);
        }


        [HttpPost] ///The model Binding inserts the properties 
        [ValidateAntiForgeryToken]
        public ActionResult AJAXCreate([Bind(Include = "Id,Description")] ToDo toDo)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId(); //created
                ApplicationUser currentUser = db.Users.FirstOrDefault //This is the query to the database
                    (x => x.Id == currentUserId); //lambda to query. So, it means where the User(object) Id = currentUser Id
                toDo.User = currentUser; //created
                toDo.IsDone = false; ///so take out Done as we are creating
                db.ToDos.Add(toDo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return PartialView("_ToDoTable", GetMyToDos());
        }

        // GET: ToDoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDo toDo = db.ToDos.Find(id);
            if (toDo == null)
            {
                return HttpNotFound();
            }
            string CurrentUserId = User.Identity.GetUserId(); ///Added in so if you are not the current user you cannot edit the ToDo list 
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == CurrentUserId);

            if(toDo.User != currentUser)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(toDo);
        }

        // POST: ToDoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,IsDone")] ToDo toDo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(toDo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(toDo);
        }

        [HttpPost]
        
        public ActionResult AJAXEdit(int? id, bool value)
            //For the IsDone
            // The arguments - the 1st shows the programme what the user is changing.
            //The value set to isDone
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDo toDo = db.ToDos.Find(id);
            if (toDo == null)
            {
                return HttpNotFound();
            }else
            {
                toDo.IsDone = value; //isDone has an Id so has a value 
                db.Entry(toDo).State = EntityState.Modified; //Copied from above
                db.SaveChanges();
                return PartialView("_toDoTable", GetMyToDos()); //Within the function because otherwise there will be the HTTPNotFound
            }
          
        }

        // GET: ToDoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDo toDo = db.ToDos.Find(id);
            if (toDo == null)
            {
                return HttpNotFound();
            }
            return View(toDo);
        }

        // POST: ToDoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ToDo toDo = db.ToDos.Find(id);
            db.ToDos.Remove(toDo);
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
