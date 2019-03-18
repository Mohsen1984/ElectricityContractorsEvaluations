using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using App.UI.Models;
using App.UI.Models.Common;

namespace App.UI.Controllers
{
    public class EducationalLevelController : Controller
    {
        private static List<EducationalLevelModel> AllItems;
        private readonly EvaluationContext db;
        public EducationalLevelController(EvaluationContext d)
        {
            db = d;
            //if (AllItems == null)
            //{
            AllItems = new List<EducationalLevelModel>();
            var a = db.EducationalLevels;
            AllItems = a.ToList();


            // }
        }

        [HttpGet]
        public ActionResult Get_Lov()
        {
            var result = db.EducationalLevels.Select(a => new { id = a.EducationalLevelId, name = a.Title });
            return Ok(result);
        }



        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetAllPaged(EducationalLevelSearchModel model)
        {
            var filtered = AllItems;
            if (model.Title != null)
                filtered = filtered.Where(x => x.Title.Contains(model.Title)).ToList();
            if (model.Description != null)
                filtered = filtered.Where(x => x.Description.Contains(model.Description)).ToList();
            PagedList<EducationalLevelModel> result = new PagedList<EducationalLevelModel>();
            result.Items = filtered.Skip((model.PageIndex * model.PageSize)).Take(model.PageSize).ToList();
            result.PageIndex = model.PageIndex;
            result.PageSize = model.PageSize;
            result.TotalItemsCount = filtered.Count;
            return Ok(result);
        }
        [HttpGet]
        public ActionResult GetById(int id)
        {
            var result = AllItems.Where(x => x.EducationalLevelId == id).FirstOrDefault();
            return Ok(result);
        }
        [HttpPost]
        public ActionResult Create([FromBody]EducationalLevelModel model)
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
        public ActionResult Edit([FromBody]EducationalLevelModel model)
        {
            //validation
            var result = AllItems.Where(x => x.EducationalLevelId == model.EducationalLevelId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            result.Title = model.Title;
            result.State = model.State;
            result.Description = model.Description;
            db.Update(result);
            db.SaveChangesAsync();
            return Ok();
        }
        public ActionResult Delete([FromBody]EducationalLevelModel model)
        {
            //validation
            var result = AllItems.Where(x => x.EducationalLevelId == model.EducationalLevelId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            db.Remove(result);
            db.SaveChangesAsync();
            return Ok();
        }
    }
}
