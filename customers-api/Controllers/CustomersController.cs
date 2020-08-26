using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomersService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CustomersService.Controllers
{
    
    [ApiController]
    [Route("api/customers")]
    [Produces("application/json")]
    public class CustomersController : ControllerBase
    {
        public Services.CustomersService CustomersService { get; }
        public ILogger<CustomersController> Logger { get; }

        public CustomersController(Services.CustomersService customersService, ILogger<CustomersController> logger)
        {
            CustomersService = customersService;
            Logger = logger;
        }

        /// <summary>
        /// Retrieve a list of customers
        /// </summary>
        /// <returns>A list of customer object instances <see cref="CustomerListModel"/></returns>
        /// <response code="200">Returns the list of customres</response>
        /// <response code="500">If server code crashed</response>
        [HttpGet]
        [Route("")]
        public ActionResult<IEnumerable<CustomerListModel>> GetAllCustomers()
        {
            try
            {
                var all = CustomersService.GetAllCustomers();
                return Ok(all);
            }
            catch (Exception e)
            {
                Logger.LogCritical(e, "Error while reading all customers");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Retrieve a particular customer by its identifier
        /// </summary>
        /// <param name="id">Customer's identifier</param>
        /// <returns>The found customer instance</returns>
        /// <response code="200">Returns the desired customer object</response>
        /// <response code="404">If no customer was found with provided identifier</response>
        /// <response code="400">If invalid request has been issued</response>
        /// <response code="500">If server code crashed</response>
        [HttpGet]
        [Route("{id:guid}", Name = "GetCustomerById")]
        public ActionResult<CustomerDetailsModel> GetCustomerById([FromRoute] Guid id)
        {
            try
            {
                var customer = CustomersService.GetCustomerById(id);
                if (customer == null)
                {
                    return NotFound();
                }

                return Ok(customer);
            }
            catch (Exception e)
            {
                Logger.LogCritical(e, "Error while reading particular customer");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Create a new customer
        /// </summary>
        /// <param name="customerCreateModel">The new customer that should be created <see cref="CustomerCreateModel"/></param>
        /// <returns>The created customer</returns>
        /// <response code="201">When creation was succesful</response>
        /// <response code="400">If invalid request has been issued</response>
        /// <response code="500">If server code crashed</response>
        [HttpPost]
        [Route("")]
        public IActionResult CreateCustomer([FromBody] CustomerCreateModel customerCreateModel)
        {
            try
            {
                var createdCustomer = CustomersService.CreateCustomer(customerCreateModel);
                
                return CreatedAtAction("GetCustomerById", new { Id = createdCustomer.Id }, createdCustomer);
            }
            catch (Exception e)
            {
                Logger.LogCritical(e, "Error while creating new Customer");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Delete a customer by its identifer
        /// </summary>
        /// <param name="id">Customer's identifier</param>
        /// <returns>Status Code 200 if operation was successful</returns>
        /// <response code="400">If invalid request has been issued</response>
        /// <response code="404">If no customer was found with given identifier</response>
        /// <response code="500">If server code crashed</response>
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteCustomer([FromRoute] Guid id)
        {
            try
            {
                var success = CustomersService.DeleteCustomerById(id);
                if (success)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception e)
            {
                Logger.LogCritical(e, "Error while deleting customer");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Update an existing customer
        /// </summary>
        /// <param name="id">Customer's identifier</param>
        /// <param name="customerUpdateModel">Customer object instance with changes <see cref="CustomerUpdateModel"/></param>
        /// <returns>Status Code 200 along with the modified customer <see cref="CustomerDetailsModel"/>, if operation was successful</returns>
        /// <response code="400">If invalid request has been issued</response>
        /// <response code="404">If no customer was found with given identifier</response>
        /// <response code="500">If server code crashed</response>
        [HttpPut]
        [Route("{id:guid}")]
        public ActionResult<CustomerDetailsModel> UpdateCustomer([FromRoute] Guid id, [FromBody] CustomerUpdateModel customerUpdateModel)
        {
            try
            {
                var customer = CustomersService.UpdateCustomerById(id, customerUpdateModel);
                if (customer == null)
                {
                    return NotFound();
                }
                return Ok(customer);
            }
            catch (Exception e)
            {
                Logger.LogCritical(e, "Error while updating customer");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Retrieve the total number of customers
        /// </summary>
        /// <returns>The total number of customers</returns>
        /// <response code="200">If operation succeeded</response>
        /// <response code="500">If server code crashed</response>
        [HttpGet]
        [Route("count")]
        public ActionResult<int> GetCustomerCount()
        {
            try
            {
                var count = CustomersService.GetCustomerCount();
                return Ok(new { Count = count });
            }
            catch (Exception e)
            {
                Logger.LogCritical(e, "Error while reading customer count");
                return StatusCode(500);
            }
        }

    }
}