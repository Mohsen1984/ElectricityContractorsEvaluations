using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using App.UI.Models;
using App.UI.Models.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Paint.Models;

namespace App.UI.Controllers
{

    public class Quality
    {
        public string Text;
        public int Grade;
    }
    public class PopulateTreeItems
    {
        public int Id;
        public int ParentId;
        public int ItemType;
        public int MinGrade;
        public int MaxGrade;
        public decimal Grade;
        public decimal Grade1;
        public decimal Grade2;
        public decimal Grade3;
        public string Text;
        public bool HasChild;
        public int Weight;
        public List<PopulateTreeItems> SubItems;
        public List<Quality> QualityDs;
    }

    public class PopulateFormDetails
    {
        public string TitleForm;
        public string TitleGrade1;
        public string TitleGrade2;
        public string TitleGrade3;
        public string CodeForm;
        public string Project;
        public string Evaluator;
        public string Evaluated;
        public string Description;
        public int MinGrade;
        public int SumGrade;
        public int SumGrade1;
        public int SumGrade2;
        public int SumGrade3;
        public string EnterDate;
        public string ActionDate;
        public string ActionDateColor;
    }
    public class PopulateEvaluationFormController : Controller
    {
        private  List<EvaluationFactorTreeModel> AllItems;
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
        public ActionResult GetDetails(int id = 29569)
        {
            var de = db.EVForms.Where(x => x.EVFormId == id).Select(a =>
                new PopulateFormDetails
                {
                    TitleForm = a.Title,
                    CodeForm = a.Description,
                    Project = a.Description,
                    Evaluator = a.EvaluatorRole.Title + " ( " + a.EvaluatorPersone.FullName + " ) ",
                    Evaluated = a.EvaluatedRole.Title + " ( " + a.EvaluatedPersone.FullName + " ) ",
                    Description = a.EvaluationPeriod.Title
                }).FirstOrDefault();

            var details = new PopulateFormDetails();
            details.TitleGrade1 = "مدیر کل";
            details.TitleGrade2 = "سرپرست";
            details.TitleGrade3 = "ناظر";
            details.TitleForm = de.TitleForm;
            details.CodeForm = de.CodeForm;
            details.Project = de.Project;
            details.Evaluator = de.Evaluator;
            details.Evaluated = de.Evaluated;
            details.Description = de.Description;
            details.MinGrade = 16;
            details.EnterDate = "1397/12/15";
            details.ActionDate = "1397/12/29";
            details.ActionDateColor = "red";
            details.SumGrade = 30;
            details.SumGrade1 = 19;
            details.SumGrade2 = 18;
            details.SumGrade3 = 17;
            return Json(details);
        }
        //public ActionResult GetDetails()
        //{
        //    var details = new PopulateFormDetails();
        //    details.TitleGrade1 = "مدیر کل";
        //    details.TitleGrade2 = "سرپرست";
        //    details.TitleGrade3 = "ناظر";
        //    details.TitleForm = "ارزیابی اخلاق";
        //    details.CodeForm = "کد ارزیابی";
        //    details.Project = "پروژه ارزیابی";
        //    details.Evaluator = "محمد مدرک";
        //    details.Evaluated = "محمد مدرک";
        //    details.Description = "توضیحات ارزیابی اخلاق";
        //    details.MinGrade = 16;
        //    details.EnterDate = "1397/12/15";
        //    details.ActionDate = "1397/12/29";
        //    details.ActionDateColor = "red";
        //    details.SumGrade = 30;
        //    details.SumGrade1 = 19;
        //    details.SumGrade2 = 18;
        //    details.SumGrade3 = 17;
        //    return Json(details);
        //}

        //public ActionResult getTreeItems()
        //{
        //    var PopulateTreeItems = new List<PopulateTreeItems>();
        //    var qualityDs = new List<Quality>();
        //    qualityDs.Add(new Quality
        //    {
        //        Text = "بد",
        //        Grade = 1
        //    });
        //    qualityDs.Add(new Quality
        //    {
        //        Text = "خوب",
        //        Grade = 2
        //    });
        //    qualityDs.Add(new Quality
        //    {
        //        Text = "عالی",
        //        Grade = 3
        //    });
        //    // must be recursive
        //    PopulateTreeItems.Add(new PopulateTreeItems
        //    {
        //        Id = 1,
        //        ItemType = 1,
        //        Grade1 = 12,
        //        Grade2 = 13,
        //        Grade3 = 20,
        //        Weight = 2,
        //        Text = "منو 1",
        //        HasChild = false,
        //        QualityDs = qualityDs
        //    });
        //    var subItems2 = new List<PopulateTreeItems>();
        //    subItems2.Add(new PopulateTreeItems
        //    {
        //        Id = 21,
        //        Grade1 = 12,
        //        Grade2 = 13,
        //        Grade3 = 20,
        //        Weight = 2, MinGrade = 0, MaxGrade = 20,
        //        ParentId = 2,
        //        Text = "زیر منو 1",
        //        HasChild = false
        //    });
        //    var subSubItems2 = new List<PopulateTreeItems>();
        //    subSubItems2.Add(new PopulateTreeItems
        //    {
        //        Id = 221,
        //        Grade1 = 12,
        //        Grade2 = 13,
        //        Grade3 = 20,
        //        Weight = 2, MinGrade = 0, MaxGrade = 20,
        //        ParentId = 22,
        //        Text = "زیر منو 1_1",
        //        HasChild = false
        //    });
        //    subSubItems2.Add(new PopulateTreeItems
        //    {
        //        Id = 222,
        //        Grade1 = 12,
        //        Grade2 = 13,
        //        Grade3 = 20,
        //        Weight = 2, MinGrade = 0, MaxGrade = 20,
        //        ParentId = 22,
        //        Text = "زیر منو 1_2",
        //        HasChild = false
        //    });
        //    subItems2.Add(new PopulateTreeItems
        //    {
        //        Id = 22,
        //        Grade1 = 12,
        //        Grade2 = 13,
        //        Grade3 = 20,
        //        Weight = 2, MinGrade = 0, MaxGrade = 20,
        //        ParentId = 2,
        //        Text = "زیر منو 2",
        //        HasChild = true,
        //        SubItems = subSubItems2
        //    });
        //    PopulateTreeItems.Add(new PopulateTreeItems
        //    {
        //        Id = 2,
        //        Grade1 = 12,
        //        Grade2 = 13,
        //        Grade3 = 20,
        //        Weight = 2, MinGrade = 0, MaxGrade = 20,
        //        Text = "منو 2",
        //        HasChild = true,
        //        SubItems = subItems2
        //    });
        //    PopulateTreeItems.Add(new PopulateTreeItems
        //    {
        //        Id = 3,
        //        Grade1 = 12,
        //        Grade2 = 13,
        //        Grade3 = 20,
        //        Weight = 2, MinGrade = 0, MaxGrade = 20,
        //        Text = "منو 3",
        //        HasChild = false
        //    });
        //    var subItems4 = new List<PopulateTreeItems>();
        //    subItems4.Add(new PopulateTreeItems
        //    {
        //        Id = 41,
        //        Grade1 = 12,
        //        Grade2 = 13,
        //        Grade3 = 20,
        //        Weight = 2, MinGrade = 0, MaxGrade = 20,
        //        ParentId = 4,
        //        Text = "زیر منو 1",
        //        HasChild = false
        //    });
        //    var subSubItems4 = new List<PopulateTreeItems>();
        //    subSubItems4.Add(new PopulateTreeItems
        //    {
        //        Id = 421,
        //        Grade1 = 12,
        //        Grade2 = 13,
        //        Grade3 = 20,
        //        Weight = 2, MinGrade = 0, MaxGrade = 20,
        //        ParentId = 42,
        //        Text = "زیر منو 1_1",
        //        HasChild = false
        //    });
        //    subSubItems4.Add(new PopulateTreeItems
        //    {
        //        Id = 422,
        //        Grade1 = 12,
        //        Grade2 = 13,
        //        Grade3 = 20,
        //        Weight = 2, MinGrade = 0, MaxGrade = 20,
        //        ParentId = 42,
        //        Text = "زیر منو 1_2",
        //        HasChild = false
        //    });
        //    subItems4.Add(new PopulateTreeItems
        //    {
        //        Id = 42,
        //        Grade1 = 12,
        //        Grade2 = 13,
        //        Grade3 = 20,
        //        Weight = 2, MinGrade = 0, MaxGrade = 20,
        //        ParentId = 4,
        //        Text = "زیر منو 2",
        //        HasChild = true,
        //        SubItems = subSubItems4
        //    });
        //    subItems4.Add(new PopulateTreeItems
        //    {
        //        Id = 43,
        //        Grade1 = 12,
        //        Grade2 = 13,
        //        Grade3 = 20,
        //        Weight = 2,
        //        ParentId = 4,
        //        Text = "زیر منو 3",
        //        HasChild = false,
        //        ItemType = 1,
        //        QualityDs = qualityDs
        //    });
        //    PopulateTreeItems.Add(new PopulateTreeItems
        //    {
        //        Id = 4,
        //        Grade = 30,
        //        MinGrade = 20,
        //        MaxGrade = 60,
        //        Grade1 = 12,
        //        Grade2 = 13,
        //        Grade3 = 20,
        //        Weight = 2,
        //        Text = "منو 4",
        //        HasChild = true,
        //        SubItems = subItems4
        //    });
        //    return Json(PopulateTreeItems);
        //}
        [HttpGet]
        public ActionResult getTreeItems(int id)
        {
            var PopulateTreeItems = new List<PopulateTreeItems>();
            var qualityDs = new List<Quality>();
            qualityDs.Add(new Quality
            {
                Text = "بد",
                Grade = 1
            });
            qualityDs.Add(new Quality
            {
                Text = "خوب",
                Grade = 2
            });
            qualityDs.Add(new Quality
            {
                Text = "عالی",
                Grade = 3
            });
            PopulateTreeItems = db.EVFormItems.Where(x => x.EVFormRef == id).Select(a => new PopulateTreeItems
            {
                Id = (int)a.EvaluationFactorRef,
                ParentId = (int)a.EvaluationFactorRef,
                ItemType = Convert.ToInt16(a.EvaluationFactor.EvaluationFactorType),
                MinGrade = 1,
                MaxGrade=1,
                Grade = 1,
                Grade1 = 1,
                Grade2 = 2,
                Grade3 =(decimal) a.Score,                
                Text = a.EvaluationFactor.Title.Substring(0, 20),                
                HasChild = false,
                //HasChild = (db.EVFormItems.Where(p => p. == a.EvaluationFactorRef).Count() == 0 ? false : true),
                Weight = (int)a.WeightFactor,
                //SubItems = a.Children.Select(b => new PopulateTreeItems {
                //    Grade1 = 1,
                //    Grade2 = 2,
                //    Grade3 = 3,
                //    Id = b.EvaluationFactorId, Text = b.Title.Substring(0,40), ParentId = b.EvaluationFactorRef, HasChild = (db.EvaluationFactorTree.Where(p => p.EvaluationFactorRef == b.EvaluationFactorId).Count() == 0 ? false : true) }).Take(5).ToList()
                QualityDs =qualityDs
                
                
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
