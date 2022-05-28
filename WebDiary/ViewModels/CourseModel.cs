using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using WebDiary.DB.Models;

namespace WebDiary.ViewModels
{
    public class CourseModel
    {
        [DisplayName("Группа")] public string GroupId { get; set; }

        public List<SelectListItem> Groups { get; set; }

        [DisplayName("Название предмета")] public string CourseInfoId { get; set; }

        public List<SelectListItem> CourseInfos { get; set; }
        
        [DisplayName("Преподаватель")] public string TeacherId { get; set; }

        public List<SelectListItem> Teachers { get; set; }
        
        [DisplayName("Тип занятия")] public ClassType ClassType { get; set; }
    }
}