using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductsService.Models;

namespace ProductsService.Controllers
{
    
    [ApiController]
    [Route("api/products")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        public Services.ProductsService ProductsService { get; }
        public ILogger<ProductsController> Logger { get; }

        public ProductsController(Services.ProductsService productsService, ILogger<ProductsController> logger)
        {
            ProductsService = productsService;
            Logger = logger;
        }

        /// <summary>
        /// Retrieve a list of products
        /// </summary>
        /// <returns>A list of product object instances <see cref="ProductListModel"/></returns>
        /// <response code="200">Returns the list of customres</response>
        /// <response code="500">If server code crashed</response>
        [HttpGet]
        [Route("")]
        public ActionResult<IEnumerable<ProductListModel>> GetAllProducts()
        {
            try
            {
                var all = ProductsService.GetAllProducts();
                return Ok(all);
            }
            catch (Exception e)
            {
                Logger.LogCritical(e, "Error while reading all products");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Retrieve a particular product by its identifier
        /// </summary>
        /// <param name="id">Product's identifier</param>
        /// <returns>The found product instance</returns>
        /// <response code="200">Returns the desired product object</response>
        /// <response code="404">If no product was found with provided identifier</response>
        /// <response code="400">If invalid request has been issued</response>
        /// <response code="500">If server code crashed</response>
        [HttpGet]
        [Route("{id:guid}", Name = "GetProductById")]
        public ActionResult<ProductDetailsModel> GetProductById([FromRoute] Guid id)
        {
            try
            {
                var product = ProductsService.GetProductById(id);
                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception e)
            {
                Logger.LogCritical(e, "Error while reading particular product");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="productCreateModel">The new product that should be created <see cref="ProductCreateModel"/></param>
        /// <returns>The created product</returns>
        /// <response code="201">When creation was succesful</response>
        /// <response code="400">If invalid request has been issued</response>
        /// <response code="500">If server code crashed</response>
        [HttpPost]
        [Route("")]
        public IActionResult CreateProduct([FromBody] ProductCreateModel productCreateModel)
        {
            try
            {
                var createdProduct = ProductsService.CreateProduct(productCreateModel);

                return CreatedAtAction("GetProductById", new { Id = createdProduct.Id }, createdProduct);
            }
            catch (Exception e)
            {
                Logger.LogCritical(e, "Error while creating new product");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Delete a product by its identifer
        /// </summary>
        /// <param name="id">Product's identifier</param>
        /// <returns>Status Code 200 if operation was successful</returns>
        /// <response code="400">If invalid request has been issued</response>
        /// <response code="404">If no product was found with given identifier</response>
        /// <response code="500">If server code crashed</response>
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteProductById([FromRoute] Guid id)
        {
            try
            {
                var success = ProductsService.DeleteProductById(id);
                if (success)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception e)
            {
                Logger.LogCritical(e, "Error while deleting product");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Update an existing product
        /// </summary>
        /// <param name="id">Product's identifier</param>
        /// <param name="productUpdateModel">Product object instance with changes <see cref="ProductUpdateModel"/></param>
        /// <returns>Status Code 200 along with the modified product <see cref="ProductDetailsModel"/>, if operation was successful</returns>
        /// <response code="400">If invalid request has been issued</response>
        /// <response code="404">If no product was found with given identifier</response>
        /// <response code="500">If server code crashed</response>
        [HttpPut]
        [Route("{id:guid}")]
        public ActionResult<ProductDetailsModel> UpdateProductById([FromRoute] Guid id, [FromBody] ProductUpdateModel productUpdateModel)
        {
            try
            {
                var product = ProductsService.UpdateProductById(id, productUpdateModel);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception e)
            {
                Logger.LogCritical(e, "Error while updating product");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Retrieve the total number of products
        /// </summary>
        /// <returns>The total number of products</returns>
        /// <response code="200">If operation succeeded</response>
        /// <response code="500">If server code crashed</response>
        [HttpGet]
        [Route("totalcount")]
        public ActionResult<int> GetProductCount()
        {
            try
            {
                var count = ProductsService.GetProductCount();
                return Ok(new { Count = count });
            }
            catch (Exception e)
            {
                Logger.LogCritical(e, "Error while reading product count");
                return StatusCode(500);
            }
        }
    }
}