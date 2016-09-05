using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoMapper;
using System.Net.Http;
using System.Web.Http;
using TeduShop.Web.Models;
using TeduShop.Service;
using TeduShop.Model.Models;
using TeduShop.Web.Infrastructure.Core;
namespace TeduShop.Web.Api
{

    [RoutePrefix("api/productcategory")]
    public class ProductCategoryController : ApiControllerBase
    {
        IProductCategoryService _productCategoryService;
        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService):base(errorService)
        {
            this._productCategoryService = productCategoryService;
        }

        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, int pageSize = 2)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;


                var model = _productCategoryService.GetAll();
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreateDate).Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(query);

                var PaginationSet = new PaginationSet<ProductCategoryViewModel>()
                {
                    Items = responseData,
                    Page =  page,
                    TotalCount = totalRow,
                    TotalPages =(int)Math.Ceiling((decimal)totalRow/pageSize)
                };

                var response = request.CreateResponse(HttpStatusCode.OK, PaginationSet);
                return response;
            });
        }
    }
}
