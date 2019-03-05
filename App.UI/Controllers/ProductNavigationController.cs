using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using App.UI.Models.Product;
using App.UI.Models.Common;

namespace App.UI.Controllers
{
    public class ProductNavigationController : Controller
    {
        private static List<Product> products;
        public ProductNavigationController()
        {
            if (products == null)
            {
                products = new List<Product>();
                for (int i = 1; i < 100; i++)
                {
                    products.Add(new Product() {
                        Id = i,
                        Name=$"Product-{i}",
                        Description=$"Product-Description-{i}"
                    });
                }
            }
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetAllPaged(ProductSearchModel model)
        {
            var filtered = products;
            if (model.Name!=null)
                filtered = filtered.Where(x => x.Name.Contains(model.Name)).ToList();
            if (model.Description != null)
                filtered = filtered.Where(x => x.Description.Contains(model.Description)).ToList();
            PagedList<Product> result = new PagedList<Product>();
            result.Items = filtered.Skip((model.PageIndex * model.PageSize)).Take(model.PageSize).ToList();
            result.PageIndex = model.PageIndex;
            result.PageSize = model.PageSize;
            result.TotalItemsCount = filtered.Count;
            return Ok(result);
        }
        [HttpGet]
        public ActionResult GetById(int id)
        {
            var result = products.Where(x => x.Id == id).FirstOrDefault();
            return Ok(result);
        }
        [HttpPost]
        public ActionResult Create([FromBody]Product model)
        {
            //validation
            products.Add(model);
            return Ok();
        }
        [HttpPost]
        public ActionResult Edit([FromBody]Product model)
        {
            //validation
            var result = products.Where(x => x.Id == model.Id).FirstOrDefault();
            if (result == null)
                return BadRequest();
            result.Name = model.Name;
            result.Description = model.Description;
            return Ok();
        }
        public ActionResult Delete([FromBody]Product model)
        {
            //validation
            var result = products.RemoveAll(x => x.Id == model.Id);
            return Ok();
        }
    }
}
