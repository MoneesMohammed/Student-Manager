using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Manager
{
    public class clsStudent
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public int Grade { get; set; }

        public clsStudent()
        {
            this.StudentID = -1;
            this.StudentName = "";
            this.Grade = 0;

        }

    }

}
