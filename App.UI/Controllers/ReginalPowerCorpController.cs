using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.UI.Models;
using App.UI.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace App.UI.Controllers
{
    public class ReginalPowerCorpController : Controller
    {
        private static List<ReginalPowerCorpModel> AllItems;
        private static EvaluationContext db;
        public ReginalPowerCorpController(EvaluationContext d)
        {
            db = d;
            //if (AllItems == null)
            //{
                AllItems = new List<ReginalPowerCorpModel>();
                var a = db.ReginalPowerCorps;
                AllItems = a.ToList();


           // }
        }


        public ActionResult Get_Lov()
        {
            var result=db.ReginalPowerCorps.Select(a => new { id = a.ReginalPowerCorpId, name =a.Title});
            return Ok(result);
        }



        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetAllPaged(ReginalPowerCorpSearchModel model)
        {
            var filtered = AllItems;
            if (model.Title != null)
                filtered = filtered.Where(x => x.Title.Contains(model.Title)).ToList();
            if (model.Manager != null)
                filtered = filtered.Where(x => x.Manager.Contains(model.Manager)).ToList();
            PagedList<ReginalPowerCorpModel> result = new PagedList<ReginalPowerCorpModel>();
            result.Items = filtered.Skip((model.PageIndex * model.PageSize)).Take(model.PageSize).ToList();
            result.PageIndex = model.PageIndex;
            result.PageSize = model.PageSize;
            result.TotalItemsCount = filtered.Count;
            return Ok(result);
        }
        [HttpGet]
        public ActionResult GetById(int id)
        {
            var result = AllItems.Where(x => x.ReginalPowerCorpId == id).FirstOrDefault();
            return Ok(result);
        }
        [HttpPost]
        public ActionResult Create([FromBody]ReginalPowerCorpModel model)
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
        public ActionResult Edit([FromBody]ReginalPowerCorpModel model)
        {
            //validation
            var result = AllItems.Where(x => x.ReginalPowerCorpId == model.ReginalPowerCorpId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            result.Title = model.Title;
            result.Manager = model.Manager;
            result.ManagerMobile = model.ManagerMobile;
            result.Tel = model.Tel;
            result.Fax = model.Fax;
            result.Mail = model.Mail;
            result.MainAddress = model.MainAddress;
            result.SubAddress = model.SubAddress;
            result.RegisterCode = model.RegisterCode;
            result.State = model.State;
            result.Description = model.Description;
            db.Update(result);
            db.SaveChangesAsync();
            return Ok();
        }
        public ActionResult Delete([FromBody]ReginalPowerCorpModel model)
        {
            //validation
            var result = AllItems.Where(x => x.ReginalPowerCorpId == model.ReginalPowerCorpId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            db.Remove(result);
            db.SaveChangesAsync();
            return Ok();
        }
    }
}


