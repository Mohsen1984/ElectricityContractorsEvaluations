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
    public class EvaluationFactorTreeController : Controller
    {
        private static List<EvaluationFactorTreeModel> AllItems;
        private readonly EvaluationContext db;
        public EvaluationFactorTreeController(EvaluationContext d)
        {
            db = d;
            //if (AllItems == null)
            //{
                AllItems = new List<EvaluationFactorTreeModel>();
                var a = db.EvaluationFactorTrees;
                AllItems = a.ToList();


           // }
        }


        [HttpGet]
        public ActionResult Get_Lov()
        {
            var result = db.EvaluationFactorTrees.Select(a => new { id = a.EvaluationFactorId, name = a.Title });
            return Ok(result);
        }

        [HttpGet]
        public ActionResult Get_LovEvaluationFactorType()
        {
            var result = new[] { new { id = "0", name = "کمی" }, new { id = "1", name = "کیفی" } };

            return Ok(result);
        }

        [HttpGet]
        public ActionResult CreateAsChild(int id)
        {
            //var result = AllItems.Where(x => x.ProjectTreeId == id).Select( s=>new { s.Title ,s.ReginalPowerCorpRef,s.ProjectTreeRef, ReginalPowerCorpTitle=s.ReginalPowerCorp.Title, ProjectParent=s.ProjectTree.Title,s.LevelCode,s.Code,s.IsTemplate,s.State});
            new EvaluationFactorTreeModel();
            var result = new EvaluationFactorTreeModel { EvaluationFactorRef = id };


            return Ok(result);
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetAllPaged(EvaluationFactorTreeSearchModel model)
        {
    

            var select = db.EvaluationFactorTrees.Select(s => new { s.EvaluationFactorId, s.Title,s.EvaluationFactorRef, EvaluationFactorParentName = s.EvaluationFactorParent.Title,s.EvaluationFactorTypeName,s.EvaluationFactorType, s.State, s.Description });
            AllItems = JsonConvert.DeserializeObject<List<EvaluationFactorTreeModel>>(JsonConvert.SerializeObject(select));
            var filtered = AllItems;

            if (model.Title != null)
                filtered = filtered.Where(x => x.Title.Contains(model.Title)).ToList();
            if (model.ProjectNo != null)
                filtered = filtered.Where(x => x.Description.Contains(model.ProjectNo)).ToList();
            PagedList<EvaluationFactorTreeModel> result = new PagedList<EvaluationFactorTreeModel>();
            result.Items = filtered.Skip((model.PageIndex * model.PageSize)).Take(model.PageSize).ToList();
            result.PageIndex = model.PageIndex;
            result.PageSize = model.PageSize;
            result.TotalItemsCount = filtered.Count;
            return Ok(result);
        }
        [HttpGet]
        public ActionResult GetById(int id)
        {
            var result = db.EvaluationFactorTrees.Where(x => x.EvaluationFactorId == id).FirstOrDefault();
            return Ok(result);
        }
        [HttpPost]
        public ActionResult Create([FromBody]EvaluationFactorTreeModel model)
        {
            //validation
            
            if (ModelState.IsValid)
            {
                if (model.EvaluationFactorRef == 0)
                    model.EvaluationFactorRef = null;
                db.Add(model);

                db.SaveChanges();

            }
            return Ok();
        }
        [HttpPost]
        public ActionResult Edit([FromBody]EvaluationFactorTreeModel model)
        {
            //validation
            var result = AllItems.Where(x => x.EvaluationFactorId == model.EvaluationFactorId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            
           
            
            result.Title = model.Title;
            result.EvaluationFactorType = model.EvaluationFactorType;
            result.EvaluationFactorRef = model.EvaluationFactorRef;
            result.State = model.State;
            result.Description = model.Description;
            db.Update(result);
            db.SaveChanges();
            return Ok();
        }
        public ActionResult Delete([FromBody]EvaluationFactorTreeModel model)
        {
            //validation
            var result = AllItems.Where(x => x.EvaluationFactorId == model.EvaluationFactorId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            db.Remove(result);
            db.SaveChanges();
            return Ok();
        }


        public ActionResult EntityTreeNodesEvaluationFactor(EvaluationFactorTreeNodeModel node)
        {
            List<EvaluationFactorTreeNodeModel> result;
            if (node.Id == null)
            {
                result = db.EvaluationFactorTrees.Where(w => w.EvaluationFactorRef ==null).Select(x =>
                      new EvaluationFactorTreeNodeModel
                      {
                          Id =x.EvaluationFactorId,
                          Text = x.Title,
                          HasChild = (db.EvaluationFactorTrees.Where(p => p.EvaluationFactorRef == x.EvaluationFactorId).Count() == 0 ? false : true),
                          ParentId = null,
           
                      }).ToList();
            }
            else
            {
                result = db.EvaluationFactorTrees.Where(x => x.EvaluationFactorRef == node.Id).Select(x => new EvaluationFactorTreeNodeModel
                {
                    Id = x.EvaluationFactorId,
                    Text = x.Title,
                    HasChild = (db.EvaluationFactorTrees.Where(p => p.EvaluationFactorRef == x.EvaluationFactorId).Count() == 0 ? false : true),
                    ParentId = null,

                }).ToList();
            }
            return Json(result);
        }
    }
}


