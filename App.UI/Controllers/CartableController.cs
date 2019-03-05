using System;
using System.Collections.Generic;
using System.Diagnostics;
using App.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Paint.Models;

namespace Paint.Controllers
{
    public class _Action
    {
        public int Id;
        public string Title;
        public string TranslatedTitle;
        public string Link;
        public string Icon;
    }
    public class _Task
    {
        public int Id;
        public string Title;
        public string DueDate;
        public int Priority;
        public string CreatedBy;
        public string CreatedDate;
        public string FormLink;
        public _Status Status;
        public string Description;
        public List<_Action> Actions;
    }
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
    public class CartableController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public ActionResult GetBoxes()
        {
            List<_Box> Boxes = new List<_Box>();
            Boxes.Add(new _Box
            {
                Id = 1,
                Number = 23,
                BoxClass = "redBox",
                Title = "New",
                DsUrl = "Cartable/GetRedBoxes",
                Icon = "fa-play"
            });
            Boxes.Add(new _Box
            {
                Id = 2,
                Number = 1,
                BoxClass = "greenBox",
                Title = "Done",
                DsUrl = "Cartable/GetGreenBoxes",
                Icon = "fa-stop"
            });
            Boxes.Add(new _Box
            {
                Id = 3,
                Number = 5,
                BoxClass = "blueBox",
                Title = "In Progress",
                DsUrl = "Cartable/GetBlueBoxes",
                Icon = "fa-pause"
            });
            Boxes.Add(new _Box
            {
                Id = 4,
                Number = 5,
                BoxClass = "orangeBox",
                Title = "In Progress",
                DsUrl = "Cartable/GetBlueBoxes",
                Icon = "fa-asterisk"
            });
            Boxes.Add(new _Box
            {
                Id = 5,
                Number = 5,
                BoxClass = "purpleBox",
                Title = "In Progress",
                DsUrl = "Cartable/GetBlueBoxes",
                Icon = "fa-star"
            });
            return Json(Boxes);
        }

        public ActionResult GetRedBoxes()
        {
            var actions = new List<_Action>();
            actions.Add(new _Action
            {
                Id = 1,
                Title = "new",
                TranslatedTitle = "عملیات 1",
                Link = "/Person",
                Icon = "fa-star"
            });
            actions.Add(new _Action
            {
                Id = 2,
                Title = "action1",
                TranslatedTitle = "عملیات 2",
                Link = "/Person",
                Icon = "fa-tasks"
            });

            var ds = new GridDs<_Task>
            {
                Items = new List<_Task>(),
                PageIndex = 0,
                PageSize = 10
            };
            ds.Items.Add(new _Task
            {
                Id = 1,
                Title = "New",
                DueDate = "2018-08-19",
                Priority = 3,
                CreatedBy = "علی پورنعمت",
                CreatedDate = "2018-08-19",
                FormLink = "/Person",
                Status = new _Status
                {
                    Id = 3,
                    Status = "Not Started",
                    translatedStatus = "شروع نشده"
                },
                Description = "این یک تست است",
                Actions = actions
            });
            return Json(ds);
        }

        public ActionResult GetBlueBoxes()
        {

            var actions = new List<_Action>();
            actions.Add(new _Action
            {
                Id = 1,
                Title = "new",
                TranslatedTitle = "عملیات 1",
                Link = "/Person",
                Icon = "fa-star"
            });
            actions.Add(new _Action
            {
                Id = 2,
                Title = "action1",
                TranslatedTitle = "عملیات 2",
                Link = "/Person",
                Icon = "fa-tasks"
            });

            var ds = new GridDs<_Task>
            {
                Items = new List<_Task>(),
                PageIndex = 0,
                PageSize = 10
            };
            ds.Items.Add(new _Task
            {
                Id = 1,
                Title = "In Progress",
                DueDate = "2018-08-19",
                Priority = 3,
                CreatedBy = "علی پورنعمت",
                CreatedDate = "2018-08-19",
                FormLink = "/Person",
                Status = new _Status
                {
                    Id = 1,
                    Status = "In Progress",
                    translatedStatus = "در دست اقدام"
                },
                Description = "این یک تست است",
                Actions = actions
            });
            return Json(ds);
        }

        public ActionResult GetGreenBoxes()
        {
            var actions = new List<_Action>();
            actions.Add(new _Action
            {
                Id = 1,
                Title = "new",
                TranslatedTitle = "عملیات 1",
                Link = "/Person",
                Icon = "fa-star"
            });
            actions.Add(new _Action
            {
                Id = 2,
                Title = "action1",
                TranslatedTitle = "عملیات 2",
                Link = "/Person",
                Icon = "fa-tasks"
            });

            var ds = new GridDs<_Task>
            {
                Items = new List<_Task>(),
                PageIndex = 0,
                PageSize = 10
            };
            ds.Items.Add(new _Task
            {
                Id = 1,
                Title = "Done",
                DueDate = "2018-08-19",
                Priority = 3,
                CreatedBy = "علی پورنعمت",
                CreatedDate = "2018-08-19",
                FormLink = "/Person",
                Status = new _Status
                {
                    Id = 2,
                    Status = "Completed",
                    translatedStatus = "انجام شده"
                },
                Description = "این یک تست است",
                Actions = actions
            });
            return Json(ds);
        }
        public ActionResult GetStatuses()
        {
            List<_Status> Statuses = new List<_Status>();
            Statuses.Add(new _Status
            {
                Id = 1,
                Status = "In Progress",
                translatedStatus = "در دست اقدام"
            });
            Statuses.Add(new _Status
            {
                Id = 2,
                Status = "Completed",
                translatedStatus = "انجام شده"
            });
            Statuses.Add(new _Status
            {
                Id = 3,
                Status = "Not Started",
                translatedStatus = "شروع نشده"
            });
            return Json(Statuses);
        }

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
