using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Context;
using ProductApp.Models.Entities;
using ProductApp.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace ProductApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult> CreateProductAsync(ProductRequest req)
        {
            try
            {
                var productEntity = new ProductEntity()
                {
                    Id = Guid.NewGuid(),
                    Name = req.Name,
                    Price = req.Price
                };

                _context.Products.Add(productEntity);
                await _context.SaveChangesAsync();

                return new OkObjectResult(new ProductEntity
                {
                    Id = productEntity.Id,
                    Name = productEntity.Name,
                    Price = productEntity.Price,
                });
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }
        [HttpGet]
        public async Task<IEnumerable<ProductModel>> GetAllProductAsync()
        {
            var productModel = new List<ProductModel>();

            try
            {
                foreach (var item in await _context.Products.ToListAsync())
                    productModel.Add(new ProductModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Price = item.Price
                    });

                return productModel;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return productModel;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetOneProductAsync(Guid id)
        {
            try
            {
                var productModel = await _context.Products.FindAsync(id);
                if (productModel == null)
                    return new NotFoundResult();

                return new OkObjectResult(new ProductModel
                {
                    Id = productModel.Id,
                    Name = productModel.Name,
                    Price= productModel.Price,
                });
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProductAsync(int id, ProductModel productModel)
        {
            try
            {
                //if (id != productModel.Id)
                //    return new BadRequestResult();

                var productEntity = await _context.Products.FindAsync(id);
                if (productEntity != null)
                {
                    productEntity.Name = productModel.Name;
                    productEntity.Price = productModel.Price;
                    _context.Entry(productEntity).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    return new OkResult();
                }
                return new NotFoundResult();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductAsync(int id)
        {
            try
            {
                var productEntity = await _context.Products.FindAsync(id);
                if (productEntity == null)
                    return new NotFoundResult();
                _context.Products.Remove(productEntity);
                await _context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }
    }
}
