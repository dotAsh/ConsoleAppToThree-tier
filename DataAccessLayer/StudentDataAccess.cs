using Newtonsoft.Json;
using StudentManagementSystem.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.DAL
{
    public class StudentDataAccess: IStudentDataAccess
    {
        private string filePath;

        public StudentDataAccess(string path)
        {
            FilePath = path;
        }
        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }


        public List<Student>? LoadStudentsFromJson()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    return JsonConvert.DeserializeObject<List<Student>>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while loading students from file: {ex.Message}");
            }
            return new List<Student>();
        }

        public void SaveStudentsToJson(List<Student> students)
        {
            try
            {
                string json = JsonConvert.SerializeObject(students);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while saving students to file: {ex.Message}");
            }
        }

       

    }
}
