using System;
using System.Linq;
using System.Threading.Tasks;
using AdventureWorksAPI.Extensions;
using AdventureWorksAPI.Models;
using AdventureWorksAPI.Responses;
using AdventureWorksAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorksAPI.Controllers
{
    [Route("api/Production")]
    public class ProductionController : Controller
    {
        private IAdventureWorksRepository AdventureWorksRepository;

        public ProductionController(IAdventureWorksRepository repository)
        {
            AdventureWorksRepository = repository;
        }

        protected override void Dispose(Boolean disposing)
        {
            if (AdventureWorksRepository != null)
            {
                AdventureWorksRepository.Dispose();
            }

            base.Dispose(disposing);
        }

        // GET Production/Product
        [HttpGet]
        [Route("Product")]
        public async Task<IActionResult> Get()
        {
            var response = new ListModelResponse<ProductViewModel>() as IListModelResponse<ProductViewModel>;

            try
            {
                response.Model = await Task.Run(() =>
                {
                    return AdventureWorksRepository.GetProducts().Select(item => item.ToViewModel()).ToList();
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
        [HttpGet]
        [Route("Product/{id}")]
        public async Task<IActionResult> Get(Int32 id)
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
    }
}
