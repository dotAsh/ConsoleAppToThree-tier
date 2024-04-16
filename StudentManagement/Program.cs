
using System;
using System.Collections.Generic;
using System.IO;
using StudentManagementSystem.DAL;
using StudentManagementSystem.BLL;
namespace StudentManagementSystem
{
    
    
    public delegate void PrintDelegate(string message);


    internal class Program
    {
       
        public static event PrintDelegate MessagePrompted;

        static void Main(string[] args)
        {
           
            List<Student> students = new List<Student>();

            StudentDataAccess stdDataAccess = new StudentDataAccess("data.json");
            
            students = stdDataAccess.LoadStudentsFromJson();
               
            IStudentManagement studentManager = new StudentManager(stdDataAccess);
            studentManager.LoadStudents();



            PrintDelegate prompt = PrintMessage;
            MessagePrompted += PrintMessage;

            while (true)
            {
                //  list of students
                Console.WriteLine("List of Students:");
                foreach (var student in studentManager.GetStudents())
                {
                    Console.WriteLine(student);
                }
                Console.WriteLine();

                // Display options
                //invoking delegate
                prompt("1. Add New Student");
                prompt("2. View Student Details");
                prompt("3. Delete Student");

                //here we Raise the an event to show the exit option 
                OnMessagePrinted("4. Exit");
                //prompt("4. Exit");

                Console.Write("Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddNewStudent(studentManager);
                        break;
                    case 2:
                        ViewStudentDetails(studentManager);
                        break;
                    case 3:
                        DeleteStudent(studentManager);
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }


        }
        public static void OnMessagePrinted(string message)
        {

            if (MessagePrompted != null)
            {

                MessagePrompted(message);
            }
        }

        public static void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }
        public static void ViewStudentDetails(IStudentManagement studentManager)
        {
            Console.Write("Enter Student ID (or type 'exit' to return to main menu): ");
            string studentID = Console.ReadLine();

            if (studentID.ToLower() == "exit")
                return;

            Student student = studentManager.GetStudentByID(studentID);
            if (student != null)
            {
                Console.WriteLine($"First Name: {student.FirstName}");
                Console.WriteLine($"Middle Name: {student.MiddleName}");
                Console.WriteLine($"Last Name: {student.LastName}");
                Console.WriteLine($"Student ID: {student.StudentId}");
                Console.WriteLine($"Joining Batch: {student.JoiningBatch}");
                Console.WriteLine($"Department: {student.Department}");
                Console.WriteLine($"Degree: {student.Degree}");


                Console.WriteLine("\nDo you want to add a new semester and courses? (Y/N)");
                string input = Console.ReadLine();
                if (input.ToLower() == "y")
                {
                    AddSemesterAndCourses(student);
                }
                else if (input.ToLower() == "n")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'Y' for Yes or 'N' for No.");
                }
            }
            else
            {
                Console.WriteLine("Student not found!");
            }
        }
        public static void AddSemesterAndCourses(Student student)
        {

            Console.WriteLine("Enter Semester Code ( format - Sum, Fal, Spr):");
            string semesterCode = Console.ReadLine();
            Console.WriteLine("Enter Year (YYYY, e.g., 2024):");
            string year = Console.ReadLine();
            Semester newSemester = new Semester(semesterCode, year);

            //here listRef's type is List<Semester>
            dynamic listRef = new List<Semester>();
            if (student.SemestersAttended == null)
            {
                student.SemestersAttended = listRef;
            }
            // Add the new semester to the student's list of attended semesters
            //student.SemestersAttended.Add(newSemester);

            //here we are using Extension methods of student class
            student.AddSemester(semesterCode, year);

            List<Course> hardcodedCourses = new List<Course>{
                    new Course { CourseId = "CSC 101", CourseName = "Introduction to Computer Science", InstructorName = "Prof. Smith", NumberOfCredits = 3 },
                    new Course { CourseId = "ENG 101", CourseName = "English Composition", InstructorName = "Prof. Johnson", NumberOfCredits = 3 },
                    new Course { CourseId = "BBA 201", CourseName = "Introduction to Business", InstructorName = "Prof. Williams", NumberOfCredits = 3 }

            };

            //but here listRef's type is List<Course>
            listRef = new List<Course>();
            if (student.CoursesInEachSemester == null)
            {
                student.CoursesInEachSemester = listRef;
            }

            //  courses student hasn't taken yet
            var coursesNotTaken = hardcodedCourses.Where(course => !student.CoursesInEachSemester.Any(c => c.CourseId == course.CourseId));

            //  available courses
            Console.WriteLine("Available Courses:");
            foreach (var course in coursesNotTaken)
            {
                Console.WriteLine($"{course.CourseId} - {course.CourseName}");
            }


            Console.WriteLine("Enter the Course IDs to add (comma-separated):");
            string input = Console.ReadLine();
            string[] selectedCourseIDs = input.Split(',');


            foreach (var courseID in selectedCourseIDs)
            {
                Course selectedCourse = hardcodedCourses.FirstOrDefault(course => course.CourseId == courseID.Trim());
                if (selectedCourse != null)
                {
                    student.CoursesInEachSemester.Add(selectedCourse);
                    Console.WriteLine($"Added course {selectedCourse.CourseName}");
                }
                else
                {
                    Console.WriteLine($"Course with ID {courseID} not found.");
                }
            }
        }


        public static void DeleteStudent(IStudentManagement studentManager)
        {
            Console.Write("Enter Student ID: ");
            string studentID = Console.ReadLine();

            studentManager.DeleteStudent(studentID);
            Console.WriteLine("Student deleted successfully!");
        }
        public static void AddNewStudent(IStudentManagement studentManager)
        {
            Console.WriteLine("Enter student details:");


            Student newStudent = new Student();
            Console.Write("First Name: ");
            newStudent.FirstName = Console.ReadLine();
            Console.Write("Middle Name: ");
            newStudent.MiddleName = Console.ReadLine();
            Console.Write("Last Name: ");
            newStudent.LastName = Console.ReadLine();
            Console.Write("Student ID: format xxx-xxx-xxx: ");
            newStudent.StudentId = Console.ReadLine();
            Console.Write("Joining Batch: XXX (Semester code format) one space YYYY (year format) - xxx yyyy  :");
            string[] JoiningBatchInfo = Console.ReadLine().Split();

            newStudent.JoiningBatch = new Semester(JoiningBatchInfo[0], JoiningBatchInfo[1]);
            Console.Write("Department (0 for ComputerScience, 1 for BBA, 2 for English): ");
            newStudent.Department = (Department)Enum.Parse(typeof(Department), Console.ReadLine());
            Console.Write("Degree (0 for BSC, 1 for BBA, 2 for BA, 3 for MSC, 4 for MBA, 5 for MA): ");
            newStudent.Degree = (Degree)Enum.Parse(typeof(Degree), Console.ReadLine());

            studentManager.AddStudent(newStudent);
            Console.WriteLine("Student added successfully!");
        }
    }
}
