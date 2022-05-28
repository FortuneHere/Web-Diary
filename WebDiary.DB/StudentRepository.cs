using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebDiary.DB.Models;

namespace WebDiary.DB
{
    public class StudentRepository
    {
        private readonly ApplicationDbContext db;

        public StudentRepository() =>
            db = new ApplicationDbContext();

        public List<Student> GetAll()
        {
            return db.Students
                     .Include(s => s.User)
                     .Include(s => s.Groups.Select(g => g.Group)).ToList();
        }

        public List<Student> GetByGroupId(long groupId)
        {
            return db.Students
                     .Include(s => s.User)
                     .Include(s => s.Groups)
                     .Where(s => s.Groups.Any(g => g.GroupId == groupId)).ToList();
        }

        public void Add(Student student)
        {
            db.Students.Add(student);
            db.SaveChanges();
        }
        
        public void Delete(long studentId)
        {
            var student = db.Students.FirstOrDefault(s => s.Id == studentId);
            if (student != null)
            {
                db.Students.Remove(student);
                db.SaveChanges();
            }
        }
    }
}