using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.UI.Models;
using App.UI.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace App.UI.Controllers
{
    public class ExternalOrgTypeController : Controller
    {
        private static List<ExternalOrgTypeModel> AllItems;
        private static EvaluationContext db;
        public ExternalOrgTypeController(EvaluationContext d)
        {
            db = d;
            //if (AllItems == null)
            //{
                AllItems = new List<ExternalOrgTypeModel>();
                var a = db.ExternalOrgTypes;
                AllItems = a.ToList();


           // }
        }


        [HttpGet]
        public ActionResult Get_Lov()
        {
            var result = db.ExternalOrgTypes.Select(a => new { id = a.ExternalOrgTypeId, name = a.Title });
            return Ok(result);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAllPaged(ExternalOrgTypeSearchModel model)
        {
            var filtered = AllItems;
            if (model.Title != null)
                filtered = filtered.Where(x => x.Title.Contains(model.Title)).ToList();
            if (model.Description != null)
                filtered = filtered.Where(x => x.Description.Contains(model.Description)).ToList();
            PagedList<ExternalOrgTypeModel> result = new PagedList<ExternalOrgTypeModel>();
            result.Items = filtered.Skip((model.PageIndex * model.PageSize)).Take(model.PageSize).ToList();
            result.PageIndex = model.PageIndex;
            result.PageSize = model.PageSize;
            result.TotalItemsCount = filtered.Count;
            return Ok(result);
        }
        [HttpGet]
        public ActionResult GetById(int id)
        {
            var result = AllItems.Where(x => x.ExternalOrgTypeId == id).FirstOrDefault();
            return Ok(result);
        }
        [HttpPost]
        public ActionResult Create([FromBody]ExternalOrgTypeModel model)
        {
            //validation
            
            if (ModelState.IsValid)
            {
                db.Add(model);
                db.SaveChanges();

            }
            return Ok();
        }
        [HttpPost]
        public ActionResult Edit([FromBody]ExternalOrgTypeModel model)
        {
            //validation
            var result = AllItems.Where(x => x.ExternalOrgTypeId == model.ExternalOrgTypeId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            result.Title = model.Title;
            result.Code = model.Code;
            result.Description = model.Description;
            db.Update(result);
            db.SaveChanges();
            return Ok();
        }
        public ActionResult Delete([FromBody]ExternalOrgTypeModel model)
        {
            //validation
            var result = AllItems.Where(x => x.ExternalOrgTypeId == model.ExternalOrgTypeId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            db.Remove(result);
            db.SaveChanges();
            return Ok();
        }
    }
}


