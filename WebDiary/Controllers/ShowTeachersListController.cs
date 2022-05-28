using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebDiary.DB;
using WebDiary.DB.Models;
using WebDiary.ViewModels;

namespace WebDiary.Controllers
{
    public class ShowTeachersListController : BaseController
    {
        private readonly CourseRepository courseRepository;
        private readonly TeacherRepository teacherRepository;

        public ShowTeachersListController()
        {
            teacherRepository = new TeacherRepository();
            courseRepository = new CourseRepository();
        }

        // GET: ShowTeachersList
        public ActionResult Index() =>
            View(teacherRepository.GetAll());

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TeacherModel teacher)
        {
            if (Save(teacher))
            {
                return RedirectToAction("Index");
            }

            ViewBag.Message = "Запрос не прошел валидацию";
            return View(teacher);
        }

        public ActionResult Edit(int id)
        {
            var teacher = teacherRepository.Get(id);
            var courses = courseRepository.GetAll().ToList();

            return View("Create", new TeacherModel
            {
                Id = teacher.Id,
                FirstName = teacher.User.FirstName,
                LastName = teacher.User.LastName,
                MiddleName = teacher.User.MiddleName,
                Login = teacher.User.Login,
                Password = teacher.User.Password,
                IsAdmin = teacher.IsAdministrator,
                Post = teacher.Post
            });
        }

        [HttpPost]
        public ActionResult Edit(TeacherModel teacher)
        {
            if (Save(teacher))
            {
                return RedirectToAction("Index");
            }

            ViewBag.Message = "Запрос не прошел валидацию";
            return View("Create", teacher);
        }

        public ActionResult Delete(long id)
        {
            teacherRepository.Delete(id);

            return RedirectToAction("Index");
        }

        private bool Save(TeacherModel teacher)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }

            teacherRepository.Add(new Teacher
            {
                Id = teacher.Id,
                User = new User
                {
                    Id = teacher.Id,
                    Login = teacher.Login,
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    MiddleName = teacher.MiddleName,
                    Password = teacher.Password
                },
                IsAdministrator = teacher.IsAdmin,
                Post = teacher.Post
            });
            return true;
        }
    }
}