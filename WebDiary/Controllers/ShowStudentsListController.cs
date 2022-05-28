using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebDiary.DB;
using WebDiary.DB.Models;
using WebDiary.ViewModels;

namespace WebDiary.Controllers
{
    public class ShowStudentsListController : BaseController
    {
        private readonly GroupRepository groupRepository;
        private readonly StudentRepository studentRepository;
        private readonly StudyYearProvider studyYearProvider;

        public ShowStudentsListController()
        {
            studentRepository = new StudentRepository();
            groupRepository = new GroupRepository();
            studyYearProvider = new StudyYearProvider();
        }

        public ActionResult Index()
        {
            var students = studentRepository.GetAll();
            return View(students);
        }

        [HttpGet]
        public ActionResult Create() =>
            View();

        [HttpPost]
        public ActionResult Create(StudentModel student)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Запрос не прошел валидацию";
                return View(student);
            }

            var group = groupRepository.GetByNumber(student.GroupNumber, studyYearProvider.GetYear());
            if (group == null)
            {
                ModelState.AddModelError("GroupNumber", "Такой группы нет");
                ViewBag.Message = "Запрос не прошел валидацию";
                return View(student);
            }

            studentRepository.Add(new Student
            {
                User = new User
                {
                    Login = student.Login,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    MiddleName = student.MiddleName,
                    Password = student.Password
                },
                Groups = new List<GroupStudent>()
                {
                    new GroupStudent()
                    {
                        GroupId = @group.Id
                    }
                }
            });
            return RedirectToAction("Index");
        }

        public ActionResult Delete(long id)
        {
            studentRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}