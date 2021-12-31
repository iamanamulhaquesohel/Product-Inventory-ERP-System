using Microsoft.AspNetCore.Mvc;
using Product_Inventory_ERP_Project.Areas.Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product_Inventory_ERP_Project.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    public class ProductTypeController : Controller
    {
        private readonly InventoryERPDbContext db = null;
        public ProductTypeController(InventoryERPDbContext db) { this.db = db; }

        //Index Action
        public IActionResult Index(string search = "")
        {
            ViewBag.Search = search;
            if (!string.IsNullOrEmpty(search))
            {
                var searchData = db.ProductType
                    .Where(x => x.Name.ToLower().StartsWith(search.ToLower()))
                    .Where(pr => pr.Is_Deleted == 0)
                    .ToList();
                return View(searchData);
            }
            else
            {
                return View(db.ProductType.Where(pr => pr.Is_Deleted == 0).ToList());
            }
        }
        //Create Action 
        public IActionResult Create()
        {
            return View();
        }
        //Create Post Action
        [HttpPost]
        public IActionResult Create(ProductType productType)
        {
            if (ModelState.IsValid)
            {
                productType._Key = RandomStringGenerator.RandomString(32);
                productType.Is_Deleted = 0;
                db.ProductType.Add(productType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productType);
        }
        //Checkbox Delete Action
        public IActionResult SelectionDelete(int[] ids)
        {
            foreach (int i in ids)
            {
                var exists = db.ProductType.FirstOrDefault(x => x.Id == i);
                if (exists !=null)
                {
                    exists.Is_Deleted = 1;
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Delete Action
        public IActionResult Delete(int id)
        {
            var exists = db.ProductType.FirstOrDefault(x => x.Id == id);
            if (exists != null)
            {
                exists.Is_Deleted = 1;
            }
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        //Edit Action
        public IActionResult Edit(int id, string _Key)
        {
            var data = db.ProductType.FirstOrDefault(x => x.Id == id);
            if (data == null)
            {
                return NotFound("Data Not Found");
            }
            else 
            {
                if (data._Key != _Key)
                {
                    return BadRequest();
                }
            }
            return View(data);
        }
        //Edit Post Action
        [HttpPost]
        public IActionResult Edit(ProductType productType)
        {
            if (ModelState.IsValid)
            {
                var data = db.ProductType.FirstOrDefault(x => x.Id == productType.Id);
                if (data == null)
                {
                    return NotFound("Data Not Found");
                }
                else
                {
                    if (data.Id != productType.Id)
                    {
                        return BadRequest();
                    }
                    else
                    {
                        if (data._Key != productType._Key)
                        {
                            return BadRequest();
                        }
                    }
                }
                db.Entry(productType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productType);
        }

    }
    
}
