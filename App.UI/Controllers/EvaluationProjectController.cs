using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using App.UI.Models;
using App.UI.Models.Common;
using Newtonsoft.Json;

namespace App.UI.Controllers
{
    public class EvaluationProjectController : Controller
    {
        private static List<EvaluationProjectModel> AllItems;
        private readonly EvaluationContext db;

        public EvaluationProjectController(EvaluationContext d)
        {
            db = d;
    
            //if (AllItems == null)
            //{
            AllItems = new List<EvaluationProjectModel>();
            //var a = db.EvaluationProjects;
           // AllItems = a.ToList();


            // }
        }


        public ActionResult Get_Lov()
        {
            var result = db.Persons.Select(a => new { id = a.PersonId, name = a.FullName });
            return Ok(result);
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult GetAllPaged(EvaluationProjectSearchModel model)
        {
       
            var select = db.EvaluationProjects.Where(w=>w.PeriodRef==model.PeriodId).Select(s=>new { s.EvaluationProjectId,s.ReginalPowerCorpRef, ReginalPowerCorpTitle = s.ReginalPowerCorp.Title, s.PeriodRef, PeriodTitle = s.Period.Title,s.ProjectTreeRef, ProjectTreeTitle = s.ProjectTree.Title });         
            AllItems = JsonConvert.DeserializeObject<List<EvaluationProjectModel>>(JsonConvert.SerializeObject(select));

            var filtered = AllItems;

            if (model.Title != null)
              //  filtered = filtered.Where(x => x..Contains(model.Title)).ToList();

            if (model.Description != null)
                filtered = filtered.Where(x => x.Description.Contains(model.Description)).ToList();
            PagedList<EvaluationProjectModel> result = new PagedList<EvaluationProjectModel>();
            result.Items = filtered.Skip((model.PageIndex * model.PageSize)).Take(model.PageSize).ToList();
            result.PageIndex = model.PageIndex;
            result.PageSize = model.PageSize;
            result.TotalItemsCount = filtered.Count;
            return Ok(result);
        }


        [HttpGet]
        public ActionResult GetById(int id)
        {
            var result = db.EvaluationProjects.Where(x => x.EvaluationProjectId == id).FirstOrDefault();
            return Ok(result);
        }


        [HttpPost]
        public ActionResult Create([FromBody]EvaluationProjectModel model)
        {
            //validation

            if (ModelState.IsValid)
            {
                db.Add(model);
                db.SaveChangesAsync();

            }
            return Ok();
        }


        [HttpPost]
        public ActionResult Edit([FromBody]EvaluationProjectModel model)
        {
            //validation
            var result = db.EvaluationProjects.Where(x => x.EvaluationProjectId == model.EvaluationProjectId).FirstOrDefault();
            if (result == null)
                return BadRequest();

            result.ReginalPowerCorpRef = model.ReginalPowerCorpRef;
            result.ProjectTreeRef = model.ProjectTreeRef;
            result.PeriodRef = model.PeriodRef;
            result.State = model.State;
            result.Description = model.Description;

            db.Update(result);
            db.SaveChangesAsync();
            return Ok();
        }

        public ActionResult Delete([FromBody]EvaluationProjectModel model)
        {
            //validation
            var result = db.EvaluationProjects.Where(x => x.EvaluationProjectId == model.EvaluationProjectId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            db.Remove(result);
            db.SaveChangesAsync();
            return Ok();
        }
    }
}