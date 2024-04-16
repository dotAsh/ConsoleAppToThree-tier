using System;
using System.ComponentModel;


namespace StudentManagementSystem.DAL
{
    [Serializable]
    
    public class Semester
    {
        //Attributes is used here 
        [DefaultValue(0)]
        public string SemesterCode { get; set; }
        public string Year { get; set; }

       
        public Semester(string semcode, string y)
        {
            SemesterCode = semcode;
            Year = y;
        }
    }
}
