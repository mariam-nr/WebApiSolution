using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiSolution.Domain.Models;
using WebApiSolution.Services.Implementations;

namespace WebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("GetProduct/{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
                return NotFound("Product not found");

            return Ok(product);
        }

        [HttpPost("AddProduct")]
        public IActionResult AddProduct(Product product)
        {
            var newProduct = _productService.AddProduct(product);
            if (newProduct == null)
                return BadRequest("Addproduct failed");
            return Ok("Product added");
            //return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut("UpdateProduct/{id}")]
        public IActionResult UpdateProduct(int id, Product product)
        {
            var uptProd = _productService.UpdateProduct(id, product);
            if (uptProd == null)
                return BadRequest("UpdateProduct failed");
            return Ok("Product updated");
        }

        [HttpDelete("DeleteProduct/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var result = _productService.DeleteProduct(id);
            if (!result)
                return NotFound("Product not found");

            return Ok("Product Deleted");
        }

        [HttpPost("category/AddCategory")]
        public IActionResult AddCategory(Category category)
        {
            var newCategory = _productService.AddCategory(category);
            if (newCategory == null)
                return BadRequest("AddCategory failed");
            return Ok("Category added");
        }


        [HttpGet("category/GetProductsByCategory/{categoryId}")]
        public IActionResult GetProductsByCategory(int categoryId)
        {
            var products = _productService.GetProductsByCategory(categoryId);
            return Ok(products);
        }

        [HttpGet("category/GetTotalPriceByCategory/{categoryId}")]
        public IActionResult GetTotalPriceByCategory(int categoryId)
        {
            var totalPrice = _productService.GetTotalPriceByCategory(categoryId);
            return Ok(totalPrice);
        }

        [HttpGet("GetTotalPricePerCategory")]
        public IActionResult GetTotalPricePerCategory()
        {
            var result = _productService.GetTotalPricePerCategory();
            return Ok(result);
        }
    }
}
