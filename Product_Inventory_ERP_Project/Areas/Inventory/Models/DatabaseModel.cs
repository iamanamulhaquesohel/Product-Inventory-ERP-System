using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Product_Inventory_ERP_Project.Areas.Inventory.Models
{
    public class ProductType
    {
        public ProductType()
        {
            this.Category = new List<Category>();
        }
        [Display(Name ="Product Type Name")]
        public int Id { get; set; }
        [Required, StringLength(40), Display(Name = "Product Type Name")]
        public string Name { get; set; }
        [Required, StringLength(150),]
        public string Description { get; set; }
        [StringLength(32)]
        public string _Key { get; set; }
        [Column(TypeName = "tinyint")]
        public int Is_Deleted { get; set; }
        public ICollection<Category> Category { get; set; }
    }
    public class Category
    {
        public Category()
        {
            this.Product = new List<Product>();
        }
        [Display(Name = "Category Name")]
        public int Id { get; set; }
        [Required, StringLength(40), Display(Name = "Category Name")]
        public string Name { get; set; }
        [Required, StringLength(150),]
        public string Description { get; set; }
        [StringLength(32)]
        public string _Key { get; set; }
        [Column(TypeName = "tinyint")]
        public int Is_Deleted { get; set; }
        [Required, Display(Name = "Product Type Name")]
        public int Product_Type_Id { get; set; }
        [ForeignKey("Product_Type_Id")]
        public virtual ProductType ProductType { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
    public class Product
    {
        [Display(Name = "Product Name")]
        public int Id { get; set; }
        [Required, StringLength(40), Display(Name = "Product Name")]
        public string Name { get; set; }
        [Required, StringLength(150),]
        public string Description { get; set; }
        [StringLength(32)]
        public string _Key { get; set; }
        [Column(TypeName = "tinyint")]
        public int Is_Deleted { get; set; }
        [Required, Display(Name ="Category Name")]
        public int Category_Id { get; set; }
        [ForeignKey("Category_Id")]
        public virtual Category Category { get; set; }
    }

    public class InventoryERPDbContext : DbContext
    {
        public InventoryERPDbContext(DbContextOptions<InventoryERPDbContext> options) : base(options) { }

        //Dbset Entity
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }


        //Adding Expense Category on model Creating into the database
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ProductType>().HasData(
        //        new ProductType { Id=1, Name="Raw", Description="Demo" },
        //        new ProductType { Id=2, Name="Finish", Description="Demo" },
        //        new ProductType { Id=3, Name="Accessories", Description="Demo" },
        //        new ProductType { Id=4, Name="Others", Description="Demo" },         
        //        new ProductType { Id=5, Name="Spare Parts", Description="Demo" }         
        //        );
        //    modelBuilder.Entity<Category>().HasData(
        //        new Category { Id=1, Name="Raw Jute", Description="Raw Jute"},
        //        new Category { Id=2, Name="Yarn", Description="Finish"},
        //        new Category { Id=3, Name="Niddle", Description="Demo"}
        //        );
        //    modelBuilder.Entity<Product>().HasData(
        //        new Product { Id=1, Name="Deshi Jute", Description="Deshi Jute"},
        //        new Product { Id=2, Name="2 Ply", Description="Yarn"},
        //        new Product { Id=3, Name="3 Ply", Description="Yarn"}
        //        );
        //}
    }
}
