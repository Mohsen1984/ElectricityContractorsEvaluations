using System;
using System.Collections.Generic;
using System.Diagnostics;
using App.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Paint.Models;
using System.Linq;

namespace Paint.Controllers
{
    public class PopulateTreeItems
    {
        public int Id;
        public int? ParentId;
        public decimal Grade1;
        public decimal Grade2;
        public decimal? Grade3;
        public string Text;
        public bool HasChild;
        public List<PopulateTreeItems> SubItems;
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
    public class PopulateEvaluationFormController : Controller
    {
        private static List<EvaluationFactorTreeModel> AllItems;
        private readonly EvaluationContext db;
        public PopulateEvaluationFormController(EvaluationContext d)
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

        public ActionResult GetDetails(int id = 0)
        {
            var de = db.EVForms.Where(x=>x.EVFormId==id).Select(a =>
            new DesignFormDetails
            {
                TitleForm = a.Title,
                CodeForm = a.Description,
                Project = a.Description,
                Evaluator = a.EvaluatorRole.Title + " ( " + a.EvaluatorPersone.FullName + " ) ",
                Evaluated = a.EvaluatedRole.Title + " ( " + a.EvaluatedPersone.FullName + " ) ",
                Description = a.EvaluationPeriod.Title
            }).FirstOrDefault();

            var details = new DesignFormDetails();
            details.TitleForm =de.TitleForm;
            details.CodeForm = de.CodeForm;
            details.Project = de.Project;
            details.Evaluator = de.Evaluator;
            details.Evaluated = de.Evaluated;
            details.Description = de.Description;
            return Json(details);
        }

        public ActionResult getTreeItems(int id)
        {
            var PopulateTreeItems = new List<PopulateTreeItems>();

            // must be recursive
            //PopulateTreeItems.Add(new PopulateTreeItems
            //{
            //    Id = 1,
            //    Grade1 = 12,
            //    Grade2 = 13,
            //    Grade3 = 20,
            //    Text = "menu1",
            //    HasChild = false
            //});
            //var subItems2 = new List<PopulateTreeItems>();
            //subItems2.Add(new PopulateTreeItems
            //{
            //    Id = 21,
            //    Grade1 = 12,
            //    Grade2 = 13,
            //    Grade3 = 20,
            //    ParentId = 2,
            //    Text = "subMenu1",
            //    HasChild = false
            //});
            //var subSubItems2 = new List<PopulateTreeItems>();
            //subSubItems2.Add(new PopulateTreeItems
            //{
            //    Id = 221,
            //    Grade1 = 12,
            //    Grade2 = 13,
            //    Grade3 = 20,
            //    ParentId = 22,
            //    Text = "subMenu1_1",
            //    HasChild = false
            //});
            //subSubItems2.Add(new PopulateTreeItems
            //{
            //    Id = 222,
            //    Grade1 = 12,
            //    Grade2 = 13,
            //    Grade3 = 20,
            //    ParentId = 22,
            //    Text = "subMenu1_2",
            //    HasChild = false
            //});
            //subItems2.Add(new PopulateTreeItems
            //{
            //    Id = 22,
            //    Grade1 = 12,
            //    Grade2 = 13,
            //    Grade3 = 20,
            //    ParentId = 2,
            //    Text = "subMenu2",
            //    HasChild = true,
            //    SubItems = subSubItems2
            //});
            //PopulateTreeItems.Add(new PopulateTreeItems
            //{
            //    Id = 2,
            //    Grade1 = 12,
            //    Grade2 = 13,
            //    Grade3 = 20,
            //    Text = "menu2",
            //    HasChild = true,
            //    SubItems = subItems2
            //});
            //PopulateTreeItems.Add(new PopulateTreeItems
            //{
            //    Id = 3,
            //    Grade1 = 12,
            //    Grade2 = 13,
            //    Grade3 = 20,
            //    Text = "menu3",
            //    HasChild = false
            //});
            //var subItems4 = new List<PopulateTreeItems>();
            //subItems4.Add(new PopulateTreeItems
            //{
            //    Id = 41,
            //    Grade1 = 12,
            //    Grade2 = 13,
            //    Grade3 = 20,
            //    ParentId = 4,
            //    Text = "subMenu1",
            //    HasChild = false
            //});
            //var subSubItems4 = new List<PopulateTreeItems>();
            //subSubItems4.Add(new PopulateTreeItems
            //{
            //    Id = 421,
            //    Grade1 = 12,
            //    Grade2 = 13,
            //    Grade3 = 20,
            //    ParentId = 42,
            //    Text = "subMenu1_1",
            //    HasChild = false
            //});
            //subSubItems4.Add(new PopulateTreeItems
            //{
            //    Id = 422,
            //    Grade1 = 12,
            //    Grade2 = 13,
            //    Grade3 = 20,
            //    ParentId = 42,
            //    Text = "subMenu1_2",
            //    HasChild = false
            //});
            //subItems4.Add(new PopulateTreeItems
            //{
            //    Id = 42,
            //    Grade1 = 12,
            //    Grade2 = 13,
            //    Grade3 = 20,
            //    ParentId = 4,
            //    Text = "subMenu2",
            //    HasChild = true,
            //    SubItems = subSubItems4
            //});
            //subItems4.Add(new PopulateTreeItems
            //{
            //    Id = 43,
            //    Grade1 = 12,
            //    Grade2 = 13,
            //    Grade3 = 20,
            //    ParentId = 4,
            //    Text = "subMenu3",
            //    HasChild = false
            //});
            //PopulateTreeItems.Add(new PopulateTreeItems
            //{
            //    Id = 4,
            //    Grade1 = 12,
            //    Grade2 = 13,
            //    Grade3 = 20,
            //    Text = "menu4",
            //    HasChild = true,
            //    SubItems = subItems4
            //});
            //PopulateTreeItems = db.EvaluationFactorTree.Select(a => new PopulateTreeItems
            //{
            //    Grade1 = 1,
            //    Grade2 = 2,
            //    Grade3 = 3,
            //    Id = a.EvaluationFactorId,
            //    Text = a.Title.Substring(0, 40),
            //    ParentId = (int)a.EvaluationFactorRef,
            //    HasChild = (db.EvaluationFactorTree.Where(p => p.EvaluationFactorRef == a.EvaluationFactorId).Count() == 0 ? false : true),
            //    SubItems = a.Children.Select(b => new PopulateTreeItems
            //    {
            //        Grade1 = 1,
            //        Grade2 = 2,
            //        Grade3 = 3,
            //        Id = b.EvaluationFactorId,
            //        Text = b.Title.Substring(0, 40),
            //        ParentId = b.EvaluationFactorRef,
            //        HasChild = (db.EvaluationFactorTree.Where(p => p.EvaluationFactorRef == b.EvaluationFactorId).Count() == 0 ? false : true)
            //    }).Take(5).ToList()
            //}).Take(5).ToList();
            PopulateTreeItems = db.EVFormItems.Where(x=>x.EVFormRef==id).Select(a => new PopulateTreeItems
            {
                //Grade1 = 1,
                //Grade2=2,
                Grade3=a.Score,
                Id =(int) a.EvaluationFactorRef,
                Text = a.EvaluationFactor.Title.Substring(0, 20),
                ParentId = (int)a.EvaluationFactorRef,
                HasChild=false
                //HasChild = (db.EVFormItems.Where(p => p. == a.EvaluationFactorRef).Count() == 0 ? false : true),
                //SubItems = a.Children.Select(b => new PopulateTreeItems {
                //    Grade1 = 1,
                //    Grade2 = 2,
                //    Grade3 = 3,
                //    Id = b.EvaluationFactorId, Text = b.Title.Substring(0,40), ParentId = b.EvaluationFactorRef, HasChild = (db.EvaluationFactorTree.Where(p => p.EvaluationFactorRef == b.EvaluationFactorId).Count() == 0 ? false : true) }).Take(5).ToList()
            }).ToList();
            return Json(PopulateTreeItems);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
