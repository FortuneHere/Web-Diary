using System;
using System.Configuration;
using WebDiary.DB.Models;

namespace WebDiary.ViewModels
{
    public class StudyYearProvider
    {
        public int GetYear()
        {
            var settings = ConfigurationManager.AppSettings["StudyYear"];
            int year;
            if (int.TryParse(settings, out year))
                return year;
            return DateTime.Today.Year;
        }

        public SemesterType GetSemester()
        {
            var settings = ConfigurationManager.AppSettings["Semester"];
            int year;
            if (int.TryParse(settings, out year))
                return (SemesterType) year;

            return new DateTime(DateTime.Today.Year, 7, 1) > DateTime.Today ? SemesterType.First : SemesterType.Second;
        }

        public DateTime GetSemesterStartDate()
        {
            if (GetSemester() == SemesterType.First)
            {
                return new DateTime(GetYear(), 9, 1);
            }

            return new DateTime(GetYear(), 2, 10);
        }

        public DateTime GetSemesterEndDate()
        {
            if (GetSemester() == SemesterType.First)
            {
                return new DateTime(GetYear(), 12, 31);
            }

            return new DateTime(GetYear(), 5, 31);
        }
    }
}