using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebDiary.DB;
using WebDiary.DB.Models;
using WebDiary.ViewModels;

namespace WebDiary.Controllers
{
    public class LessonsController : Controller
    {
        private readonly CourseRepository courseRepository;
        private readonly LessonRepository lessonRepository;
        private readonly StudyYearProvider studyYearProvider;

        public LessonsController()
        {
            courseRepository = new CourseRepository();
            lessonRepository = new LessonRepository();
            studyYearProvider = new StudyYearProvider();
        }

        [HttpGet]
        public ActionResult Create(long courseId)
        {
            var course = courseRepository.Get(courseId);
            var year = studyYearProvider.GetYear();
            var semester = studyYearProvider.GetSemester();
            return View(new CreateLessonsModel()
            {
                CourseId = courseId,
                CourseTitle = course.CourseInfo.Title,
                StartDate = studyYearProvider.GetSemesterStartDate().ToShortDateString(),
                EndDate = studyYearProvider.GetSemesterEndDate().ToShortDateString(),
                SettingsByDay = new List<CreateLessonDayModel>()
                {
                    new CreateLessonDayModel()
                    {
                        DayName = "Понедельник",
                        DayNumber = 1,
                    },
                    new CreateLessonDayModel()
                    {
                        DayName = "Вторник",
                        DayNumber = 2,
                    },
                    new CreateLessonDayModel()
                    {
                        DayName = "Среда",
                        DayNumber = 3,
                    },
                    new CreateLessonDayModel()
                    {
                        DayName = "Четверг",
                        DayNumber = 4,
                    },
                    new CreateLessonDayModel()
                    {
                        DayName = "Пятница",
                        DayNumber = 5,
                    },
                    new CreateLessonDayModel()
                    {
                        DayName = "Суббота",
                        DayNumber = 6,
                    },
                }
            });
        }

        [HttpPost]
        public ActionResult Create(CreateLessonsModel model)
        {
            DateTime startDate;
            if (!DateTime.TryParse(model.StartDate, out startDate))
            {
                ModelState.AddModelError("StartDate", "Неверный формат даты");
            }
            DateTime endDate;
            if (!DateTime.TryParse(model.EndDate, out endDate))
            {
                ModelState.AddModelError("EndDate", "Неверный формат даты");
            }
            if(startDate < studyYearProvider.GetSemesterStartDate())
            {
                ModelState.AddModelError("StartDate", "Дата начала указана раньше даты начал семестра");
            }
            if(endDate > studyYearProvider.GetSemesterEndDate().AddMonths(2))
            {
                ModelState.AddModelError("EndDate", "Дата окончания указана позже окончания семестра");
            }
            if(startDate > endDate)
            {
                ModelState.AddModelError("EndDate", "Дата начала занятий больше даты окочания занятий");
            }

            var days = model.SettingsByDay.Where(d => d.LessonDayNumber > 0).ToList();
            if (days.Count == 0)
            {
                ViewBag.Message = "Не заданы дни, в которых есть занятия";
                return View(model);
            }

            if (days.Any(d => d.LessonCount == 0))
            {
                ViewBag.Message = "В выбраных днях не указано количество занятий";
                return View(model);
            }
            if(!ModelState.IsValid)
                return View(model);

            var lessons = new List<Lesson>();
            var currentDay = startDate;
            while (currentDay <= endDate)
            {
                var day = days.FirstOrDefault(d => d.DayNumber == (int) currentDay.DayOfWeek);
                if (day != null)
                {
                    lessons.Add(new Lesson()
                    {
                        CourseId = model.CourseId,
                        Date = currentDay,
                        BuildNumber = day.BuildNumber,
                        ClassroomNumber = day.ClassroomNumber,
                        StartTime = GetStartDate(day),
                        EndTime = GetEndDate(day)
                    });
                }

                currentDay = currentDay.AddDays(1);
            }
            if(lessons.Count == 0)
            {
                ViewBag.Message = "По данным критериям не создано ни одного занятия";
                return View(model);
            }

            lessonRepository.AddLessons(lessons);
            return RedirectToAction("Index", "ShowCoursesList");
        }
        
        public TimeSpan GetStartDate(CreateLessonDayModel model)
        {
            var startTime = new TimeSpan(8, 0, 0);
            return startTime.Add(new TimeSpan(0, (90 + 10) * (model.LessonDayNumber - 1), 0));
        }

        public TimeSpan GetEndDate(CreateLessonDayModel model)
        {
            var startTime = GetStartDate(model);
            return startTime.Add(new TimeSpan(0, 90 * model.LessonCount + 10 * (model.LessonCount - 1), 0));
        }

    }
}