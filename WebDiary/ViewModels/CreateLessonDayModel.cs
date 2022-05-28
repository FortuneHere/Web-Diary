namespace WebDiary.ViewModels
{
    public class CreateLessonDayModel
    {
        public string DayName { get; set; }
        
        public int DayNumber { get; set; }

        public int LessonDayNumber { get; set; }

        public int LessonCount { get; set; }

        public string BuildNumber { get; set; }

        public string ClassroomNumber { get; set; }
    }
}