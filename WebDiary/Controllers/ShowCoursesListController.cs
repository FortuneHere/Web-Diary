using System;
using System.Linq;
using System.Web.Mvc;
using WebDiary.DB;
using WebDiary.DB.Models;
using WebDiary.ViewModels;

namespace WebDiary.Controllers
{
    public class ShowCoursesListController : BaseController
    {
        private readonly CourseRepository courseRepository;
        private readonly TeacherRepository teacherRepository;
        private readonly GroupRepository groupRepository;
        private readonly StudentRepository studentRepository;
        private readonly StudyYearProvider studyYearProvider;

        public ShowCoursesListController()
        {
            teacherRepository = new TeacherRepository();
            courseRepository = new CourseRepository();
            groupRepository = new GroupRepository();
            studentRepository = new StudentRepository();
            studyYearProvider = new StudyYearProvider();
        }

        public ActionResult Index() =>
            View(courseRepository.GetForStudyYear(studyYearProvider.GetYear(), studyYearProvider.GetSemester()));

        [HttpGet]
        public ActionResult Create()
        {
            var courseInfos = courseRepository.GetAllCourseInfos();
            var groups = groupRepository.GetAll();
            var teachers = teacherRepository.GetAll();
            return View(new CourseModel()
            {
                CourseInfos = courseInfos.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Title
                }).ToList(),
                Groups = groups.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Number
                }).ToList(),
                Teachers = teachers.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.User.ShortFio
                }).ToList(),
            });

        }

        [HttpPost]
        public ActionResult Create(CourseModel model)
        {
            if (ModelState.IsValid)
            {
                var teacherId = int.Parse(model.TeacherId);
                var groupId = int.Parse(model.GroupId);
                var courseInfoId = int.Parse(model.CourseInfoId);

                courseRepository.Add(new Course
                {
                    ClassType = model.ClassType,
                    GroupId = groupId,
                    TeacherId = teacherId,
                    CourseInfoId = courseInfoId,
                    SemesterType = studyYearProvider.GetSemester()
                });
                return RedirectToAction("Index");
            }

            ViewBag.Message = "Запрос не прошел валидацию";
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateCourseInfo(CourseInfoModel courseModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, error = "Есть ошибки при заполнении" });
            }

            var info = courseRepository.GetCourseInfoByTitle(courseModel.Title);
            if (info != null)
            {
                return Json(new { success = false, error = "Предмет с таким названием уже существует" });
            }

            courseRepository.Add(new CourseInfo
            {
                Title = courseModel.Title,
                Department = courseModel.Department
            });
            return Json(new { success = true });
        }

        public ActionResult Delete(long id)
        {
            courseRepository.Delete(id);

            return RedirectToAction("Index");
        }
    }
}