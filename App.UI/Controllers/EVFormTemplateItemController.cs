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
    public class EVFormTemplateItemController : Controller
    {
        private static List<EVFormTemplateItemModel> AllItems;
        private readonly EvaluationContext db;
        public EVFormTemplateItemController(EvaluationContext d)
        {
            db = d;
            //if (AllItems == null)
            //{
            AllItems = new List<EVFormTemplateItemModel>();
            var a = db.EVFormTemplateItems;
            AllItems = a.ToList();


            // }
        }



        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult GetAllPaged(EVFormTemplateItemSearchModel model)
        {
          

            var select = db.EVFormTemplateItems.Include(i=>i.EVFormTemplate).Include(i=>i.EvaluationFactor).Where(w=> w.EVFormTemplateRef==model.EVFormTemplateId);         
            AllItems = JsonConvert.DeserializeObject<List<EVFormTemplateItemModel>>(JsonConvert.SerializeObject(select));

            var filtered = AllItems;

            if (model.Title != null)
                filtered = filtered.Where(x => x.Description.Contains(model.Title)).ToList();

    
 
            if (model.Description != null)
                filtered = filtered.Where(x => x.Description.Contains(model.Description)).ToList();
            PagedList<EVFormTemplateItemModel> result = new PagedList<EVFormTemplateItemModel>();
            result.Items = filtered.Skip((model.PageIndex * model.PageSize)).Take(model.PageSize).ToList();
            result.PageIndex = model.PageIndex;
            result.PageSize = model.PageSize;
            result.TotalItemsCount = filtered.Count;
            return Ok(result);
        }


        [HttpGet]
        public ActionResult GetById(int id)
        {
            var result = AllItems.Where(x => x.EVFormTemplateItemId == id).FirstOrDefault();
            return Ok(result);
        }


        [HttpPost]
        public ActionResult Create([FromBody]EVFormTemplateItemModel model)
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
        public ActionResult Edit([FromBody]EVFormTemplateItemModel model)
        {
            //validation
            var result = AllItems.Where(x => x.EVFormTemplateItemId == model.EVFormTemplateItemId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            result.EVFormTemplateRef = model.EVFormTemplateRef;
            result.EvaluationFactorRef = model.EvaluationFactorRef;
            result.State = model.State;
            result.Description = model.Description;
            db.Update(result);
            db.SaveChangesAsync();
            return Ok();
        }
        public ActionResult Delete([FromBody]EVFormTemplateItemModel model)
        {
            //validation
            var result = AllItems.Where(x => x.EVFormTemplateItemId == model.EVFormTemplateItemId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            db.Remove(result);
            db.SaveChangesAsync();
            return Ok();
        }
    }
}