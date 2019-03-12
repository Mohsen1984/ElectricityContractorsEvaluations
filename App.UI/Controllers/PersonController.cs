using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using App.UI.Models;
using App.UI.Models.Common;
using Newtonsoft.Json;

namespace App.UI.Controllers
{
    public class PersonController : Controller
    {
        private static List<PersonModel> AllItems;
        private readonly EvaluationContext db;
        public PersonController(EvaluationContext d)
        {
            db = d;
            //if (AllItems == null)
            //{
            AllItems = new List<PersonModel>();
            //var a = db.Persons;
            //AllItems = a.ToList();


            // }
        }


        public ActionResult Get_Lov()
        {
            var result = db.Persons.Select(a => new { id = a.PersonId, name = a.FullName });
            return Ok(result);
        }



        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetAllPaged(PersonSearchModel model)
        {

            var select = db.Persons.Select(s => new {s.PersonId,s.FirstName, s.LastName, s.NationalID, s.MobileNo, s.Tel, s.FatherName, s.IDNumber, s.Email, s.Address, EducationalLevelTitle= s.EducationalLevel.Title, s.State,s.Description });         
            AllItems = JsonConvert.DeserializeObject<List<PersonModel>>(JsonConvert.SerializeObject(select));

            var filtered = AllItems;

            if (model.FirstName != null)
                filtered = filtered.Where(x => x.FirstName.Contains(model.FirstName)).ToList();

            if (model.LastName != null)
                filtered = filtered.Where(x => x.LastName.Contains(model.LastName)).ToList();
 
            if (model.Description != null)
                filtered = filtered.Where(x => x.Description.Contains(model.Description)).ToList();
            PagedList<PersonModel> result = new PagedList<PersonModel>();
            result.Items = filtered.Skip((model.PageIndex * model.PageSize)).Take(model.PageSize).ToList();
            result.PageIndex = model.PageIndex;
            result.PageSize = model.PageSize;
            result.TotalItemsCount = filtered.Count;
            return Ok(result);
        }
        [HttpGet]
        public ActionResult GetById(int id)
        {
            var result = AllItems.Where(x => x.PersonId == id).FirstOrDefault();
            return Ok(result);
        }
        [HttpPost]
        public ActionResult Create([FromBody]PersonModel model)
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
        public ActionResult Edit([FromBody]PersonModel model)
        {
            //validation
            var result = AllItems.Where(x => x.PersonId == model.PersonId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            result.FirstName = model.FirstName;
            result.LastName = model.LastName;
            result.NationalID = model.NationalID;
            result.MobileNo = model.MobileNo;
            result.Tel = model.Tel;
            result.FatherName = model.FatherName;
            result.IDNumber = model.IDNumber;
            result.Email = model.Email;
            result.Address = model.Address;
            result.EducationLevelRef = model.EducationLevelRef;
            result.State = model.State;
            result.Description = model.Description;
            db.Update(result);
            db.SaveChangesAsync();
            return Ok();
        }
        public ActionResult Delete([FromBody]PersonModel model)
        {
            //validation
            var result = AllItems.Where(x => x.PersonId == model.PersonId).FirstOrDefault();
            if (result == null)
                return BadRequest();
            db.Remove(result);
            db.SaveChangesAsync();
            return Ok();
        }
    }
}