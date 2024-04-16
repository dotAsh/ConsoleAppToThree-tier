

namespace StudentManagementSystem.DAL
{
    public class Course
    {
        private string courseId;
        public string CourseId
        {
            get { return courseId; }
            set
            {
                string []test = value.Split(' ');
                if (test.Length == 2 && test[0].Length == 3 && test[1].Length == 3)
                {
                    courseId = value;
                }
                else
                {
                    throw new Exception("Invalid CourId format! Correct formet: XXX-XXX");
                }
            }
        }
        public string CourseName { get; set; }
        public string InstructorName { get; set; }
        public int NumberOfCredits { get; set; }
    }
}
