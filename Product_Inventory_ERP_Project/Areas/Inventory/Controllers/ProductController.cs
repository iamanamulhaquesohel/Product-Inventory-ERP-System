using Microsoft.AspNetCore.Mvc;
using Product_Inventory_ERP_Project.Areas.Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product_Inventory_ERP_Project.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    public class ProductController : Controller
    {
        private readonly InventoryERPDbContext db = null;
        public ProductController(InventoryERPDbContext db) { this.db = db; }

        //Index Action
        public IActionResult Index(int? typeid, int? categoryid)
        {
            ViewBag.TypeId = typeid;
            ViewBag.ProductType = db.ProductType.ToList();
            ViewBag.CategoryId = categoryid;
            ViewBag.Category = db.Category.ToList();
            return View();
        }

        //Create Action
        public IActionResult Create()
        {
            ViewBag.ProductType = db.ProductType.ToList();
            ViewBag.Category = db.Category.ToList();
            return View();
        }
        //Create Post Action
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product._Key = RandomStringGenerator.RandomString(32);
                product.Is_Deleted = 0;
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        //Delete Action
        public IActionResult Delete(int id)
        {
            var exists = db.Product.FirstOrDefault(x => x.Id == id);
            if (exists != null)
            {
                exists.Is_Deleted = 1;
            }
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
