using System.ComponentModel;

namespace WebDiary.DB.Models
{
    public enum SemesterType
    {
        [Description("Первый")]
        First = 1,
        [Description("Второй")]
        Second = 2
    }
}