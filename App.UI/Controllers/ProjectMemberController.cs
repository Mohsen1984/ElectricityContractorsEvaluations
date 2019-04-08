using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using App.UI.Models;
using App.UI.Models.Common;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace App.UI.Controllers
{
    public class ProjectMemberController : Controller
    {
        private static List<ProjectMemberModel> AllItems;
        private readonly EvaluationContext db;
        public ProjectMemberController(EvaluationContext d)
        {
            db = d;
            //if (AllItems == null)
            //{
            AllItems = new List<ProjectMemberModel>();
            //var a = db.Persons;
            //AllItems = a.ToList();


            // }
        }


        public ActionResult Get_Lov()
        {
            var result = db.ProjectMembers.Select(a => new { id = a.ProjectMemberID, name = a.PersonFullName });
            return Ok(result);
        }


        [HttpGet]
        public ActionResult Initialize(int projectInfoRef, int ServiceTreeRef)
        {
            //var result = AllItems.Where(x => x.ProjectTreeId == id).Select( s=>new { s.Title ,s.ReginalPowerCorpRef,s.ProjectTreeRef, ReginalPowerCorpTitle=s.ReginalPowerCorp.Title, ProjectParent=s.ProjectTree.Title,s.LevelCode,s.Code,s.IsTemplate,s.State});
          
            var result = new ProjectMemberModel { ProjectInfoRef = projectInfoRef,ServiceTreeRef = ServiceTreeRef };


            return Ok(result);
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetAllPaged(ProjectMemberSearchModel model)
        {


            ///*https://docs.microsoft.com/en-us/ef/core/querying/related-data*/
            var select = db.ProjectMembers.Include(i => i.RoleOrgs).Include(i => i.Persone).Include(i => i.ServiceTreechild).Where(w => 1 == 1 && w.ServiceTreeRef==model.ServiceTreeRef);


            if (model.Title != null)
                select = select.Where(w => w.Title.Contains(model.Title));

            if (model.Description != null)
                select = select.Where(x => x.Description.Contains(model.Description));

            PagedList<ProjectMemberModel> result = new PagedList<ProjectMemberModel>();
            result.TotalItemsCount = select.Count();
            if (result.TotalItemsCount != 0)
            {
                result.Items = select.Skip((model.PageIndex * model.PageSize)).Take(model.PageSize).ToList();
            }
            else
            {
                result.Items = select.ToList();
            }
            result.PageIndex = model.PageIndex;
            result.PageSize = model.PageSize;

            return Ok(result);
        }
        [HttpGet]
        public ActionResult GetById(int id)
        {
            var result = db.ProjectMembers.Where(x => x.ProjectMemberID == id).FirstOrDefault();
            return Ok(result);
        }
        [HttpPost]
        public ActionResult Create([FromBody]ProjectMemberModel model)
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
        public ActionResult Edit([FromBody]ProjectMemberModel model)
        {
            //validation
            var result = db.ProjectMembers.Where(x => x.ProjectMemberID == model.ProjectMemberID).FirstOrDefault();
            if (result == null)
                return BadRequest();
            result.State = model.State;
            result.Title = model.Title;
            result.ReginalPowerCorpRef = model.ReginalPowerCorpRef;
            //result.ServiceTreeRef = model.ServiceTreeRef;
            result.PersoneRef = model.PersoneRef;
            result.RoleOrgsRef = model.RoleOrgsRef;
            //result.ProjectInfoRef = model.ProjectInfoRef;
            result.Description = model.Description;
            db.Update(result);
            db.SaveChangesAsync();
            return Ok();
        }
        public ActionResult Delete([FromBody]ProjectMemberModel model)
        {
            //validation
            var result = db.ProjectMembers.Where(x => x.ProjectMemberID == model.ProjectMemberID).FirstOrDefault();
            if (result == null)
                return BadRequest();
            db.Remove(result);
            db.SaveChangesAsync();
            return Ok();
        }
    }
}