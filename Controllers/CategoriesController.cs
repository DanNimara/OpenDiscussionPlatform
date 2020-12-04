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


        // GET: Subjects
        public ActionResult Index()
        {
            var categories = db.Categories;
            ViewBag.Categories = categories;
            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
            }
            return View();
        }


        // GET: Show
        public ActionResult Show(int id)
        {
            Category category = db.Categories.Find(id);
            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
            }
            return View(category);
        }


        // GET: New
        public ActionResult New()
        {
            Category category = new Category();
            return View(category);
        }

        // POST: New
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
        public ActionResult Edit(int id)
        {
            Category category = db.Categories.Find(id);
            return View(category);
        }

        // PUT: Edit
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
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            TempData["message"] = "Categoria a fost stearsa!";
            return RedirectToAction("Index");
        }
    }
}
