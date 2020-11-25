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


        // POST: New
        [HttpPost]
        public ActionResult New(int id, Reply reply)
        {
            reply.SubjectID = id;
            reply.Date = DateTime.Now;
            try
            {
                if (ModelState.IsValid)
                {
                    db.Replies.Add(reply);
                    db.SaveChanges();
                    TempData["message"] = "Raspunsul a fost adaugat!";
                    return Redirect("/Subjects/Show/" + reply.SubjectID);
                }
                else
                {
                    return Redirect("/Subjects/Show/" + reply.SubjectID);
                }
            }
            catch (Exception)
            {
                return Redirect("/Subjects/Show/" + reply.SubjectID);
            }
        }


        // GET: Edit
        public ActionResult Edit(int id)
        {
            Reply reply = db.Replies.Find(id);
            return View(reply);
        }

        // PUT: Edit
        [HttpPut]
        public ActionResult Edit(int id, Reply requestReply)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var reply = db.Replies.Find(id);
                    if (TryUpdateModel(reply))
                    {
                        reply.Content = requestReply.Content;
                        db.SaveChanges();
                        TempData["message"] = "Raspunsul a fost modificat!";
                        return Redirect("/Subjects/Show/" + reply.SubjectID);
                    }
                    else
                    {
                        return View(requestReply);
                    }
                }
                else
                {
                    return View(requestReply);
                }
            }
            catch (Exception)
            {
                return View(requestReply);
            }
        }


        // DELETE: Delete
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var reply = db.Replies.Find(id);
            db.Replies.Remove(reply);
            db.SaveChanges();
            TempData["message"] = "Raspunsul a fost sters!";
            return Redirect("/Subjects/Show/" + reply.SubjectID);

        }
    }
}
