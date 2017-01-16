using System;
using System.Linq;
using System.Threading.Tasks;
using AdventureWorksAPI.Core.DataLayer;
using AdventureWorksAPI.Extensions;
using AdventureWorksAPI.Responses;
using AdventureWorksAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorksAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProductionController : Controller
    {
        private IAdventureWorksRepository AdventureWorksRepository;

        public ProductionController(IAdventureWorksRepository repository)
        {
            AdventureWorksRepository = repository;
        }

        protected override void Dispose(Boolean disposing)
        {
            AdventureWorksRepository?.Dispose();

            base.Dispose(disposing);
        }

        // GET Production/Product
        /// <summary>
        /// Retrieves a list of products
        /// </summary>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageNumber">Page number</param>
        /// <param name="name">Name</param>
        /// <returns>List response</returns>
        [HttpGet]
        [Route("Product")]
        public async Task<IActionResult> GetProducts(Int32? pageSize = 10, Int32? pageNumber = 1, String name = null)
        {
            var response = new ListModelResponse<ProductViewModel>() as IListModelResponse<ProductViewModel>;

            try
            {
                response.PageSize = (Int32)pageSize;
                response.PageNumber = (Int32)pageNumber;

                response.Model = await Task.Run(() =>
                {
                    return AdventureWorksRepository
                        .GetProducts(response.PageSize, response.PageNumber, name)
                        .Select(item => item.ToViewModel())
                        .ToList();
                });

                response.Message = String.Format("Total of records: {0}", response.Model.Count());
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }

            return response.ToHttpResponse();
        }

        // GET Production/Product/5
        /// <summary>
        /// Retrieves a specific product by id
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>Single response</returns>
        [HttpGet]
        [Route("Product/{id}")]
        public async Task<IActionResult> GetProduct(Int32 id)
        {
            var response = new SingleModelResponse<ProductViewModel>() as ISingleModelResponse<ProductViewModel>;

            try
            {
                response.Model = await Task.Run(() =>
                {
                    return AdventureWorksRepository.GetProduct(id).ToViewModel();
                });
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }

            return response.ToHttpResponse();
        }

        // POST Production/Product/
        /// <summary>
        /// Creates a new product on Production catalog
        /// </summary>
        /// <param name="value">Product entry</param>
        /// <returns>Single response</returns>
        [HttpPost]
        [Route("Product")]
        public async Task<IActionResult> CreateProduct([FromBody]ProductViewModel value)
        {
            var response = new SingleModelResponse<ProductViewModel>() as ISingleModelResponse<ProductViewModel>;

            try
            {
                var entity = await Task.Run(() =>
                {
                    return AdventureWorksRepository.AddProduct(value.ToEntity());
                });

                response.Model = entity.ToViewModel();
                response.Message = "The data was saved successfully";
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.ToString();
            }

            return response.ToHttpResponse();
        }

        // PUT Production/Product/5
        /// <summary>
        /// Updates an existing product
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <param name="value">Product entry</param>
        /// <returns>Single response</returns>
        [HttpPut]
        [Route("Product/{id}")]
        public async Task<IActionResult> UpdateProduct(Int32 id, [FromBody]ProductViewModel value)
        {
            var response = new SingleModelResponse<ProductViewModel>() as ISingleModelResponse<ProductViewModel>;

            try
            {
                var entity = await Task.Run(() =>
                {
                    return AdventureWorksRepository.UpdateProduct(id, value.ToEntity());
                });

                response.Model = entity.ToViewModel();
                response.Message = "The record was updated successfully";
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }

            return response.ToHttpResponse();
        }

        // DELETE Production/Product/5
        /// <summary>
        /// Delete an existing product
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>Single response</returns>
        [HttpDelete]
        [Route("Product/{id}")]
        public async Task<IActionResult> DeleteProduct(Int32 id)
        {
            var response = new SingleModelResponse<ProductViewModel>() as ISingleModelResponse<ProductViewModel>;

            try
            {
                var entity = await Task.Run(() =>
                {
                    return AdventureWorksRepository.DeleteProduct(id);
                });

                response.Model = entity.ToViewModel();
                response.Message = "The record was deleted successfully";
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }

            return response.ToHttpResponse();
        }
    }
}
