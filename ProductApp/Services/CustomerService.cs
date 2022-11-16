using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApp.Context;
using ProductApp.Models.Entities;
using ProductApp.Models;
using System.Diagnostics;

namespace ProductApp.Services
{
    public class CustomerService
    {
        //private readonly DataContext _context;

        //public CustomerService (DataContext context)
        //{
        //    _context = context;
        //}
        //public async Task<ActionResult> CreateCustomerAsync(CustomerRequest req)
        //{
        //    try
        //    {
        //        var customerEntity = new CustomerEntity()
        //        {
        //            Name = req.Name
        //        };

        //        _context.Customers.Add(customerEntity);
        //        await _context.SaveChangesAsync();

        //        return new OkResult();
        //    }
        //    catch (Exception ex) { Debug.WriteLine(ex.Message); }
        //    return new BadRequestResult();
        //}

        //public async Task<IEnumerable<CustomerModel>> GetAllCustomersAsync()
        //{
        //    var customerModel = new List<CustomerModel>();

        //    try
        //    {
        //        foreach (var item in await _context.Customers.ToListAsync())
        //            customerModel.Add(new CustomerModel
        //            {
        //                Id = item.Id,
        //                Name = item.Name
        //            });

        //        return customerModel;
        //    }
        //    catch (Exception ex) { Debug.WriteLine(ex.Message); }
        //    return customerModel;
        //}

        //public async Task<ActionResult> GetOneCustomerAsync(int id)
        //{
        //    try
        //    {
        //        var customerEntity = await _context.Customers.FindAsync(id);
        //        if (customerEntity == null)
        //            return new NotFoundResult();

        //        return new OkObjectResult(new CustomerModel
        //        {
        //            Id = customerEntity.Id,
        //            Name = customerEntity.Name,
        //        });
        //    }
        //    catch (Exception ex) { Debug.WriteLine(ex.Message); }
        //    return new BadRequestResult();
        //}

        //public async Task<ActionResult> UpdateCustomerAsync(int id, CustomerModel customersModel)
        //{
        //    try
        //    {
        //        if (id != customersModel.Id)
        //            return new BadRequestResult();

        //        var customerEntity = await _context.Customers.FindAsync(id);
        //        if (customerEntity != null)
        //        {
        //            customerEntity.Name = customersModel.Name;
        //            _context.Entry(customerEntity).State = EntityState.Modified;
        //            await _context.SaveChangesAsync();

        //            return new OkResult();
        //        }
        //        return new NotFoundResult();
        //    }
        //    catch (Exception ex) { Debug.WriteLine(ex.Message); }
        //    return new BadRequestResult();
        //}

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
