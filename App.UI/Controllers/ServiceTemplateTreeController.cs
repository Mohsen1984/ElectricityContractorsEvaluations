using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using App.UI.Models;
using App.UI.Models.Common;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace App.UI.Controllers
{
    public class ServiceTemplateTreeController : Controller
    {
        private static List<ServiceTemplateTreeModel> AllItems;
        private readonly EvaluationContext db;
        public ServiceTemplateTreeController(EvaluationContext d)
        {
            db = d;
            //AllItems = new List<ProjectTreeModel>();
            //AllItems = db.ProjectTrees.ToList();

        }

        public ActionResult Get_Lov()
        {
            var result = db.ServiceTemplateTrees.Select(a => new { id = a.ServiceTemplateTreeId, name = a.Title });
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
            new ServiceTemplateTreeModel();
            var result = new ServiceTemplateTreeModel { ServiceTemplateTreeRef = id, Level = level };


            return Ok(result);
        }

        [HttpGet]
        public ActionResult GetAllPaged(ServiceTemplateTreeSearchModel model)
        {

            var select = db.ProjectTrees.Select(s => new { s.Title, s.State, s.ProjectTreeId,ProjectParent=s.ProjectTreechild.Title, s.ReginalPowerCorpRef, s.Code,s.Description,s.Level, s.LevelCode, ReginalPowerCorpTitle = s.ReginalPowerCorp.Title });
            AllItems = JsonConvert.DeserializeObject<List<ServiceTemplateTreeModel>>(JsonConvert.SerializeObject(select));

            var filtered = AllItems;
            if (model.Title != null)
                filtered = filtered.Where(x => x.Title.Contains(model.Title)).ToList();
            if (model.Description != null)
                filtered = filtered.Where(x => x.Description.Contains(model.Description)).ToList();
            PagedList<ServiceTemplateTreeModel> result = new PagedList<ServiceTemplateTreeModel>();

          
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
        public ActionResult GetsParentsService(ServiceTemplateTreeSearchModel model)
        {

            AllItems = db.ServiceTemplateTrees.Where(w=>w.ProjectTreeRef==model.ProjectTreeRef & w.ServiceTemplateTreeRef==null & w.Level=="2").ToList();
            //AllItems = JsonConvert.DeserializeObject<List<ServiceTemplateTreeModel>>(JsonConvert.SerializeObject(select));

            var filtered = AllItems;
            if (model.Title != null)
                filtered = filtered.Where(x => x.Title.Contains(model.Title)).ToList();
            if (model.Description != null)
                filtered = filtered.Where(x => x.Description.Contains(model.Description)).ToList();
            PagedList<ServiceTemplateTreeModel> result = new PagedList<ServiceTemplateTreeModel>();


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
                   
            var result = db.ServiceTemplateTrees.Where(x => x.ServiceTemplateTreeId== id).FirstOrDefault();            

            return Ok(result);
        }

        public ServiceTemplateTreeModel GetByIdd(int? id)
        {

            var result = db.ServiceTemplateTrees.Where(x => x.ServiceTemplateTreeId == id).FirstOrDefault();

            return result;
        }



        [HttpPost]
        public ActionResult Create([FromBody]ServiceTemplateTreeModel model)
        {
            //validation


            if (ModelState.IsValid)
            {
                model.LevelCode = model.LevelCode + "-" + model.Level;
                if (model.ServiceTemplateTreeRef == 0)
                    model.ServiceTemplateTreeRef = null;
                db.Add(model);
                db.SaveChanges();
               // Thread.Sleep(100);
            }
            return Ok();
        }

        [HttpPost]
        public ActionResult Edit([FromBody]ServiceTemplateTreeModel model)
        {
            //validation

            var result = db.ServiceTemplateTrees.Where(x => x.ServiceTemplateTreeId == model.ServiceTemplateTreeId).FirstOrDefault();
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
        public ActionResult Delete([FromBody]ServiceTemplateTreeModel model)
        {
            //validation
            var result = AllItems.Where(x => x.ServiceTemplateTreeId== model.ServiceTemplateTreeId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            db.Remove(result);
            db.SaveChanges();
            return Ok();
        }

        public ActionResult EntityServiceTemplateTreeNodes(ServiceTemplateTreeNodeModel node)
        {
            List<ServiceTemplateTreeNodeModel> result;
            if (node.Id == null)
            {
                result = db.ServiceTemplateTrees.Where(w=> w.ServiceTemplateTreeRef== null & w.ProjectTreeRef==node.ProjectRef).Select(x =>
                  new ServiceTemplateTreeNodeModel
                  {
                      Id = x.ServiceTemplateTreeId,
                      Text = x.Title,
                      HasChild = (db.ServiceTemplateTrees.Where(p=> p.ServiceTemplateTreeRef == x.ServiceTemplateTreeId).Count()==0 ? false : true),
                      ParentId = null,
                      Level = x.Level,
                      LevelCode = x.LevelCode
                  }).ToList();
            }
            else
            {
                result = db.ServiceTemplateTrees.Where(x => x.ServiceTemplateTreeRef == node.Id ).Select(x => new ServiceTemplateTreeNodeModel
                {
                    Id = x.ServiceTemplateTreeId,
                    Text = x.Title,
                    HasChild = (db.ServiceTemplateTrees.Where(p => p.ServiceTemplateTreeRef == x.ServiceTemplateTreeId).Count() == 0 ? false : true),
                    ParentId = null,
                    Level = x.Level,
                    LevelCode = x.LevelCode
                }).ToList();
            }
            return Json(result);
        }
    }
}


