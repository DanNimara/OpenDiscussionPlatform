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
        private Models.AppContext db = new Models.AppContext();

        // GET: Subjects
        public ActionResult Index()
        {
            return View();
        }

        // GET: Show
        public ActionResult Show(int id)
        {
            Subject subject = db.Subjects.Find(id);
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

        //POST: New
        [HttpPost]
        public ActionResult New(Subject subject)
        {
            subject.Date = DateTime.Now;
            try
            {
                db.Subjects.Add(subject);
                db.SaveChanges();
                TempData["message"] = "Subiectul a fost adaugat cu succes!";
                return Redirect("/Categories/Show/" + subject.CategoryID);
            }
            catch (Exception)
            {
                return View(subject);
            }
        }


        public ActionResult Edit(int id)
        {
            Subject subject = db.Subjects.Find(id);

            return View(subject);
        }

        //PUT: Edit
        [HttpPut]
        public ActionResult Edit(int id, Subject requestSubject)
        {
            try
            {
                var subject = db.Subjects.Find(id);
                if (TryUpdateModel(subject))
                {
                    subject.Title= requestSubject.Title;
                    subject.Content = requestSubject.Content;
                    db.SaveChanges();
                    TempData["message"] = "Subiectul a fost modificat!";
                    return Redirect("/Categories/Show/" + subject.CategoryID);
                }

                return View(requestSubject);
            }
            catch (Exception)
            {
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
            TempData["message"] = "Subiectul a fost sters cu succes!";
            return Redirect("/Categories/Show/" + subject.CategoryID);
        }
    }
}