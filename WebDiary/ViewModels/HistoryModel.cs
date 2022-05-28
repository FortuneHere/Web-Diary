using System.Collections.Generic;
using System.ComponentModel;
using WebDiary.DB.Models;

namespace WebDiary.ViewModels
{
    public class HistoryModel
    {
        [DisplayName("Номер группы")]
        public string GroupNumber { get; set; }

        [DisplayName("Предмет")]
        public string SubjectTitle { get; set; }

        public ClassType ClassType { get; set; }

        public List<HistoryItem> Items { get; set; }

        public double AvarageMark { get; set; }
    }
}