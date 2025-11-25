using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace XTStyle.Demo.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class BreadcrumbItem
    {
        public string Text { get; set; }
        public object Data { get; set; }
    }

    public class ProgressStep
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public static class SampleData
    {
        public static ObservableCollection<Product> GetProducts()
        {
            return new ObservableCollection<Product>
            {
                new Product { Id = 1, Name = "Laptop Dell XPS 15", Category = "Electronics", Price = 1299.99m, Stock = 15, Status = "Active", CreatedDate = DateTime.Now.AddDays(-30) },
                new Product { Id = 2, Name = "iPhone 15 Pro", Category = "Electronics", Price = 999.99m, Stock = 25, Status = "Active", CreatedDate = DateTime.Now.AddDays(-25) },
                new Product { Id = 3, Name = "Samsung Galaxy S24", Category = "Electronics", Price = 899.99m, Stock = 20, Status = "Active", CreatedDate = DateTime.Now.AddDays(-20) },
                new Product { Id = 4, Name = "Sony WH-1000XM5", Category = "Audio", Price = 349.99m, Stock = 30, Status = "Active", CreatedDate = DateTime.Now.AddDays(-15) },
                new Product { Id = 5, Name = "iPad Pro 12.9", Category = "Electronics", Price = 1099.99m, Stock = 12, Status = "Active", CreatedDate = DateTime.Now.AddDays(-10) },
                new Product { Id = 6, Name = "MacBook Pro 16", Category = "Electronics", Price = 2499.99m, Stock = 8, Status = "Low Stock", CreatedDate = DateTime.Now.AddDays(-8) },
                new Product { Id = 7, Name = "AirPods Pro 2", Category = "Audio", Price = 249.99m, Stock = 50, Status = "Active", CreatedDate = DateTime.Now.AddDays(-5) },
                new Product { Id = 8, Name = "LG OLED TV 55", Category = "Electronics", Price = 1799.99m, Stock = 5, Status = "Low Stock", CreatedDate = DateTime.Now.AddDays(-3) },
                new Product { Id = 9, Name = "Canon EOS R5", Category = "Camera", Price = 3899.99m, Stock = 3, Status = "Low Stock", CreatedDate = DateTime.Now.AddDays(-2) },
                new Product { Id = 10, Name = "DJI Mavic 3", Category = "Drone", Price = 2199.99m, Stock = 7, Status = "Active", CreatedDate = DateTime.Now.AddDays(-1) },
                new Product { Id = 11, Name = "Bose QuietComfort 45", Category = "Audio", Price = 329.99m, Stock = 18, Status = "Active", CreatedDate = DateTime.Now },
                new Product { Id = 12, Name = "Microsoft Surface Pro 9", Category = "Electronics", Price = 1299.99m, Stock = 14, Status = "Active", CreatedDate = DateTime.Now },
                new Product { Id = 13, Name = "Nintendo Switch OLED", Category = "Gaming", Price = 349.99m, Stock = 22, Status = "Active", CreatedDate = DateTime.Now },
                new Product { Id = 14, Name = "PlayStation 5", Category = "Gaming", Price = 499.99m, Stock = 10, Status = "Active", CreatedDate = DateTime.Now },
                new Product { Id = 15, Name = "Xbox Series X", Category = "Gaming", Price = 499.99m, Stock = 11, Status = "Active", CreatedDate = DateTime.Now }
            };
        }

        public static List<string> GetCategories()
        {
            return new List<string>
            {
                "All Categories",
                "Electronics",
                "Audio",
                "Camera",
                "Drone",
                "Gaming"
            };
        }

        public static ObservableCollection<BreadcrumbItem> GetBreadcrumbs()
        {
            return new ObservableCollection<BreadcrumbItem>
            {
                new BreadcrumbItem { Text = "Home", Data = "home" },
                new BreadcrumbItem { Text = "Products", Data = "products" },
                new BreadcrumbItem { Text = "Electronics", Data = "electronics" }
            };
        }

        public static ObservableCollection<ProgressStep> GetProgressSteps()
        {
            return new ObservableCollection<ProgressStep>
            {
                new ProgressStep { Title = "Account Setup", Description = "Create your account" },
                new ProgressStep { Title = "Personal Info", Description = "Enter your details" },
                new ProgressStep { Title = "Verification", Description = "Verify your email" },
                new ProgressStep { Title = "Complete", Description = "All done!" }
            };
        }
    }
}
