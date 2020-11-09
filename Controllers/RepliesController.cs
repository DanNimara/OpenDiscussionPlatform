using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpenDiscussionPlatform.Controllers
{
    public class RepliesController : Controller
    {
        // GET: Replies
        public ActionResult Index()
        {
            return View();
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
                var reply = db.Replies.Find(id);
                if (TryUpdateModel(reply))
                {
                    reply.Content = requestReply.Content;
                    db.SaveChanges();
                    TempData["message"] = "Raspunsul a fost modificat!";
                    return Redirect("/Subjects/Show/" + reply.SubjectID);
                }

                return View(requestReply);
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
            TempData["message"] = "Raspunsul a fost sters cu succes!";
            return Redirect("/Subjects/Show/" + reply.SubjectID);
        }
    }
}