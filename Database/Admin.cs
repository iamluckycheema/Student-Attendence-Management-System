using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Attendence_Management_System.Database
{
    public class Admin
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        [Unique]
        public string AdminId { get; set; }
        public string Password { get; set; }
    }
}
