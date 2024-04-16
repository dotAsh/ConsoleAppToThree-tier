using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.DAL
{
    public interface IStudentDataAccess
    {
        

        public List<Student>? LoadStudentsFromJson();
       

        public void SaveStudentsToJson(List<Student> students);
       

    }
}
