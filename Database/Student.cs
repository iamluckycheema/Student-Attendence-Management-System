using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Student_Attendence_Management_System.Database
{
    public class Student
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        [Unique, NotNull]
        public string RollNumber { get; set; }
        [NotNull]
        public string Password { get; set; }
    }
}
