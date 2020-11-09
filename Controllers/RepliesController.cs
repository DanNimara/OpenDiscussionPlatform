using OpenDiscussionPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpenDiscussionPlatform.Controllers
{
    public class RepliesController : Controller
    {
        private Models.AppContext db = new Models.AppContext();

        // GET: Subjects
        public ActionResult Index()
        {
            return View();
        }

        // GET: Show
        public ActionResult Show()
        {
            return View();
        }

        //POST: New
        [HttpPost]
        public ActionResult New(int id, Reply reply)
        {
            reply.Date = DateTime.Now;
            reply.SubjectID = id;
            try
            {
                db.Replies.Add(reply);
                db.SaveChanges();
                TempData["message"] = "Comentariul a fost adaugat cu succes!";
                return Redirect("/Subjects/Show/" + reply.SubjectID);
            }
            catch (Exception)
            {
                return Redirect("/Subjects/Show/" + reply.SubjectID);
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
                    subject.Title = requestSubject.Title;
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