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
    public class EVFormTemplateController : Controller
    {
        private static List<EVFormTemplateModel> AllItems;
        private readonly EvaluationContext db;
        public EVFormTemplateController(EvaluationContext d)
        {
            db = d;
            //if (AllItems == null)
            //{
            AllItems = new List<EVFormTemplateModel>();
            var a = db.EVFormTemplates;
            AllItems = a.ToList();


            // }
        }



        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult GetAllPaged(EVFormTemplateSearchModel model)
        {

             ///*https://docs.microsoft.com/en-us/ef/core/querying/related-data*/
            var select = db.EVFormTemplates.Include(i=> i.EvaluationPeriod).Include(i => i.EvaluatedRole).Include(i => i.EvaluatorRole).Include(i => i.ProjectTree).Include(i => i.ReginalPowerCorp);//.Select(s=> new {s.EVFormTemplateId,s.EvaluatorRole,EvaluatorRoleTitle=s.EvaluatorRole.Title,s.EvaluatedRoleRef,EvaluatedRoleTitle=s.EvaluatedRole.Title,s.ReginalPowerCorpRef,ReginalPowerCorpTitle=s.ReginalPowerCorp.Title,s.ProjectTreeRef,ProjectTreeTitle=s.ProjectTree.Title });         
            AllItems = JsonConvert.DeserializeObject<List<EVFormTemplateModel>>(JsonConvert.SerializeObject(select));

            var filtered = AllItems;

            if (model.Title != null)
                filtered = filtered.Where(x => x.Description.Contains(model.Title)).ToList();

    
 
            if (model.Description != null)
                filtered = filtered.Where(x => x.Description.Contains(model.Description)).ToList();
            PagedList<EVFormTemplateModel> result = new PagedList<EVFormTemplateModel>();
            result.Items = filtered.Skip((model.PageIndex * model.PageSize)).Take(model.PageSize).ToList();
            result.PageIndex = model.PageIndex;
            result.PageSize = model.PageSize;
            result.TotalItemsCount = filtered.Count;
            return Ok(result);
        }


        [HttpGet]
        public ActionResult GetById(int id)
        {
            var result = AllItems.Where(x => x.EVFormTemplateId == id).FirstOrDefault();
            return Ok(result);
        }


        [HttpPost]
        public ActionResult Create([FromBody]EVFormTemplateModel model)
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
        public ActionResult Edit([FromBody]EVFormTemplateModel model)
        {
            //validation
            var result = AllItems.Where(x => x.EVFormTemplateId == model.EVFormTemplateId).FirstOrDefault();
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
        public ActionResult Delete([FromBody]EVFormTemplateModel model)
        {
            //validation
            var result = AllItems.Where(x => x.EVFormTemplateId == model.EVFormTemplateId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            db.Remove(result);
            db.SaveChangesAsync();
            return Ok();
        }
    }
}