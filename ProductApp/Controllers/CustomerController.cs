using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApp.Context;
using ProductApp.Models;
using ProductApp.Models.Entities;
using System.Diagnostics;

namespace ProductApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DataContext _context;

        public CustomerController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> CreateCustomerAsync(CustomerRequest req)
        {
            try
            {
                var customerEntity = new CustomerEntity()
                {
                    Name = req.Name,
                };

                _context.Customers.Add(customerEntity);
                await _context.SaveChangesAsync();

                return new OkResult();
                //    (new CustomerEntity
                //{
                //    Id = customerEntity.Id,
                //    Name = customerEntity.Name,
                //});
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }
        [HttpGet]
        public async Task<IEnumerable<CustomerModel>> GetAllCustomerAsync()
        {
            var customerModel = new List<CustomerModel>();

            try
            {
                foreach (var item in await _context.Customers.ToListAsync())
                    customerModel.Add(new CustomerModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                    });

                return customerModel;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return customerModel;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetOneCustomerAsync(int id)
        {
            try
            {
                var customerModel = await _context.Customers.FindAsync(id);
                if (customerModel == null)
                    return new NotFoundResult();

                return new OkObjectResult(new CustomerModel
                {
                    Id = customerModel.Id,
                    Name = customerModel.Name,
                });
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }
        //[HttpGet]
        //public async Task<ActionResult> GetAllCustomersAsync()
        //{
        //    return new OkObjectResult(await _customerService.GetAllCustomersAsync());
        //}
        //[HttpGet("{id}")]
        //public async Task<ActionResult> GetOneCustomerAsync(int id)
        //{
        //    //var result = await _customerService.GetOneCustomerAsync(id);
        //    //if (result == null)
        //    //{ return new BadRequestResult(); }
        //    //return new OkObjectResult(result);
        //}
        //[HttpPut("{id}")]
        //public async Task<ActionResult> UpdateCustomerAsync(int id, CustomerModel customersModel)
        //{



        //    //try
        //    //{
        //    //    if (id != customersModel.Id)
        //    //        return new BadRequestResult();

        //    //    var customerEntity = await _context.Customers.FindAsync(id);
        //    //    if (customerEntity != null)
        //    //    {
        //    //        customerEntity.Name = customersModel.Name;
        //    //        _context.Entry(customerEntity).State = EntityState.Modified;
        //    //        await _context.SaveChangesAsync();

        //    //        return new OkResult();
        //    //    }
        //    //    return new NotFoundResult();
        //    //}
        //    //catch (Exception ex) { Debug.WriteLine(ex.Message); }
        //    //return new BadRequestResult();
        //}
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> DeleteCustomerAsync(int id)
        //{
        //    try
        //    {
        //        var customerEntity = await _context.Customers.FindAsync(id);
        //        if (customerEntity == null)
        //            return new NotFoundResult();
        //        _context.Customers.Remove(customerEntity);
        //        await _context.SaveChangesAsync();
        //        return new OkResult();
        //    }
        //    catch (Exception ex) { Debug.WriteLine(ex.Message); }
        //    return new BadRequestResult();
        //}
    }
}
