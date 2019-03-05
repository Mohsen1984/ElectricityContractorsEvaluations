using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.UI.Models;
using App.UI.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace App.UI.Controllers
{
    public class ProjectInfoController : Controller
    {
        private static List<ProjectInfoModel> AllItems;
        private readonly EvaluationContext db;
        public ProjectInfoController(EvaluationContext d)
        {
            db = d;
            
               
        
            //if (AllItems == null)
            //{
                AllItems = new List<ProjectInfoModel>();
                var a = db.ProjectInfos;
                AllItems = a.ToList();


           // }
        }


        [HttpGet]
        public ActionResult Get_Lov()
        {
            var result = db.ProjectInfos.Select(a => new { id = a.ProjectId, name = a.Title });
            return Ok(result);
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetAllPaged(ProjectInfoSearchModel model)
        {
            var select = db.ProjectInfos.Include(i => i.ReginalPowerCorp).Include(i=> i.ProjectTree);
            AllItems = JsonConvert.DeserializeObject<List<ProjectInfoModel>>(JsonConvert.SerializeObject(select));
            var filtered = AllItems;

            if (model.ProjectTreeRef != 0)
                filtered = filtered.Where(x => x.ProjectTreeRef==model.ProjectTreeRef).ToList();

            if (model.ServiceTemplateTreeRef > -1)
                filtered = filtered.Where(x => x.ServiceTemplateTreeRef == model.ServiceTemplateTreeRef).ToList();

            if (model.Title != null)
                filtered = filtered.Where(x => x.Title.Contains(model.Title)).ToList();

            if (model.ProjectNo != null)
                filtered = filtered.Where(x => x.Description.Contains(model.ProjectNo)).ToList();

            PagedList<ProjectInfoModel> result = new PagedList<ProjectInfoModel>();
            result.Items = filtered.Skip((model.PageIndex * model.PageSize)).Take(model.PageSize).ToList();
            result.PageIndex = model.PageIndex;
            result.PageSize = model.PageSize;
            result.TotalItemsCount = filtered.Count;
            return Ok(result);
        }
        [HttpGet]
        public ActionResult GetById(int id)
        {
            var result = AllItems.Where(x => x.ProjectId == id).FirstOrDefault();
            return Ok(result);
        }
        [HttpPost]
        public ActionResult Create([FromBody]ProjectInfoModel model)
        {
            //validation

            if (ModelState.IsValid)
            {
                using (TransactionScope tranScope = new TransactionScope())
                {
                    db.Add(model);
                    db.SaveChanges();
                   

                    InserTempTree(model.ServiceTemplateTreeRef, model.ProjectId);
                tranScope.Complete();
            }
        }
            return Ok();
        }

        public void InserTempTree(int? ServiceTemplateTreeRef,int ProjectInfoRef,int parentServiceRef=0)
        {
            
            if (parentServiceRef == 0)
            {
                var i = new ServiceTreeModel();
                var ModservicetempTree = db.ServiceTemplateTrees.Where(w => w.ServiceTemplateTreeId == ServiceTemplateTreeRef).First();

                i.Title = ModservicetempTree.Title;
                i.ServiceTemplateTreeRef = ModservicetempTree.ServiceTemplateTreeId;
                i.ProjectInfoRef = ProjectInfoRef;
                db.Add(i);
                db.SaveChanges();

                int serviceId = i.ServiceTreeId;
                InserTempTree(ServiceTemplateTreeRef, ProjectInfoRef, serviceId);
            }
            else
            {
                var ModservicetempTree = db.ServiceTemplateTrees.Where(w => w.ServiceTemplateTreeRef == ServiceTemplateTreeRef).ToList();
                foreach (var a in ModservicetempTree)
                {
                    var i = new ServiceTreeModel();

                    i.Title = a.Title;
                    i.ServiceTreeRef = parentServiceRef;
                    i.ServiceTemplateTreeRef = a.ServiceTemplateTreeId;
                    i.ProjectInfoRef = ProjectInfoRef;
                    db.Add(i);
                    db.SaveChanges();

                    int serviceId = i.ServiceTreeId;
                    ServiceTemplateTreeRef = a.ServiceTemplateTreeId;

                    InserTempTree(ServiceTemplateTreeRef, ProjectInfoRef, serviceId);
                }
            }

        }


        [HttpPost]
        public ActionResult Edit([FromBody]ProjectInfoModel model)
        {
            //validation
            var result = AllItems.Where(x => x.ProjectId == model.ProjectId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            
           
            
            result.Title = model.Title;
            result.ProjectNo = model.ProjectNo;
            result.ProjectTreeRef = model.ProjectTreeRef;
            result.ReginalPowerCorpRef = model.ReginalPowerCorpRef;
            result.State = model.State;
            result.Description = model.Description;
            db.Update(result);
            db.SaveChanges();
            return Ok();
        }
        public ActionResult Delete([FromBody]ProjectInfoModel model)
        {
            //validation
            var result = AllItems.Where(x => x.ProjectId == model.ProjectId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            db.Remove(result);
            db.SaveChanges();
            return Ok();
        }
    }
}


