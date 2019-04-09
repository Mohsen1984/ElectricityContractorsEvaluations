using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


using App.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Paint.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using App.UI.Models.Common;

namespace App.UI.Controllers
{


    public class _Status
    {
        public int Id;
        public string Status;
        public string translatedStatus;
    }
    public class _Box
    {
        public int Id;
        public string Title;
        public int Number;
        public string BoxClass;
        public string DsUrl;
        public string Icon;
    }

    public class GridDs<T>
    {
        public List<T> Items;
        public int PageIndex;
        public int PageSize;
    }


 
    public class TaskListController : Controller
    {
        enum StatusKey
        {
            Not_Started = 0,
            In_Progress = 5,
            Completed = 10

        }
        private  Dictionary<StatusKey, _Status> status = new Dictionary<StatusKey, _Status>
        {
            { StatusKey.Not_Started, new _Status { Id = 0, Status = "Not_Started", translatedStatus = "اقدام نشده" } },
            { StatusKey.In_Progress, new _Status { Id = 5, Status = "In_Progress", translatedStatus = "در دست اقدام" } },
            { StatusKey.Completed, new _Status { Id = 10, Status = "Completed", translatedStatus = "اقدام شده" } }
        };

        private static List<TaskListModel> AllItems;
        private readonly EvaluationContext db;
        public TaskListController(EvaluationContext d)
        {
            db = d;
            if (AllItems == null)
            {
            AllItems = new List<TaskListModel>();
            //000var a = db.TaskLists.Include(i => i.ProjectInfo).Include(i=>i.EvaluationPeriod);
            //AllItems = a.ToList();


             }
        }

        [HttpPost]
        public ActionResult AddTask([FromBody] AddTaskModel task)
        {
            TaskListModel newtask = new TaskListModel();
            newtask.Title = task.Title;
            newtask.WfInstanceID = task.WfInstanceID;
            newtask.FormLink = task.FormLink;
            newtask.DueDate = task.DueDate;
            newtask.SenderProjectMemberRef = task.SenderProjectMemberRef;
            newtask.ReciverProjectMemberRef = task.ReciverProjectMemberRef;
            newtask.Priority = task.Priority;
            newtask.ProjectInfoRef = task.ProjectInfoRef;
            newtask.EvaluationPeriodRef = task.EvaluationPeriodRef;

            db.TaskLists.Add(newtask);

            return Json(newtask.TaskId);
        }

        [HttpGet]
        public ActionResult GetAllPaged(TaskListSearchModel model)
        {


            return Ok(GetTasklist(model));
        }


        protected PagedList<TaskListModel> GetTasklist(TaskListSearchModel model)
        {
            AllItems = db.TaskLists.Include(i => i.ProjectInfo).Include(i => i.EvaluationPeriod)
                .Include(i=>i.SenderProjectMember).ThenInclude(ti=>ti.Persone)
                .Include(i => i.SenderProjectMember).ThenInclude(ti=>ti.RoleOrgs)

                .Include(i => i.ReciverProjectMember).ThenInclude(ti => ti.Persone)
                .Include(i => i.ReciverProjectMember).ThenInclude(ti => ti.RoleOrgs).Where(w=>w.EvaluationPeriod.State==1)
                .ToList();


            var filtered = AllItems;

            if (model.Title != null)
                filtered = filtered.Where(x => x.Title.Contains(model.Title)).ToList();
            if(model.OuteCome != null )
                filtered = filtered.Where(w => w.OutCome==model.OuteCome).ToList();
            if (model.ProjectInfo != null)
                filtered = filtered.Where(w => w.ProjectInfo.Title.Contains(model.ProjectInfo)).ToList();

            //if (model.Description != null)
            //    filtered = filtered.Where(x => x.Description.Contains(model.Description)).ToList();
            PagedList<TaskListModel> result = new PagedList<TaskListModel>();
            result.Items = filtered.Skip((model.PageIndex * model.PageSize)).Take(model.PageSize).ToList();
            result.PageIndex = model.PageIndex;
            result.PageSize = model.PageSize;
            result.TotalItemsCount = filtered.Count;
            return result;
        }

        public IActionResult Index()
        {
            return View();
        }


        public ActionResult Get_LovStatuses()
        {
            List<_Status> Statuses = new List<_Status>();
            Statuses.Add(new _Status
            {
                Id = (status[StatusKey.In_Progress]).Id,
                Status = (status[StatusKey.In_Progress]).Status,
                translatedStatus = (status[StatusKey.In_Progress]).translatedStatus
            });

            Statuses.Add(new _Status
            {
                Id = (status[StatusKey.Completed]).Id,
                Status = (status[StatusKey.Completed]).Status,
                translatedStatus = (status[StatusKey.Completed]).translatedStatus
            });
            Statuses.Add(new _Status
            {
                Id = (status[StatusKey.Not_Started]).Id,
                Status = (status[StatusKey.Not_Started]).Status,
                translatedStatus = (status[StatusKey.Not_Started]).translatedStatus
            });
            return Json(Statuses);
        }

        public ActionResult GetBoxes()
        {
            List<_Box> Boxes = new List<_Box>();

            ////( 0 شروع نشده / 5 در حال بررسی / 10 تکمیل شده)  

         
            Boxes.Add(new _Box
            {
                Id = (status[StatusKey.Not_Started]).Id,
                Number = AllItems.Where(w => w.OutCome == 0).Count(),
                BoxClass = "redBox",
                Title = (status[StatusKey.Not_Started]).translatedStatus,
                DsUrl = "TaskList/GetRedBoxes",
                Icon = "fa-play"
            });
            Boxes.Add(new _Box
            {
                Id = (status[StatusKey.In_Progress]).Id,
                Number = AllItems.Where(w => w.OutCome == 5).Count(),
                BoxClass = "blueBox",
                Title = (status[StatusKey.In_Progress]).translatedStatus,
                DsUrl = "TaskList/GetBlueBoxes",
                Icon = "fa-pause"
            });


            Boxes.Add(new _Box
            {
                Id = (status[StatusKey.Completed]).Id,
                Number = AllItems.Where(w => w.OutCome == 10).Count(),
                BoxClass = "greenBox",
                Title = (status[StatusKey.Completed]).translatedStatus,
                DsUrl = "TaskList/GetGreenBoxes",
                Icon = "fa-stop"
            });



           
            return Json(Boxes);
        }

        public ActionResult GetRedBoxes(TaskListSearchModel model)
        {



            var actions = new List<App.UI.Models._Action>();

            model.OuteCome = (int)StatusKey.Not_Started;
            var CurrentTaskList = GetTasklist(model);// db.TaskLists.Where(w => w.OutCome == (int)StatusKey.Not_Started).DefaultIfEmpty().FirstOrDefault<TaskListModel>();



            if (CurrentTaskList != null)
            {
                actions.Add(new App.UI.Models._Action
                {
                    Id = 1,
                    Title = "new",
                    TranslatedTitle = "مشاهده گردش",
                    Link = "/Person",
                    Icon = "fa-star"
                });
                actions.Add(new App.UI.Models._Action
                {
                    Id = 2,
                    Title = "action1",
                    TranslatedTitle = "برگشت به نفر قبل",
                    Link = "/Person",
                    Icon = "fa-tasks"
                });

                Parallel.ForEach(CurrentTaskList.Items, i => { i.Actions = actions; });
                
               
            }

            return Json(CurrentTaskList);

      
        }

        public ActionResult GetBlueBoxes(TaskListSearchModel model)
        {

            var actions = new List<App.UI.Models._Action>();
       

            model.OuteCome = (int)StatusKey.In_Progress;
            var CurrentTaskList = GetTasklist(model);//db.TaskLists.Where(w => w.OutCome == (int)StatusKey.In_Progress).DefaultIfEmpty().First<TaskListModel>();

            if (CurrentTaskList != null)
            {
                actions.Add(new App.UI.Models._Action
                {
                    Id = 1,
                    Title = "new",
                    TranslatedTitle = "مشاهده گردش",
                    Link = "/Person",
                    Icon = "fa-star"
                });
                actions.Add(new App.UI.Models._Action
                {
                    Id = 2,
                    Title = "action1",
                    TranslatedTitle = "برگشت به نفر قبل",
                    Link = "/Person",
                    Icon = "fa-tasks"
                });
                Parallel.ForEach(CurrentTaskList.Items, i => { i.Actions = actions; });

            }


            return Json(CurrentTaskList);
        }

        public ActionResult GetGreenBoxes(TaskListSearchModel model)
        {


            var actions = new List<App.UI.Models._Action>();
         

            model.OuteCome = (int)StatusKey.Completed;
            var CurrentTaskList = GetTasklist(model);// db.TaskLists.Where(w => w.OutCome == (int)StatusKey.Completed).DefaultIfEmpty().First<TaskListModel>();

            if (CurrentTaskList != null)
            {
                actions.Add(new App.UI.Models._Action
                {
                    Id = 1,
                    Title = "new",
                    TranslatedTitle = "مشاهده گردش",
                    Link = "/Person",
                    Icon = "fa-star"
                });
                actions.Add(new App.UI.Models._Action
                {
                    Id = 2,
                    Title = "action1",
                    TranslatedTitle = "برگشت به نفر قبل",
                    Link = "/Person",
                    Icon = "fa-tasks"
                });
                Parallel.ForEach(CurrentTaskList.Items, i => { i.Actions = actions; });

            }

            return Json(CurrentTaskList);

           
        }


        //Using In Search

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}