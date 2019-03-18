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
    public class ContractInfoController : Controller
    {
        private static List<ContractInfoModel> AllItems;
        private readonly EvaluationContext db;
        public ContractInfoController(EvaluationContext d)
        {
            db = d;
            //if (AllItems == null)
            //{
                AllItems = new List<ContractInfoModel>();
                var a = db.ContractInfos;
                AllItems = a.ToList();


           // }
        }


        [HttpGet]
        public ActionResult Get_Lov()
        {
            var result = db.ContractInfos.Select(a => new { id = a.ContractId, name = a.Title });
            return Ok(result);
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetAllPaged(ContractInfoSearchModel model)
        {
            var select = db.ContractInfos.Select(s => new { s.ContractId, s.Title,s.ContractNo, ProjectTreeTitle = s.ProjectTree.Title,s.ProjectTreeRef, ReginalPowerCorpTitle = s.ReginalPowerCorp.Title,s.ReginalPowerCorpRef, s.State, s.Description });
            AllItems = JsonConvert.DeserializeObject<List<ContractInfoModel>>(JsonConvert.SerializeObject(select));
            var filtered = AllItems;

            if (model.Title != null)
                filtered = filtered.Where(x => x.Title.Contains(model.Title)).ToList();
            if (model.ContractNo != null)
                filtered = filtered.Where(x => x.Description.Contains(model.ContractNo)).ToList();
            PagedList<ContractInfoModel> result = new PagedList<ContractInfoModel>();
            result.Items = filtered.Skip((model.PageIndex * model.PageSize)).Take(model.PageSize).ToList();
            result.PageIndex = model.PageIndex;
            result.PageSize = model.PageSize;
            result.TotalItemsCount = filtered.Count;
            return Ok(result);
        }
        [HttpGet]
        public ActionResult GetById(int id)
        {
            var result = AllItems.Where(x => x.ContractId == id).FirstOrDefault();
            return Ok(result);
        }
        [HttpPost]
        public ActionResult Create([FromBody]ContractInfoModel model)
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
        public ActionResult Edit([FromBody]ContractInfoModel model)
        {
            //validation
            var result = AllItems.Where(x => x.ContractId == model.ContractId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            
           
            
            result.Title = model.Title;
            result.ContractNo = model.ContractNo;
            result.ProjectTreeRef = model.ProjectTreeRef;
            result.ReginalPowerCorpRef = model.ReginalPowerCorpRef;
            result.State = model.State;
            result.Description = model.Description;
            db.Update(result);
            db.SaveChanges();
            return Ok();
        }
        public ActionResult Delete([FromBody]ContractInfoModel model)
        {
            //validation
            var result = AllItems.Where(x => x.ContractId == model.ContractId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            db.Remove(result);
            db.SaveChanges();
            return Ok();
        }
    }
}


