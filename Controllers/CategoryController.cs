using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.Data;
using MoviesApp.Models;


namespace MoviesApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = _db.Categories;
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            //Check if category name equals display order
            if(category.Name == category.DisplayOrder.ToString()){
                ModelState.AddModelError("name", "The display order cannot exactly match the name");
            }

            if(ModelState.IsValid){
            //Check if the category is already added
            var existingCategory = _db.Categories.FirstOrDefault(c => c.Name == category.Name);

            if(existingCategory != null){
                ModelState.AddModelError("name", "A category with the same name already exists");
                return View(category);
            }
            _db.Categories.Add(category);
            _db.SaveChanges();
            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index");

            }

            return View(category);
           
        }

         public IActionResult Edit(int? id)
        {  
            if(id == null || id.Value == 0) {
                return NotFound();
            }
            var category = _db.Categories.Find(id); 

            if(category == null) {
                return NotFound();
            }

            return View(category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            //Check if category name equals display order
            if(category.Name == category.DisplayOrder.ToString()){
                ModelState.AddModelError("name", "The display order cannot exactly match the name");
            }

            if(ModelState.IsValid){
            //Check if the category is already added
            var existingCategory = _db.Categories.FirstOrDefault(c => c.Name == category.Name);

            if(existingCategory != null){
                ModelState.AddModelError("name", "A category with the same name already exists");
                return View(category);
            }
            _db.Categories.Update(category);
            _db.SaveChanges();
            TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index");

            }

            return View(category);
           
        }

        public IActionResult Delete(int? id)
        {
            if(id == null || id.Value == 0) {
                return NotFound();
            }
            var category = _db.Categories.Find(id);

            if(category == null){
                return NotFound();
            }
             _db.Categories.Remove(category);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }


    }
}

