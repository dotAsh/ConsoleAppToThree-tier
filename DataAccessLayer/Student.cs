using System;
using System.Collections.Generic;


namespace StudentManagementSystem.DAL
{
    
    public class Student
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        //Nullables used 
        public string? LastName { get; set; }
        public string StudentId { get; set; }
        public Semester JoiningBatch { get; set; } 
        public Department Department { get; set; }
        public Degree Degree { get; set; }
       // Nullables used here
        public List<Semester>? SemestersAttended { get; set; }
        // Nullables used here
        public List<Course>? CoursesInEachSemester { get; set; }
        public Student()
        {
            SemestersAttended = new List<Semester>();
            CoursesInEachSemester = new List<Course>();
        }
        public override string ToString()
        {
            return $"{FirstName} {LastName} ({StudentId})";
        }

    }
    public static class StudentExtensions
    {
        public static void AddSemester(this Student student, string semesterCode, string year)
        {
            if (student.SemestersAttended == null)
            {
                student.SemestersAttended = new List<Semester>();
            }
            Semester newSemester = new Semester(semesterCode, year);
            student.SemestersAttended.Add(newSemester);
        }
    }


}
