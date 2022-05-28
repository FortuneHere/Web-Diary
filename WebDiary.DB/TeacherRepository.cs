using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebDiary.DB.Models;

namespace WebDiary.DB
{
    public class TeacherRepository
    {
        private readonly ApplicationDbContext db;

        public TeacherRepository() =>
            db = new ApplicationDbContext();

        public List<Teacher> GetAll()
        {
            return db.Teachers
                     .Include(s => s.User).ToList();
        }

        public Teacher Get(long id)
        {
            return db.Teachers
                     .Include(s => s.User)
                     .Include(s => s.Courses)
                     .FirstOrDefault(g => g.Id == id);
        }

        public void Add(Teacher teacher)
        {
            if (db.Teachers.Any(t => t.Id == teacher.Id))
            {
                db.Entry(teacher).State = EntityState.Modified;
                db.Entry(teacher.User).State = EntityState.Modified;
            }
            else
            {
                db.Teachers.Add(teacher);
            }

            db.SaveChanges();
        }

        public void Delete(long teacherId)
        {
            var teacher = db.Teachers.FirstOrDefault(s => s.Id == teacherId);
            if (teacher != null)
            {
                db.Teachers.Remove(teacher);
                db.SaveChanges();
            }
        }
    }
}