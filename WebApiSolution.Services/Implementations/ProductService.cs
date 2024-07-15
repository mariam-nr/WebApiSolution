using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiSolution.Data;
using WebApiSolution.Domain.Models;

namespace WebApiSolution.Services.Implementations
{
    public class ProductService
    {
        private readonly AppDbContext _dbContext;

        public ProductService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IEnumerable<Product> GetAllProducts()
        {
            return _dbContext.Products.ToList();
        }

        public Product GetProductById(int id)
        {

            return _dbContext.Products.FirstOrDefault(p => p.Id == id);
        }

        public Product AddProduct(Product product)
        {
            var categoryExists = _dbContext.Categories.FirstOrDefault(c => c.Id == product.CategoryId);
            if (categoryExists == null)
                return null;
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            return product;
        }

        public Product UpdateProduct(int id, Product product)
        {
            if (_dbContext.Products.FirstOrDefault(p => p.Id == id) == null || _dbContext.Categories.FirstOrDefault(c=> c.Id == product.CategoryId) == null)
                return null;
            var prod2 = new Product();
            prod2.CategoryId = product.CategoryId;
            prod2.Name = product.Name;
            prod2.Price = product.Price;
            _dbContext.Products.Update(prod2);
            _dbContext.SaveChanges();
            return prod2;
        }

        public bool DeleteProduct(int id)
        {
            var product = _dbContext.Products.Find(id);
            if (product == null)
                return false;

            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
            return true;
        }

        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            return _dbContext.Products
                .Where(p => p.CategoryId == categoryId)
                .ToList();
        }

        public Category AddCategory(Category category)
        {
            if (category == null)
                return null;
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
            return category;
        }

        public decimal GetTotalPriceByCategory(int categoryId)
        {
            return _dbContext.Products
                .Where(p => p.CategoryId == categoryId)
                .Sum(p => p.Price);
        }

        public Dictionary<string, decimal> GetTotalPricePerCategory()
        {
            return _dbContext.Categories
                .Select(c => new
                {
                    CategoryName = c.Name,
                    TotalPrice = _dbContext.Products.Where(p => p.Id == c.Id).Sum(p => p.Price)
                }).ToDictionary(c => c.CategoryName, c => c.TotalPrice);
                //.ToList();
        }
    }
}
