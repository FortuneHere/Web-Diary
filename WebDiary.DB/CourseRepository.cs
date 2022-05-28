using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebDiary.DB.Models;

namespace WebDiary.DB
{
    public class CourseRepository
    {
        private readonly ApplicationDbContext db;

        public CourseRepository() =>
            db = new ApplicationDbContext();

        public List<Course> GetAll()
        {
            return db.Courses
                     .Include(c => c.CourseInfo)
                     .Include(c => c.Group)
                     .Include(c => c.Teacher)
                     .ToList();
        }

        public List<CourseInfo> GetAllCourseInfos()
        {
            return db.CourseInfos
                     .ToList();
        }

        public List<Course> GetForStudyYear(int year, SemesterType semesterType)
        {
            return db.Courses
                     .Include(c => c.CourseInfo)
                     .Include(c => c.Group)
                     .Include(c => c.Teacher)
                     .Include(c => c.Teacher.User)
                     .Where(c => c.Group.StudyYear == year && c.SemesterType == semesterType)
                     .ToList();
        }

        public List<Course> GetByTeacher(long teacherId, int studyYear, SemesterType semesterType)
        {
            return db.Courses
                     .Include(c => c.CourseInfo)
                     .Include(c => c.Group)
                     .Where(c => c.TeacherId == teacherId && 
                                 c.Group.StudyYear == studyYear && 
                                 c.SemesterType == semesterType)
                     .ToList();
        }
        
        public List<Course> GetByGroup(long groupId, int studyYear, SemesterType semesterType)
        {
            return db.Courses
                     .Include(c => c.CourseInfo)
                     .Include(c => c.Group)
                     .Where(c => c.GroupId == groupId && 
                                 c.Group.StudyYear == studyYear && 
                                 c.SemesterType == semesterType)
                     .ToList();
        }

        public Course Get(long courseId)
        {
            return db.Courses
                     .Include(c => c.CourseInfo)
                     .FirstOrDefault(g => g.Id == courseId);
        }

        public CourseInfo GetCourseInfoByTitle(string title)
        {
            return db.CourseInfos
                     .FirstOrDefault(g => g.Title == title);
        }

        public void Add(Course course)
        {
            db.Courses.Add(course);
            db.SaveChanges();
        }

        public void Add(CourseInfo course)
        {
            db.CourseInfos.Add(course);
            db.SaveChanges();
        }

        public void Delete(long courseId)
        {
            var group = db.Courses.FirstOrDefault(s => s.Id == courseId);
            if (group != null)
            {
                db.Courses.Remove(group);
                db.SaveChanges();
            }
        }

    }
}