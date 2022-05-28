using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebDiary.DB.Models;

namespace WebDiary.DB
{
    public class GroupRepository
    {
        private readonly ApplicationDbContext db;

        public GroupRepository() =>
            db = new ApplicationDbContext();

        public List<Group> GetAll()
        {
            return db.Groups.OrderBy(g => g.Number).ToList();
        }

        public List<Group> GetForStudyYear(int year)
        {
            return db.Groups.Where(g => g.StudyYear == year).OrderBy(g => g.Number).ToList();
        }

        public Group Get(long groupId)
        {
            return db.Groups.FirstOrDefault(g => g.Id == groupId);
        }
        
        public Group GetForStudent(long studentId, int todayYear)
        {
            return db.Groups
                .Include(g => g.Students)
                .FirstOrDefault(g => g.Students.Any(s => s.StudentId == studentId));
        }
        
        public Group GetByNumber(string number, int todayYear)
        {
            return db.Groups.FirstOrDefault(g => g.Number == number && g.StudyYear == todayYear);
        }
        
        public void Add(Group group)
        {
            db.Groups.Add(group);
            db.SaveChanges();
        }
        
        public void Delete(long groupId)
        {
            var group = db.Groups.FirstOrDefault(s => s.Id == groupId);
            if (group != null)
            {
                db.Groups.Remove(group);
                db.SaveChanges();
            }
        }
    }
}