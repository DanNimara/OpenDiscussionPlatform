using OpenDiscussionPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpenDiscussionPlatform.Controllers
{
    public class SubjectsController : Controller
    {
        private Models.ApplicationDbContext db = new Models.ApplicationDbContext();


        // GET: Subjects
        public ActionResult Index()
        {
            return View();
        }


        // GET: Show
        public ActionResult Show(int id)
        {
            Subject subject = db.Subjects.Find(id);
            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
            }
            return View(subject);
        }


        // GET: New
        public ActionResult New(int id)
        {
            Subject subject = new Subject();
            subject.CategoryID = id;
            var category = db.Categories.Find(id);
            ViewBag.CategoryName = category.Name;
            return View(subject);
        }

        // POST: New
        [HttpPost]
        public ActionResult New(Subject subject)
        {
            subject.Date = DateTime.Now;
            try
            {
                if (ModelState.IsValid)
                {
                    db.Subjects.Add(subject);
                    db.SaveChanges();
                    TempData["message"] = "Subiectul a fost adaugat!";
                    return Redirect("/Categories/Show/" + subject.CategoryID);
                }
                else
                {
                    var category = db.Categories.Find(subject.CategoryID);
                    ViewBag.CategoryName = category.Name;
                    return View(subject);
                }
            }
            catch (Exception)
            {
                var category = db.Categories.Find(subject.CategoryID);
                ViewBag.CategoryName = category.Name;
                return View(subject);
            }
        }


        // GET: Edit
        public ActionResult Edit(int id)
        {
            Subject subject = db.Subjects.Find(id);
            var category = db.Categories.Find(subject.CategoryID);
            ViewBag.CategoryName = category.Name;
            return View(subject);
        }

        //PUT: Edit
        [HttpPut]
        public ActionResult Edit(int id, Subject requestSubject)
        {
            try
            {
                if (ModelState.IsValid) {
                    var subject = db.Subjects.Find(id);
                    if (TryUpdateModel(subject))
                    {
                        subject.Title = requestSubject.Title;
                        subject.Content = requestSubject.Content;
                        db.SaveChanges();
                        TempData["message"] = "Subiectul a fost modificat!";
                        return Redirect("/Categories/Show/" + subject.CategoryID);
                    }
                }
                var category = db.Categories.Find(requestSubject.CategoryID);
                ViewBag.CategoryName = category.Name;
                return View(requestSubject);
            }
            catch (Exception)
            {
                var category = db.Categories.Find(requestSubject.CategoryID);
                ViewBag.CategoryName = category.Name;
                return View(requestSubject);
            }
        }


        //DELETE: Delete
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var subject = db.Subjects.Find(id);
            db.Subjects.Remove(subject);
            db.SaveChanges();
            TempData["message"] = "Subiectul a fost sters!";
            return Redirect("/Categories/Show/" + subject.CategoryID);
        }
    }
}
