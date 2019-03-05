using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using App.UI.Models.Product;
using App.UI.Models.Common;
using App.UI.Models.TreeComponent;

namespace App.UI.Controllers
{
    public class TreeComponentController : Controller
    {
        private static List<Category> categories;
        public TreeComponentController()
        {
            if (categories == null)
            {
                categories = new List<Category>();
                for (int i = 1; i < 100; i++)
                {
                    var c = new Category()
                    {
                        Id = i,
                        Name = $"Category-{i}",
                        Description = $"Category-Description-{i}",
                    };
                    c.Products = new List<Product>();
                    for (int j = 1; j < 10; j++)
                    {
                        var p = new Product()
                        {
                            Id = j,
                            Name = $"Product-{i}",
                            Description = $"Product-Description-{i}",
                        };

                        c.Products.Add(p);
                    }
                    categories.Add(c);
                }
            }
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(categories);
        }
        public ActionResult EntityTreeNodes(EntityNode node)
        {
            List<EntityNode> result;
            if (node.Id == null)
            {
                result = categories.Select(x =>
                  new EntityNode
                  {
                      Id = x.Id,
                      Text = x.Name,
                      HasChild = x.Products.Any(),
                      ParentId = null,
                      Type = "c"
                  }).ToList();
            }
            else
            {
                result = categories.Where(x => x.Id == node.Id).SelectMany(x =>x.Products,(parent, child) => new EntityNode
                {
                      Id = child.Id,
                      Text = child.Name,
                      HasChild = false,
                      ParentId = null,
                      Type = "p"
                  }).ToList();
            }
            return Json(result);
        }
    }
}
