using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace WebDiary.ViewModels
{
    public class TeacherModel : UserModelBase
    {
        public long Id { get; set; }

        [DisplayName("Администратор")] public bool IsAdmin { get; set; }

        [DisplayName("Должность")] public string Post { get; set; }

    }
}