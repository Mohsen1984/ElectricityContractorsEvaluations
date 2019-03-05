using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.UI.Models;
using App.UI.Models.Common;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace App.UI.Controllers
{
    public class ServiceTreeController : Controller
    {
        private static List<ServiceTreeModel> AllItems;
        private readonly EvaluationContext db;
        public ServiceTreeController(EvaluationContext d)
        {
            db = d;
            //AllItems = new List<ProjectTreeModel>();
            //AllItems = db.ProjectTrees.ToList();

        }

        public ActionResult Get_Lov()
        {
            var result = db.ServiceTrees.Select(a => new { id = a.ServiceTreeId, name = a.Title });
            return Ok(result);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateAsChild(int id, string level)
        {
            //var result = AllItems.Where(x => x.ProjectTreeId == id).Select( s=>new { s.Title ,s.ReginalPowerCorpRef,s.ProjectTreeRef, ReginalPowerCorpTitle=s.ReginalPowerCorp.Title, ProjectParent=s.ProjectTree.Title,s.LevelCode,s.Code,s.IsTemplate,s.State});
            new ServiceTreeModel();
            var result = new ServiceTreeModel { ServiceTemplateTreeRef = id, Level = level };


            return Ok(result);
        }

        [HttpGet]
        public ActionResult GetAllPaged(ServiceTreeSearchModel model)
        {

            var select = db.ProjectTrees.Select(s => new { s.Title, s.State, s.ProjectTreeId,ProjectParent=s.ProjectTreechild.Title, s.ReginalPowerCorpRef, s.Code,s.Description,s.Level, s.LevelCode, ReginalPowerCorpTitle = s.ReginalPowerCorp.Title });
            AllItems = JsonConvert.DeserializeObject<List<ServiceTreeModel>>(JsonConvert.SerializeObject(select));

            var filtered = AllItems;
            if (model.Title != null)
                filtered = filtered.Where(x => x.Title.Contains(model.Title)).ToList();
            if (model.Description != null)
                filtered = filtered.Where(x => x.Description.Contains(model.Description)).ToList();
            PagedList<ServiceTreeModel> result = new PagedList<ServiceTreeModel>();

          
            var a=filtered.Skip((model.PageIndex * model.PageSize)).Take(model.PageSize).ToList();
         
            result.Items = filtered.Skip((model.PageIndex * model.PageSize)).Take(model.PageSize).ToList();
            result.PageIndex = model.PageIndex;
            result.PageSize = model.PageSize;
            result.TotalItemsCount = filtered.Count;
            return Ok(result);
        }


        /// <summary>
        /// لیست خدمات سطح =2 . خدماتی که حودشان ریشه درخت باشند . پدر نداشته باشند
        /// </summary>
        /// <used>
        /// ProjectTree/Home
        /// </used>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetsParentsService(ServiceTreeSearchModel model)
        {

            var select = db.ServiceTrees.Where(w=>w.ProjectTreeRef==model.ProjectTreeRef & w.ServiceTemplateTreeRef==null & w.Level=="2");
            AllItems = JsonConvert.DeserializeObject<List<ServiceTreeModel>>(JsonConvert.SerializeObject(select));

            var filtered = AllItems;
            if (model.Title != null)
                filtered = filtered.Where(x => x.Title.Contains(model.Title)).ToList();
            if (model.Description != null)
                filtered = filtered.Where(x => x.Description.Contains(model.Description)).ToList();
            PagedList<ServiceTreeModel> result = new PagedList<ServiceTreeModel>();


            var a = filtered.Skip((model.PageIndex * model.PageSize)).Take(model.PageSize).ToList();

            result.Items = filtered.Skip((model.PageIndex * model.PageSize)).Take(model.PageSize).ToList();
            result.PageIndex = model.PageIndex;
            result.PageSize = model.PageSize;
            result.TotalItemsCount = filtered.Count;
            return Ok(result);
        }




        [HttpGet]
        public ActionResult GetById(int id)
        {
                   
            var result = db.ServiceTrees.Where(x => x.ServiceTreeId== id).FirstOrDefault();            

            return Ok(result);
        }



        [HttpPost]
        public ActionResult Create([FromBody]ServiceTreeModel model)
        {
            //validation
            
            if (ModelState.IsValid)
            {
                model.LevelCode = model.LevelCode + "-" + model.Level;
                if (model.ServiceTemplateTreeRef == 0)
                    model.ServiceTemplateTreeRef = null;
                db.Add(model);
                db.SaveChanges();

            }
            return Ok();
        }
        [HttpPost]
        public ActionResult Edit([FromBody]ServiceTreeModel model)
        {
            //validation

            var result = db.ServiceTrees.Where(x => x.ServiceTreeId == model.ServiceTreeId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            //result = model;
            result.Title = model.Title;
            result.ReginalPowerCorpRef = model.ReginalPowerCorpRef;
            result.ServiceTemplateTreeRef = model.ServiceTemplateTreeRef;
            result.Level = model.Level;
           
            result.Code = model.Code;            
            result.State = model.State;
            result.Description = model.Description;
               
            db.Update(result);
            db.SaveChanges();

            //db.Entry(model).State = EntityState.Modified;
            //var affectedRow = db.SaveChanges();

            return Ok();
        }
        public ActionResult Delete([FromBody]ServiceTreeModel model)
        {
            //validation
            var result = AllItems.Where(x => x.ServiceTreeId== model.ServiceTreeId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            db.Remove(result);
            db.SaveChanges();
            return Ok();
        }

        public ActionResult EntityServiceTreeNodes(ServiceTreeNodeModel node)
        {
            List<ServiceTreeNodeModel> result;
            if (node.Id == null)
            {
                result = db.ServiceTrees.Where(w=> w.ProjectInfoRef== node.ProjectInfoRef & w.ServiceTreeRef==null).Select(x =>
                  new ServiceTreeNodeModel
                  {
                      Id = x.ServiceTreeId,
                      Text = x.Title,
                      HasChild = (db.ServiceTrees.Where(p=> p.ServiceTreeRef == x.ServiceTreeId).Count()==0 ? false : true),
                      ParentId = null,
                      Level = x.Level,
                      LevelCode = x.LevelCode
                  }).ToList();
            }
            else
            {
                result = db.ServiceTrees.Where(x => x.ServiceTreeRef == node.Id ).Select(x => new ServiceTreeNodeModel
                {
                    Id = x.ServiceTreeId,
                    Text = x.Title,
                    HasChild = (db.ServiceTrees.Where(p => p.ServiceTreeRef == x.ServiceTreeId).Count() == 0 ? false : true),
                    ParentId = null,
                    Level = x.Level,
                    LevelCode = x.LevelCode
                }).ToList();
            }
            return Json(result);
        }
    }
}


