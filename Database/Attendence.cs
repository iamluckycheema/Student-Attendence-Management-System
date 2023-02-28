using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Attendence_Management_System.Database
{
    public class Attendance
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [ForeignKey("Student")]
        public string StudentId { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }
    }
}
