using Microsoft.AspNetCore.Mvc;
using Product_Inventory_ERP_Project.Areas.Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product_Inventory_ERP_Project.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    public class CategoryController : Controller
    {
        private readonly InventoryERPDbContext db = null;
        public CategoryController(InventoryERPDbContext db) { this.db = db; }

        //Index Action
        public IActionResult Index(int? typeid)
        {
            ViewBag.TypeId = typeid;
            ViewBag.ProductType = db.ProductType.ToList();            
            //var data = db.Category.Where(x => x.Product_Type_Id == typeid.Value && x.Is_Deleted == 0)
//                                  .Where(x=> x.Is_Deleted == 0)
  //                                .ToList();
            return View();
        }
        //Create Action
        public IActionResult Create()
        {
            ViewBag.ProductType = db.ProductType.ToList();
            return View();
        }
        //Create Post Action
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category._Key = RandomStringGenerator.RandomString(32);
                category.Is_Deleted = 0;
                db.Category.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //Checkbox Delete Action
        public IActionResult SelectionDelete(int[] ids)
        {
            foreach (int i in ids)
            {
                var exists = db.Category.FirstOrDefault(x => x.Id == i);
                if (exists != null)
                {
                    exists.Is_Deleted = 1;
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
