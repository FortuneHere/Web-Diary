using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Data;
using WebDiary.DB;
using WebDiary.DB.Models;
using WebDiary.ViewModels;

namespace WebDiary.Controllers
{
    public class SchedulerController : Controller
    {
        private readonly CourseRepository courseRepository;
        private readonly GroupRepository groupRepository;
        private readonly LessonRepository lessonRepository;
        private readonly StudyYearProvider studyYearProvider;

        public SchedulerController()
        {
            courseRepository = new CourseRepository();
            groupRepository = new GroupRepository();
            lessonRepository = new LessonRepository();
            studyYearProvider = new StudyYearProvider();
        }

        public ActionResult CheckGroupNumber(string groupNumber)
        {
            var group = groupRepository.GetByNumber(groupNumber, studyYearProvider.GetYear());
            if (group != null)
                return Json(new {success = true, groupId = group.Id});
            return Json(new {success = false, error = "Такая группа не найдена за этот учбный год"});
        }

        public ActionResult Index(long? groupId, long? courseId)
        {
            var scheduler = new DHXScheduler(this);
            scheduler.Skin = DHXScheduler.Skins.Material;
            scheduler.Localization.Set(SchedulerLocalization.Localizations.Russian);
            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = true;
            scheduler.InitialDate = new DateTime(studyYearProvider.GetYear(), studyYearProvider.GetSemester() == SemesterType.First ? 9 : 2, 1);
            scheduler.Config.first_hour = 8;
            scheduler.Config.last_hour = 21;
            scheduler.Config.time_step = 10;
            scheduler.InitialView = "month";
            scheduler.DataAction = groupId.HasValue
                ? string.Format("Data?groupId={0}", groupId)
                : string.Format("Data?courseId={0}", courseId);
            //scheduler.SetEditMode(DHTMLX.Scheduler.Authentication.EditModes.OwnEventsOnly);

            // var agenda= new DHTMLX.Scheduler.Controls.AgendaView(); // initializes the view
            // scheduler.Views.Add(agenda); // adds the view to the scheduler
            return View(scheduler);
        }

        public ActionResult TeacherIndex()
        {
            var scheduler = new DHXScheduler(this);
            scheduler.Skin = DHXScheduler.Skins.Material;
            scheduler.Localization.Set(SchedulerLocalization.Localizations.Russian);
            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = true;
            scheduler.InitialDate = new DateTime(studyYearProvider.GetYear(), studyYearProvider.GetSemester() == SemesterType.First ? 9 : 2, 1);
            scheduler.Config.first_hour = 8;
            scheduler.Config.last_hour = 21;
            scheduler.Config.time_step = 10;
            scheduler.InitialView = "month";
            scheduler.DataAction = string.Format("Data?teacherId={0}", Session["Id"]);
            
            var box = scheduler.Lightbox.SetExternalLightboxForm("TeacherLightbox", 500, 150);
            box.ClassName = "custom_lightbox";

            //scheduler.SetEditMode(DHTMLX.Scheduler.Authentication.EditModes.OwnEventsOnly);

            // var agenda= new DHTMLX.Scheduler.Controls.AgendaView(); // initializes the view
            // scheduler.Views.Add(agenda); // adds the view to the scheduler
            return View("Index", scheduler);
        }

        public ActionResult TeacherLightbox()
        {
            return View();
        }
        
        public ContentResult Data(long? groupId, long? courseId, long? teacherId)
        {
            List<SchedulerItemModel> lessons = new List<SchedulerItemModel>();
            if (groupId.HasValue)
            {
                lessons = lessonRepository
                          .GetForGroup(groupId.Value).Select(e => new SchedulerItemModel()
                          {
                              id = e.Id,
                              text = string.Format("{0} {1} {2} зд. {3}",
                                                   e.Course.CourseInfo.Title, 
                                                   e.Course.ClassType.GetShortDescription(),
                                                   e.BuildNumber, 
                                                   e.ClassroomNumber),
                              start_date = e.Date.Add(e.StartTime).ToString(CultureInfo.InvariantCulture),
                              end_date = e.Date.Add(e.EndTime).ToString(CultureInfo.InvariantCulture),
                          }).ToList();
            }
            else if (courseId.HasValue)
            {
                lessons = lessonRepository
                          .GetForCourse(courseId.Value).Select(e => new SchedulerItemModel()
                          {
                              id = e.Id,
                              text = string.Format("{0} {1} {2} зд. {3}",
                                                   e.Course.CourseInfo.Title,
                                                   e.Course.ClassType.GetShortDescription(), 
                                                   e.BuildNumber,
                                                   e.ClassroomNumber),
                              start_date = e.Date.Add(e.StartTime).ToString(CultureInfo.InvariantCulture),
                              end_date = e.Date.Add(e.EndTime).ToString(CultureInfo.InvariantCulture),
                          }).ToList();
            }
            else if (teacherId.HasValue)
            {
                lessons = lessonRepository
                          .GetForTeacher(teacherId.Value, studyYearProvider.GetYear(), studyYearProvider.GetSemester())
                          .Select(e => new SchedulerItemModel()
                          {
                              id = e.Id,
                              text = string.Format("{0} г. {1} {2} {3} зд. {4}",
                                                   e.Course.Group.Number,
                                                   e.Course.CourseInfo.Title, 
                                                   e.Course.ClassType.GetShortDescription(),
                                                   e.BuildNumber,
                                                   e.ClassroomNumber),
                              start_date = e.Date.Add(e.StartTime).ToString(CultureInfo.InvariantCulture),
                              end_date = e.Date.Add(e.EndTime).ToString(CultureInfo.InvariantCulture),
                          }).ToList();
            }

            var data = new SchedulerAjaxData(lessons);
            return data;
        }

        public ActionResult Save(int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);
            action.Type = DataActionTypes.Error;
            action.Message = "This room is already booked for this date.";

            return (new AjaxSaveResponse(action));
            // var action = new DataAction(actionValues);
            // var changedEvent = DHXEventsHelper.Bind<Event>(actionValues);
            // var entities = new SchedulerContext();
            // try
            // {
            //     switch (action.Type)
            //     {
            //         case DataActionTypes.Insert:
            //             entities.Events.Add(changedEvent);
            //             break;
            //         case DataActionTypes.Delete:
            //             changedEvent = entities.Events.FirstOrDefault(ev => ev.id == action.SourceId);
            //             entities.Events.Remove(changedEvent);
            //             break;
            //         default:// "update"
            //             var target = entities.Events.Single(e => e.id == changedEvent.id);
            //             DHXEventsHelper.Update(target, changedEvent, new List<string> { "id" });
            //             break;
            //     }
            //     entities.SaveChanges();
            //     action.TargetId = changedEvent.id;
            // }
            // catch (Exception a)
            // {
            //     action.Type = DataActionTypes.Error;
            // }

            //return (new AjaxSaveResponse(action));
            return Json(new { });
        }
    }
}