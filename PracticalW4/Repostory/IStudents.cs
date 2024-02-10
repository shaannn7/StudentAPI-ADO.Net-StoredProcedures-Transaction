using PracticalW4.Model;

namespace PracticalW4.Repostory
{
    public interface IStudents
    {
        public Students GetStudents(int id);
        public List<Students> GetAllStudents();
        public void AddStudents(Students students);
        public void UpdateStudents(Students students);
        public void DeleteStudents(int id);
    }
}
