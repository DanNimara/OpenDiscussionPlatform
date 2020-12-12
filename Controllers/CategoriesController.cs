using Microsoft.AspNet.Identity;
using OpenDiscussionPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpenDiscussionPlatform.Controllers
{
    public class CategoriesController : Controller
    {
        private Models.ApplicationDbContext db = new Models.ApplicationDbContext();

        private int _perPage = 4;

        // GET: Subjects
        public ActionResult Index()
        {
            var categories = db.Categories;
            ViewBag.Categories = categories;
            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
            }
            SetAccessRights();
            return View();
        }


        // GET: Show
        public ActionResult Show(int id)
        {
            Category category = db.Categories.Find(id);
            var subjects = category.Subjects;
            var totalItems = subjects.Count();
            var currentPage = Convert.ToInt32(Request.Params.Get("page"));

            var offset = 0;

            if (!currentPage.Equals(0))
            {
                offset = (currentPage - 1) * this._perPage;
            }

            var paginatedSubjects= subjects.Skip(offset).Take(this._perPage);

            if (TempData.ContainsKey("message"))
            {
                ViewBag.message = TempData["message"];
            }

            ViewBag.lastPage = Math.Ceiling((float)totalItems / (float)this._perPage);
            ViewBag.Subjects = paginatedSubjects;

            SetAccessRights();
            return View(category);
        }


        // GET: New
        [Authorize(Roles = "Admin")]
        public ActionResult New()
        {
            Category category = new Category();
            return View(category);
        }

        // POST: New
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult New(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Categories.Add(category);
                    db.SaveChanges();
                    TempData["message"] = "Categoria a fost adaugata!";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(category);
                }
            }
            catch (Exception)
            {
                return View(category);
            }
        }


        // GET: Edit
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            Category category = db.Categories.Find(id);
            return View(category);
        }

        // PUT: Edit
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public ActionResult Edit(int id, Category requestCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var category = db.Categories.Find(id);
                    if (TryUpdateModel(category))
                    {
                        category.Name = requestCategory.Name;
                        category.Description = requestCategory.Description;
                        db.SaveChanges();
                        TempData["message"] = "Categoria a fost editata!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(requestCategory);
                    }
                }
                else
                {
                    return View(requestCategory);
                }
            }
            catch (Exception)
            {
                return View(requestCategory);
            }
        }


        //DELETE: Delete
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            TempData["message"] = "Categoria a fost stearsa!";
            return RedirectToAction("Index");
        }


        #region Helpers
        private void SetAccessRights()
        {
            ViewBag.isUser = User.IsInRole("User");
            ViewBag.isModerator = User.IsInRole("Moderator");
            ViewBag.isAdmin = User.IsInRole("Admin");
            ViewBag.currentUser = User.Identity.GetUserId();
        }
        #endregion
    }
}
