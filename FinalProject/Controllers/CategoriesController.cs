using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Core;
using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public ActionResult Index()
        {
            return View(categoryRepository.GetAll().ToList());
        }

        #region Create
        public ActionResult Create()
        {
            return View("CatForm");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {

            if (ModelState.IsValid)
            {
                categoryRepository.Add(category);
                TempData["Message"] = "Category added successfully";
                return RedirectToAction(nameof(Index));
            }
            return View("CatForm");

        }
        #endregion

        #region Edit
        public IActionResult Edit(int id)
        {
            Category cat = categoryRepository.GetById(id);
            if (cat == null)
            {
                ErrorViewModel ErrorVM = new ErrorViewModel
                {
                    RequestId = "Category not found"
                };
                return View("Error", ErrorVM);
            }
            return View("CatForm", cat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id, Category cat)
        {
            if (id != cat.Id)
            {
                NotFound();
            }
            if (ModelState.IsValid)
            {
                categoryRepository.Update(cat);
                TempData["Message"] = "Category edited successfully";

                return RedirectToAction(nameof(Index));
            }
            return View("CatForm");
        }

        #endregion

        #region Delete
        public IActionResult Delete(int id)
        {
            Category cat = categoryRepository.GetById(id);
            if (cat == null)
            {
                ErrorViewModel ErrorVM = new ErrorViewModel
                {
                    RequestId = "Category not found"
                };
                return View("Error", ErrorVM);
            }
            categoryRepository.Delete(cat);
            TempData["Message"] = "Category deleted successfully";

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Details

        //Display the cataggory and assoictaed products. will make view later
        #endregion
    }
}