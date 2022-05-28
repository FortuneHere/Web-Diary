using System;
using System.Linq;
using System.Web.Mvc;
using WebDiary.DB;
using WebDiary.ViewModels;

namespace WebDiary.Controllers
{
    public class DairyController : BaseController
    {
        private readonly CourseRepository courseRepository;
        private readonly GroupRepository groupRepository;
        private readonly LessonRepository lessonRepository;
        private readonly StudentRepository studentRepository;
        private readonly StudyYearProvider studyYearProvider;

        public DairyController()
        {
            courseRepository = new CourseRepository();
            groupRepository = new GroupRepository();
            studentRepository = new StudentRepository();
            studyYearProvider = new StudyYearProvider();
            lessonRepository = new LessonRepository();
        }

        public ActionResult Index()
        {
            var courses = courseRepository.GetByTeacher((long) Session["Id"], studyYearProvider.GetYear(), studyYearProvider.GetSemester());
            return View(new DairyModel
            {
                Courses = courses.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = string.Format("{0} г. {1} {2}", c.Group.Number, c.CourseInfo.Title, c.ClassType.GetShortDescription())
                }).ToList(),
            });
        }

        [HttpPost]
        public ActionResult Index(DairyModel model)
        {
            return RedirectToAction("Edit", "Dairy", new {courseId = model.CourseId});
        }
        
        public ActionResult Edit(int courseId)
        {
            var course = courseRepository.Get(courseId);
            if(course == null)
                throw new Exception("Курс не найден");

            var students = studentRepository.GetByGroupId(course.GroupId);
            var lessongs = lessonRepository.GetForCourse(courseId);
            return View("Edit", new EditDairyModel
            {
                CourseId = course.Id,
                GroupNumber = course.Group.Number,
                CourseTitle = course.CourseInfo.Title,
                Students = students.Select(c => new DairyStudentModel
                {
                    StudentId = c.Id,
                    Fio = c.User.Fio,
                }).ToList(),
                Lessons = lessongs.Select(l => new DairyLessonModel()
                {
                    LessonId = l.Id,
                    Date = l.Date,
                    Marks = l.Marks.Select(m => new DairyMarkModel()
                    {
                        LessonId = m.LessonId,
                        Mark = m.Mark,
                        IsAbsent = m.IsAbsent,
                        StudentId = m.StudentId
                    }).ToList()
                }).ToList()
            });
        }


        [HttpPost]
        public void Mark(long studentId, long lessonId, string mark, bool isAbsent)
        {
            lessonRepository.SetMark(studentId, lessonId, mark, isAbsent);
        }
    }
}