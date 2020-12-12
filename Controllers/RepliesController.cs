using Microsoft.AspNet.Identity;
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
        private Models.ApplicationDbContext db = new Models.ApplicationDbContext();


        // GET: Subjects
        public ActionResult Index()
        {
            return View();
        }


        [Authorize(Roles = "User, Moderator, Admin")]
        // GET: Edit
        public ActionResult Edit(int id)
        {
            Reply reply = db.Replies.Find(id);
            if (reply.UserID == User.Identity.GetUserId())
            {
                return View(reply);
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa editati acest raspuns!";
                return Redirect("/Subjects/Show/" + reply.SubjectID);
            }
        }

        [Authorize(Roles = "User, Moderator, Admin")]
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

        [Authorize(Roles = "User, Moderator, Admin")]
        // DELETE: Delete
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var reply = db.Replies.Find(id);
           
            if (reply.UserID == User.Identity.GetUserId() || User.IsInRole("Moderator") || User.IsInRole("Admin"))
            {
                db.Replies.Remove(reply);
                db.SaveChanges();
                TempData["message"] = "Raspunsul a fost sters!";
                return Redirect("/Subjects/Show/" + reply.SubjectID);
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa stergeti acest raspuns!";
                return Redirect("/Subjects/Show/" + reply.SubjectID);
            }

        }
    }
}
