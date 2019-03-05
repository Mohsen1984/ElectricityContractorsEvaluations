using System;
using System.Collections.Generic;
using System.Diagnostics;
using App.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace App.UI.Controllers
{

    public class TreeItems
    {
        public int Id;
        public int? ParentId;
        public string Text;
        public bool HasChild;
        public List<TreeItems> SubItems;
    }

    public class DesignFormDetails
    {
        public string TitleForm;
        public string CodeForm;
        public string Project;
        public string Evaluator;
        public string Evaluated;
        public string Description;
    }
    public class DesignEvaluationFormController : Controller
    {
        private static List<EvaluationFactorTreeModel> AllItems;
        private readonly EvaluationContext db;
        public DesignEvaluationFormController(EvaluationContext d)
        {
            db = d;
            //if (AllItems == null)
            //{
            AllItems = new List<EvaluationFactorTreeModel>();
            var a = db.EvaluationFactorTrees;
            AllItems = a.ToList();


            // }
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult GetDetails()
        {
            var details = new DesignFormDetails();
            details.TitleForm = "ارزیابی اخلاق";
            details.CodeForm = "کد ارزیابی";
            details.Project = "پروژه ارزیابی";
            details.Evaluator = "محمد مدرک";
            details.Evaluated = "محمد مدرک";
            details.Description = "توضیحات ارزیابی اخلاق";
            return Json(details);
        }

        public ActionResult getTreeItems()
        {
            var TreeItems = new List<TreeItems>();
            //var TreeItems = new List<TreeItems>();
            //// must be recursive
            //TreeItems.Add(new TreeItems
            //{
            //    Id = 1,
            //    Text = "menu1",
            //    HasChild = false
            //});
            //var subItems2 = new List<TreeItems>();
            //subItems2.Add(new TreeItems
            //{
            //    Id = 21,
            //    ParentId = 2,
            //    Text = "subMenu1",
            //    HasChild = false
            //});
            //var subSubItems2 = new List<TreeItems>();
            //subSubItems2.Add(new TreeItems
            //{
            //    Id = 221,
            //    ParentId = 22,
            //    Text = "subMenu1_1",
            //    HasChild = false
            //});
            //subSubItems2.Add(new TreeItems
            //{
            //    Id = 222,
            //    ParentId = 22,
            //    Text = "subMenu1_2",
            //    HasChild = false
            //});
            //subItems2.Add(new TreeItems
            //{
            //    Id = 22,
            //    ParentId = 2,
            //    Text = "subMenu2",
            //    HasChild = true,
            //    SubItems = subSubItems2
            //});
            //TreeItems.Add(new TreeItems
            //{
            //    Id = 2,
            //    Text = "menu2",
            //    HasChild = true,
            //    SubItems = subItems2
            //});
            //TreeItems.Add(new TreeItems
            //{
            //    Id = 3,
            //    Text = "menu3",
            //    HasChild = false
            //});
            //var subItems4 = new List<TreeItems>();
            //subItems4.Add(new TreeItems
            //{
            //    Id = 41,
            //    ParentId = 4,
            //    Text = "subMenu1",
            //    HasChild = false
            //});
            //var subSubItems4 = new List<TreeItems>();
            //subSubItems4.Add(new TreeItems
            //{
            //    Id = 421,
            //    ParentId = 42,
            //    Text = "subMenu1_1",
            //    HasChild = false
            //});
            //subSubItems4.Add(new TreeItems
            //{
            //    Id = 422,
            //    ParentId = 42,
            //    Text = "subMenu1_2",
            //    HasChild = false
            //});
            //subItems4.Add(new TreeItems
            //{
            //    Id = 42,
            //    ParentId = 4,
            //    Text = "subMenu2",
            //    HasChild = true,
            //    SubItems = subSubItems4
            //});
            //subItems4.Add(new TreeItems
            //{
            //    Id = 43,
            //    ParentId = 4,
            //    Text = "subMenu3",
            //    HasChild = false
            //});
            //TreeItems.Add(new TreeItems
            //{
            //    Id = 4,
            //    Text = "menu4",
            //    HasChild = true,
            //    SubItems = subItems4
            //});
            TreeItems = db.EvaluationFactorTrees.Select(a => new TreeItems
            {
                Id = a.EvaluationFactorId,
                Text = a.Title,
                ParentId = (int)a.EvaluationFactorRef,
                HasChild = (db.EvaluationFactorTrees.Where(p => p.EvaluationFactorRef == a.EvaluationFactorId).Count() == 0 ? false : true),
                SubItems = a.Children.Select(b => new TreeItems { Id = b.EvaluationFactorId, Text = b.Title, ParentId = b.EvaluationFactorRef, HasChild = (db.EvaluationFactorTrees.Where(p => p.EvaluationFactorRef == b.EvaluationFactorId).Count() == 0 ? false : true) }).ToList()
            }).ToList();
            return Json(TreeItems);
        }

        public IActionResult Error()
        {
            return View();
            //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}
