using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace WebDiary.ViewModels
{
    public class DairyModel
    {
        [DisplayName("Название предмета")] public string CourseId { get; set; }

        public List<SelectListItem> Courses { get; set; }
    }
}