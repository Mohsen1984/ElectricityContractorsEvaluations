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
    public class ProjectTreeController : Controller
    {
        private static List<ProjectTreeModel> AllItems;
        private readonly EvaluationContext db;
        public ProjectTreeController(EvaluationContext d)
        {
            db = d;
            //AllItems = new List<ProjectTreeModel>();
            //AllItems = db.ProjectTrees.ToList();

        }


        public ActionResult Get_Lov()
        {
            var result = db.ProjectTrees.Select(a => new { id = a.ProjectTreeId, name = a.Title });
            return Ok(result);
        }

        public ActionResult Get_Lov_FirstLevel()
        {
            var result = db.ProjectTrees.Where(a => a.ProjectTreeRef==null).Select(a => new { id = a.ProjectTreeId, name = a.Title });
            return Ok(result);
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetAllPaged(ProjectTreeSearchModel model)
        {

            var select = db.ProjectTrees.Select(s => new { s.Title, s.State, s.ProjectTreeId,ProjectParent=s.ProjectTreechild.Title, s.ReginalPowerCorpRef, s.Code,s.Description,s.Level, s.LevelCode, ReginalPowerCorpTitle = s.ReginalPowerCorp.Title });
            AllItems = JsonConvert.DeserializeObject<List<ProjectTreeModel>>(JsonConvert.SerializeObject(select));

            var filtered = AllItems;// db.ProjectTrees.Select(x => new { x.ProjectTreeId, x.ReginalPowerCorp.Title, x.Description, x.LevelCode}).ToList();
            if (model.Title != null)
                filtered = filtered.Where(x => x.Title.Contains(model.Title)).ToList();
            if (model.Description != null)
                filtered = filtered.Where(x => x.Description.Contains(model.Description)).ToList();
            PagedList<ProjectTreeModel> result = new PagedList<ProjectTreeModel>();

           // var test=filtered.Select(a => a.ReginalPowerCorp.Title).Skip((model.PageIndex * model.PageSize)).Take(model.PageSize).ToList();
            var a=filtered.Skip((model.PageIndex * model.PageSize)).Take(model.PageSize).ToList();
            //a.ForEach(fi => {var sap =  });
            //    preferences.ForEach(preference =>
            //    {
            //        var sap = sapReadOnlyItems.FirstOrDefault(s => s.ItemValue = preference.ItemValue);
            //        preference.Name = (sap != null) ? sap.Name : string.Empty;
            //    });
           // result=filtered.Skip((model.PageIndex * model.PageSize)).Take(model.PageSize).ToList();


           // PagedList<ProjectTreeModel> result = new PagedList<ProjectTreeModel>();
            result.Items = filtered.Skip((model.PageIndex * model.PageSize)).Take(model.PageSize).ToList();
            result.PageIndex = model.PageIndex;
            result.PageSize = model.PageSize;
            result.TotalItemsCount = filtered.Count;
            return Ok(result);
        }
        [HttpGet]
        public ActionResult GetById(int id)
        {
            //var result = AllItems.Where(x => x.ProjectTreeId == id).Select( s=>new { s.Title ,s.ReginalPowerCorpRef,s.ProjectTreeRef, ReginalPowerCorpTitle=s.ReginalPowerCorp.Title, ProjectParent=s.ProjectTree.Title,s.LevelCode,s.Code,s.IsTemplate,s.State});
            
            var result = db.ProjectTrees.Where(x => x.ProjectTreeId == id).FirstOrDefault();
            

            return Ok(result);
        }


        [HttpGet]
        public ActionResult CreateAsChild(int id,string level)
        {
            //var result = AllItems.Where(x => x.ProjectTreeId == id).Select( s=>new { s.Title ,s.ReginalPowerCorpRef,s.ProjectTreeRef, ReginalPowerCorpTitle=s.ReginalPowerCorp.Title, ProjectParent=s.ProjectTree.Title,s.LevelCode,s.Code,s.IsTemplate,s.State});
            var result=new ProjectTreeModel();
            if (id!=-1)
            {
                 result = new ProjectTreeModel { ProjectTreeRef = id, Level = level };
            }

            


            return Ok(result);
        }

        [HttpPost]
        public ActionResult Create([FromBody]ProjectTreeModel model)
        {
            //validation
            
            if (ModelState.IsValid)
            {
                //db.Entry(model).State = EntityState.Added;
                model.LevelCode = model.LevelCode + "-" + model.Level;
                if(model.ProjectTreeRef ==0)
                {
                    model.ProjectTreeRef = null;
                }
                db.Add(model);
                db.SaveChanges();

            }
            return Ok();
        }
        [HttpPost]
        public ActionResult Edit([FromBody]ProjectTreeModel model)
        {
            //validation

            var result = db.ProjectTrees.Where(x => x.ProjectTreeId == model.ProjectTreeId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            //result = model;
            result.Title = model.Title;
            result.ReginalPowerCorpRef = model.ReginalPowerCorpRef;
            result.ProjectTreeRef = model.ProjectTreeRef;
            result.Level = model.Level;
           
            result.Code = model.Code;
            result.IsTemplate = model.IsTemplate;
            result.State = model.State;
            result.Description = model.Description;
            
           
            
            
            db.Update(result);
            db.SaveChanges();

            //db.Entry(model).State = EntityState.Modified;
            //var affectedRow = db.SaveChanges();

            return Ok();
        }
        public ActionResult Delete([FromBody]ProjectTreeModel model)
        {
            //validation
            var result = AllItems.Where(x => x.ProjectTreeId == model.ProjectTreeId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            db.Remove(result);
            db.SaveChanges();
            return Ok();
        }

        public ActionResult EntityTreeNodesProjects(ProjectTreeNodeModel node)
        {
            List<ProjectTreeNodeModel> result;
            if (node.Id == null)
            {
                result = db.ProjectTrees.Where(w=> w.ProjectTreeRef==null & w.Level=="1" ).Select(x =>
                  new ProjectTreeNodeModel
                  {
                      Id = x.ProjectTreeId,
                      Text = x.Title,
                      HasChild = (db.ProjectTrees.Where(p=> p.ProjectTreeRef==x.ProjectTreeId & p.Level == "1").Count()==0 ? false : true),
                      ParentId = null,
                      Level = x.Level,
                      LevelCode = x.LevelCode
                  }).ToList();
            }
            else
            {
                result = db.ProjectTrees.Where(x => x.ProjectTreeRef == node.Id & x.Level == "1").Select(x =>  new ProjectTreeNodeModel
                {
                    Id = x.ProjectTreeId,
                    Text = x.Title,
                    HasChild = (db.ProjectTrees.Where(p => p.ProjectTreeRef == x.ProjectTreeId & p.Level == "1").Count() == 0 ? false : true),
                    ParentId = null,
                    Level = x.Level,
                    LevelCode = x.LevelCode
                }).ToList();
            }
            return Json(result);
        }


        public ActionResult EntityTreeNodesServices(ProjectTreeNodeModel node)
        {
            List<ProjectTreeNodeModel> result;
            if (node.Id == null)
            {
                result = db.ProjectTrees.Where(w => w.ProjectTreeRef == null & w.Level == "1").Select(x =>
                      new ProjectTreeNodeModel
                      {
                          Id = x.ProjectTreeId,
                          Text = x.Title,
                          HasChild = (db.ProjectTrees.Where(p => p.ProjectTreeRef == x.ProjectTreeId).Count() == 0 ? false : true),
                          ParentId = null,
                          Level = x.Level,
                          LevelCode = x.LevelCode
                      }).ToList();
            }
            else
            {
                result = db.ProjectTrees.Where(x => x.ProjectTreeRef == node.Id & x.Level == "1").Select(x => new ProjectTreeNodeModel
                {
                    Id = x.ProjectTreeId,
                    Text = x.Title,
                    HasChild = (db.ProjectTrees.Where(p => p.ProjectTreeRef == x.ProjectTreeId).Count() == 0 ? false : true),
                    ParentId = null,
                    Level = x.Level,
                    LevelCode = x.LevelCode
                }).ToList();
            }
            return Json(result);
        }
    }
}


