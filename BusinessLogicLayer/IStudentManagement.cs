
using StudentManagementSystem.DAL;
namespace StudentManagementSystem.BLL
{
    // Internal access specifiers
    public interface IStudentManagement
    {
        void AddStudent(Student student);
        Student? GetStudentByID(string studentID);
        void DeleteStudent(string studentID);
        List<Student> GetStudents();

        void LoadStudents();
    }
}
