using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Attendence_Management_System.Database
{
    public class Teacher
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        [NotNull,Unique]
        public string FacultyId { get; set; }
        [NotNull]
        public string Password { get; set; }
    }
}
