using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebDiary.DB;
using WebDiary.DB.Models;
using WebDiary.ViewModels;

namespace WebDiary.Controllers
{
    public class StudentLessonsController : BaseController
    {
        private readonly CourseRepository courseRepository;
        private readonly GroupRepository groupRepository;
        private readonly LessonRepository lessonRepository;
        private readonly StudyYearProvider studyYearProvider;

        public StudentLessonsController()
        {
            courseRepository = new CourseRepository();
            groupRepository = new GroupRepository();
            lessonRepository = new LessonRepository();
            studyYearProvider = new StudyYearProvider();
        }

        public ActionResult Index()
        {
            var group = groupRepository.GetForStudent((long) Session["Id"], studyYearProvider.GetYear());
            return View(group != null
                            ? courseRepository
                              .GetByGroup(group.Id, studyYearProvider.GetYear(), studyYearProvider.GetSemester())
                              .ToList()
                            : new List<Course>()
            );
        }

        public ActionResult History(int courseId)
        {
            var studentId = (long) Session["Id"];
            var course = courseRepository.Get(courseId);
            var marks = lessonRepository.GetMarks(courseId, studentId);
            var intMarks = marks.Select(m =>
                                {
                                    int mark;
                                    Int32.TryParse(m.Mark, out mark);
                                    return mark;
                                })
                                .Where(m => m != 0)
                                .ToList();
            return View(new HistoryModel
            {
                Items = marks
                        .Select(m => new HistoryItem()
                        {
                            Date = m.Lesson.Date.ToShortDateString(),
                            Mark = m.Mark,
                            IsAbsent = m.IsAbsent
                        })
                        .ToList(),
                AvarageMark = intMarks.Count > 0 ? intMarks.Average() : 0,
                SubjectTitle = course.CourseInfo.Title,
                ClassType = course.ClassType
            });
        }
    }
}