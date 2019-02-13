using AskQuestion.Core.Infrastructure;
using AskQuestion.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AskQuestion.Service.Controllers
{
    public class CategoryController : ApiController
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        [Route("api/category/insert")]
        public HttpResponseMessage Insert(Category obj)
        {
            try
            {
                _categoryRepository.Insert(obj);
                _categoryRepository.Save();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            
        }
        [Authorize]
        [HttpGet]
        [Route("api/category/listcategory")]
        public IEnumerable<Category> ListCategory()
        {

            return _categoryRepository.GetAll().Select(x=>new Category()
            {
                CategoryName=x.CategoryName,
                CategoryId=x.CategoryId
            }); 

        }
        //[Authorize(Roles ="Admin")]
        //[HttpDelete]
        //[Route("api/category/deletecategory")]

    }
}
