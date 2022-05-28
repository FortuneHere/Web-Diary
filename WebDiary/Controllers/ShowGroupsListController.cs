using System;
using System.Web.Mvc;
using WebDiary.DB;
using WebDiary.DB.Models;
using WebDiary.ViewModels;

namespace WebDiary.Controllers
{
    public class ShowGroupsListController : BaseController
    {
        private readonly GroupRepository groupRepository;
        private readonly StudyYearProvider studyYearProvider;

        public ShowGroupsListController()
        {
            groupRepository = new GroupRepository();
            studyYearProvider = new StudyYearProvider();
        }

        public ActionResult Index() =>
            View(groupRepository.GetForStudyYear(studyYearProvider.GetYear()));

        [HttpGet]
        public ActionResult Create() =>
            View(new GroupModel()
            {
                StudyYear = studyYearProvider.GetYear()
            });

        [HttpPost]
        public ActionResult Create(GroupModel groupModel)
        {
            groupModel.StudyYear = studyYearProvider.GetYear();
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Запрос не прошел валидацию";
                return View(groupModel);
            }

            var group = groupRepository.GetByNumber(groupModel.Number, groupModel.StudyYear);
            if (group != null)
            {
                ModelState.AddModelError("Number", string.Format("Группа с таким номером уже существует в {0} учебном году", groupModel.StudyYear));
                return View(groupModel);
            }

            groupRepository.Add(new Group
            {
                Number = groupModel.Number,
                StudyYear = groupModel.StudyYear
            });
            return RedirectToAction("Index");
        }

        public ActionResult Delete(long id)
        {
            groupRepository.Delete(id);

            return RedirectToAction("Index");
        }
    }
}