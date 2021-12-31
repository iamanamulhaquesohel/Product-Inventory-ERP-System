using Microsoft.AspNetCore.Mvc;
using Product_Inventory_ERP_Project.Areas.Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product_Inventory_ERP_Project.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    public class HomeController : Controller
    {
        private readonly InventoryERPDbContext db = null;
        public HomeController(InventoryERPDbContext db) { this.db = db; }

        public IActionResult Index()
        {
            return View();
        }

       
    }
}
