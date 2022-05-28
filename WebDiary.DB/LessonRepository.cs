using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using WebDiary.DB.Models;

namespace WebDiary.DB
{
    public class LessonRepository
    {
        private readonly ApplicationDbContext db;

        public LessonRepository() =>
            db = new ApplicationDbContext();

        public List<Lesson> GetForGroup(long groupId)
        {
            return db.Lessons
                     .Include(l => l.Course)
                     .Include(l => l.Course.CourseInfo)
                     .Where(l => l.Course.GroupId == groupId).ToList();
        }
        
        public List<Lesson> GetForCourse(long courseId)
        {
            return db.Lessons
                     .Include(l => l.Course)
                     .Include(l => l.Course.CourseInfo)
                     .Include(l => l.Marks)
                     .Where(l => l.CourseId == courseId).ToList();
        }

        public List<Lesson> GetForTeacher(long teacherId, int studyYear, SemesterType semesterType)
        {
            return db.Lessons
                     .Include(l => l.Course)
                     .Include(l => l.Course.CourseInfo)
                     .Include(l => l.Course.Group)
                     .Where(l => l.Course.TeacherId == teacherId &&
                                 l.Course.Group.StudyYear == studyYear &&
                                 l.Course.SemesterType == semesterType)
                     .ToList();
        }

        public void AddLessons(List<Lesson> lessons)
        {
            db.Lessons.AddRange(lessons);
            db.SaveChanges();
        }

        public void SetMark(long studentId, long lessonId, string mark, bool isAbsent)
        {
            db.LessonMarks.Add(new LessonMark()
            {
                StudentId = studentId,
                LessonId = lessonId,
                IsAbsent = isAbsent,
                Mark = mark
            });
            db.SaveChanges();
        }

        public List<LessonMark> GetMarks(int courseId, long studentId)
        {
            return db.LessonMarks
                     .Include(l => l.Lesson)
                     .Where(l => l.Lesson.CourseId == courseId && l.StudentId == studentId)
                     .ToList();
        }
    }
}