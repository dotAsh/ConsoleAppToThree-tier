
using StudentManagementSystem.DAL;

namespace StudentManagementSystem.BLL
{
    
    public  class StudentManager : IStudentManagement
    {
        private List<Student> students;

        private StudentDataAccess stdDataAccess;

        public StudentManager(StudentDataAccess stdDataAcc)
        {
            stdDataAccess = stdDataAcc;
            students = new List<Student>();
        }



        // we can use this  Generic method to get the last student from the student list 
        public T GetLastItem<T>(List<T> list)
        {
            return list.Last();
        }

       
      
       

     
        public void AddStudent(Student student)
        {
            students.Add(student);
            SaveStudents();
        }

        public Student? GetStudentByID(string studentID)
        {
            return students.Find(student => student.StudentId == studentID);
        }

        public void DeleteStudent(string studentID)
        {
            students.RemoveAll(student => student.StudentId == studentID);
            SaveStudents();
        }

        public List<Student> GetStudents()
        {
            return students;
        }

        private void SaveStudents()
        {
            stdDataAccess.SaveStudentsToJson(students);
        }

        public void LoadStudents()
        {
            students = stdDataAccess.LoadStudentsFromJson();

        }

        // Used Param Arrays
        public void AddCoursesToStudent(Student student, params Course[] courses)
        {
            foreach (var course in courses)
            {
                student.CoursesInEachSemester.Add(course);
            }
        }      
    }

}
