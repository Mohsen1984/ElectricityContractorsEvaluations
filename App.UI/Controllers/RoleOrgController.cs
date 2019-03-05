using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using App.UI.Models;
using App.UI.Models.Common;

namespace App.UI.Controllers
{
    public class RoleOrgController : Controller
    {
        private static List<RoleOrgModel> AllItems;
        private readonly EvaluationContext db;
        public RoleOrgController(EvaluationContext d)
        {
            db = d;
            AllItems = new List<RoleOrgModel>();
            var a = db.RoleOrgs;
            AllItems = a.ToList();
        }


        public ActionResult Get_Lov()
        {
            var result = db.RoleOrgs.Select(a => new { id = a.RoleOrgId, name = a.Title });
            return Ok(result);
        }



        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetAllPaged(RoleOrgSearchModel model)
        {
            var filtered = AllItems;
            if (model.Title != null)
                filtered = filtered.Where(x => x.Title.Contains(model.Title)).ToList();
            if (model.Description != null)
                filtered = filtered.Where(x => x.Description.Contains(model.Description)).ToList();
            PagedList<RoleOrgModel> result = new PagedList<RoleOrgModel>();
            result.Items = filtered.Skip((model.PageIndex * model.PageSize)).Take(model.PageSize).ToList();
            result.PageIndex = model.PageIndex;
            result.PageSize = model.PageSize;
            result.TotalItemsCount = filtered.Count;
            return Ok(result);
        }
        [HttpGet]
        public ActionResult GetById(int id)
        {
            var result = AllItems.Where(x => x.RoleOrgId == id).FirstOrDefault();
            return Ok(result);
        }
        [HttpPost]
        public ActionResult Create([FromBody]RoleOrgModel model)
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
        public ActionResult Edit([FromBody]RoleOrgModel model)
        {
            //validation
            var result = AllItems.Where(x => x.RoleOrgId == model.RoleOrgId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            result.Title = model.Title;
            result.State = model.State;
            result.Description = model.Description;
            db.Update(result);
            db.SaveChangesAsync();
            return Ok();
        }
        public ActionResult Delete([FromBody]RoleOrgModel model)
        {
            //validation
            var result = AllItems.Where(x => x.RoleOrgId == model.RoleOrgId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            db.Remove(result);
            db.SaveChangesAsync();
            return Ok();
        }
    }
}