using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.UI.Models;
using App.UI.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace App.UI.Controllers
{
    public class ExternalOrganizationController : Controller
    {
        private static List<ExternalOrganizationModel> AllItems;
        private readonly EvaluationContext db;
        public ExternalOrganizationController(EvaluationContext d)
        {
            db = d;
            //if (AllItems == null)
            //{
                //AllItems = new List<ExternalOrganizationModel>();
                //var a = db.ExternalOrganizations;
                //AllItems = a.ToList();


           // }
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetAllPaged(ExternalOrganizationSearchModel model)
        {
            var select = db.ExternalOrganizations.Include(i=>i.ExternalOrgType).Where(w=>1==1);
            if (model.Title != null)
                select = select.Where(w => w.Description.Contains(model.Title));

            if (model.Description != null)
                select = select.Where(x => x.Description.Contains(model.Description));

            PagedList<ExternalOrganizationModel> result = new PagedList<ExternalOrganizationModel>();
            result.TotalItemsCount = select.Count();
            result.Items = select.Skip((model.PageIndex * model.PageSize)).Take(model.PageSize).ToList();
            result.PageIndex = model.PageIndex;
            result.PageSize = model.PageSize;

            return Ok(result);
        }
        [HttpGet]
        public ActionResult GetById(int id)
        {
            var result = db.ExternalOrganizations.Where(x => x.ExternalOrganizationId == id).FirstOrDefault();
            return Ok(result);
        }
        [HttpPost]
        public ActionResult Create([FromBody]ExternalOrganizationModel model)
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
        public ActionResult Edit([FromBody]ExternalOrganizationModel model)
        {
            //validation
            var result = db.ExternalOrganizations.Where(x => x.ExternalOrganizationId == model.ExternalOrganizationId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            result.Title = model.Title;
            result.ExternalOrgTypeRef = model.ExternalOrgTypeRef;
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
            db.SaveChanges();
            return Ok();
        }
        public ActionResult Delete([FromBody]ExternalOrganizationModel model)
        {
            //validation
            var result = db.ExternalOrganizations.Where(x => x.ExternalOrganizationId == model.ExternalOrganizationId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            db.Remove(result);
            db.SaveChanges();
            return Ok();
        }
    }
}


