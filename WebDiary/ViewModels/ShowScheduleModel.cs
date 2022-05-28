using System.Collections.Generic;
using System.ComponentModel;

namespace WebDiary.ViewModels
{
    public class ShowScheduleModel
    {
        [DisplayName("Дата")]
        public string Date { get; set; }

        [DisplayName("Номер группы")]
        public string GroupNumber { get; set; }

        public List<ScheduleItem> Items { get; set; }
    }
}