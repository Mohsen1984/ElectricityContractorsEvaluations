using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using App.UI.Models;
using App.UI.Models.Common;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace App.UI.Controllers
{
    public class EVFormController : Controller
    {
        private static List<EVFormModel> AllItems;
        private readonly EvaluationContext db;
        public EVFormController(EvaluationContext d)
        {
            db = d;
            //if (AllItems == null)
            //{
            //AllItems = new List<EVFormModel>();
            //var a = db.EVForms;
            //AllItems = a.ToList();


            //}
        }



        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult GetAllPaged(EVFormSearchModel model)
        {

             ///*https://docs.microsoft.com/en-us/ef/core/querying/related-data*/
            var select = db.EVForms.Include(i=> i.EvaluationPeriod).Include(i => i.EvaluatorRole).Include(i => i.EvaluatedRole).Include(i=>i.EvaluatorPersone).Include(i=>i.EvaluatedPersone).Include(i => i.ProjectTree).Include(i => i.ReginalPowerCorp).Where(w=>1==1);
  

            if (model.Title != null)
                select = select.Where(w => w.Title.Contains(model.Title));

            if (model.Description != null)
                select = select.Where(x => x.Description.Contains(model.Description));
            
            PagedList<EVFormModel> result = new PagedList<EVFormModel>();
            result.TotalItemsCount = select.Count();
            result.Items = select.Skip((model.PageIndex * model.PageSize)).Take(model.PageSize).ToList();
            result.PageIndex = model.PageIndex;
            result.PageSize = model.PageSize;
           
            return Ok(result);
        }


        [HttpGet]
        public ActionResult GetById(int id)
        {
            var result = db.EVForms.Where(x => x.EVFormId == id).FirstOrDefault();
            return Ok(result);
        }


        [HttpPost]
        public ActionResult Create([FromBody]EVFormModel model)
        {
            //validation

            if (ModelState.IsValid)
            {
              //  model.FromDate = Business.Base.GetMiladiDate(model.FromDatePersian);
              //  model.ToDate = Business.Base.GetMiladiDate(model.ToDatePersian);
                db.Add(model);
                db.SaveChangesAsync();

            }
            return Ok();
        }


        [HttpPost]
        public ActionResult Edit([FromBody]EVFormModel model)
        {
            //validation
            var result = db.EVForms.Where(x => x.EVFormId == model.EVFormId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            result.Title = model.Title;
            result.EvaluatorRoleRef = model.EvaluatorRoleRef;
            result.EvaluatedRoleRef = model.EvaluatedRoleRef;
            result.ProjectTreeRef = model.ProjectTreeRef;
            result.ReginalPowerCorpRef = model.ReginalPowerCorpRef;
            result.State = model.State;
            result.Description = model.Description;
            db.Update(result);
            db.SaveChangesAsync();
            return Ok();
        }
        public ActionResult Delete([FromBody]EVFormModel model)
        {
            //validation
            var result = db.EVForms.Where(x => x.EVFormId == model.EVFormId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            db.Remove(result);
            db.SaveChangesAsync();
            return Ok();
        }
    }
}