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
    public class EvaluationPeriodController : Controller
    {
        private static List<EvaluationPeriodModel> AllItems;
        private static EvaluationContext db;
        public EvaluationPeriodController(EvaluationContext d)
        {
            db = d;
            //if (AllItems == null)
            //{
            AllItems = new List<EvaluationPeriodModel>();
            var a = db.EvaluationPeriods;
            AllItems = a.ToList();


            // }
        }


        public ActionResult Get_Lov()
        {
            var result = db.EvaluationPeriods.Select(a => new { id = a.PeriodId, name = a.Title });
            return Ok(result);
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult GetAllPaged(EvaluationPeriodSearchModel model)
        {
           DateTime ddd= Business.Base.GetMiladiDate("1397/11/02");
            var select = db.EvaluationPeriods.Select(s => new {s.PeriodId,s.Title,s.ToDate,s.FromDate,s.FromDatePersian,s.ToDatePersian, ReginalPowerCorpTitle=s.ReginalPowerCorp.Title,s.ReginalPowerCorpRef, s.State,s.Description });         
            AllItems = JsonConvert.DeserializeObject<List<EvaluationPeriodModel>>(JsonConvert.SerializeObject(select));

            var filtered = AllItems;

            if (model.Title != null)
                filtered = filtered.Where(x => x.Title.Contains(model.Title)).ToList();

    
 
            if (model.Description != null)
                filtered = filtered.Where(x => x.Description.Contains(model.Description)).ToList();
            PagedList<EvaluationPeriodModel> result = new PagedList<EvaluationPeriodModel>();
            result.Items = filtered.Skip((model.PageIndex * model.PageSize)).Take(model.PageSize).ToList();
            result.PageIndex = model.PageIndex;
            result.PageSize = model.PageSize;
            result.TotalItemsCount = filtered.Count;
            return Ok(result);
        }


        [HttpGet]
        public ActionResult GetById(int id)
        {
            var result = AllItems.Where(x => x.PeriodId == id).FirstOrDefault();
            return Ok(result);
        }


        [HttpPost]
        public ActionResult Create([FromBody]EvaluationPeriodModel model)
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
        public ActionResult Edit([FromBody]EvaluationPeriodModel model)
        {
            //validation
            var result = AllItems.Where(x => x.PeriodId == model.PeriodId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            result.Title = model.Title;
            result.ToDate = model.ToDate;
            result.FromDate = model.FromDate;
            result.ReginalPowerCorpRef = model.ReginalPowerCorpRef;
            result.State = model.State;
            result.Description = model.Description;
            db.Update(result);
            db.SaveChangesAsync();
            return Ok();
        }
        public ActionResult Delete([FromBody]EvaluationPeriodModel model)
        {
            //validation
            var result = AllItems.Where(x => x.PeriodId == model.PeriodId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            db.Remove(result);
            db.SaveChangesAsync();
            return Ok();
        }
    }
}