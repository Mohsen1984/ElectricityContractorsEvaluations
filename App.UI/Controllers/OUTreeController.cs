using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.UI.Models;
using App.UI.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace App.UI.Controllers
{
    public class OUTreeController : Controller
    {
        private static List<OUTreeModel> AllItems;
        private readonly EvaluationContext db;
        public OUTreeController(EvaluationContext d)
        {
            db = d;
            //if (AllItems == null)
            //{
                AllItems = new List<OUTreeModel>();
                var a = db.OUTrees;
                AllItems = a.ToList();


           // }
        }


        [HttpGet]
        public ActionResult Get_Lov()
        {
            var result = db.OUTrees.Select(a => new { id = a.OUTreeId, name = a.Title });
            return Ok(result);
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetAllPaged(OUTreeSearchModel model)
        {
            var select = db.OUTrees.Select(s => new { s.OUTreeId, s.Title, s.ReginalPowerCorpRef,s.OUTreeRef, OuTreeParent = s.OUTree.Title , ReginalPowerCorpTitle = s.ReginalPowerCorp.Title, s.Code, s.State, s.Description });
            AllItems = JsonConvert.DeserializeObject<List<OUTreeModel>>(JsonConvert.SerializeObject(select));
            var filtered = AllItems;

            if (model.Title != null)
                filtered = filtered.Where(x => x.Title.Contains(model.Title)).ToList();
            if (model.Description != null)
                filtered = filtered.Where(x => x.Description.Contains(model.Description)).ToList();
            PagedList<OUTreeModel> result = new PagedList<OUTreeModel>();
            result.Items = filtered.Skip((model.PageIndex * model.PageSize)).Take(model.PageSize).ToList();
            result.PageIndex = model.PageIndex;
            result.PageSize = model.PageSize;
            result.TotalItemsCount = filtered.Count;
            return Ok(result);
        }
        [HttpGet]
        public ActionResult GetById(int id)
        {
            var result = AllItems.Where(x => x.OUTreeId == id).FirstOrDefault();
            return Ok(result);
        }
        [HttpPost]
        public ActionResult Create([FromBody]OUTreeModel model)
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
        public ActionResult Edit([FromBody]OUTreeModel model)
        {
            //validation
            var result = AllItems.Where(x => x.OUTreeId == model.OUTreeId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            
            result.ReginalPowerCorpRef = model.ReginalPowerCorpRef;
            result.OUTreeRef = model.OUTreeRef;
            result.Title = model.Title;
            result.Code = model.Code;
            result.State = model.State;
            result.Description = model.Description;
            db.Update(result);
            db.SaveChanges();
            return Ok();
        }
        public ActionResult Delete([FromBody]OUTreeModel model)
        {
            //validation
            var result = AllItems.Where(x => x.OUTreeId == model.OUTreeId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            db.Remove(result);
            db.SaveChanges();
            return Ok();
        }
    }
}


