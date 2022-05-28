using System.ComponentModel;

namespace WebDiary.DB.Models
{
    public enum ClassType
    {
        [Description("Лекция")]
        [ShortDescription("лек.")]
        Lecture,
        [Description("Практика")]
        [ShortDescription("пр.")]
        Practice,
        [Description("Лабораторная работа")]
        [ShortDescription("л.р.")]
        Laboratory,
        [Description("Экзамен")]
        [ShortDescription("экз.")]
        Exam
    }
}