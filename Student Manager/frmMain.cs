using Student_Manager.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Manager
{
    public partial class frmMain : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode Mode = enMode.AddNew;

        private DataTable _dtStudents = new DataTable() { Columns = { "Student ID", "Student Name" , "Grade" } };

        List<clsStudent> _students = new List<clsStudent>() 
        {
           new clsStudent { StudentID = 1, StudentName = "Ahmed" , Grade= 86 } ,
           new clsStudent { StudentID = 2, StudentName = "Hamze" , Grade= 75 } ,
           new clsStudent { StudentID = 3, StudentName = "Omar" , Grade= 66 }  ,
           new clsStudent { StudentID = 4, StudentName = "Khaled" , Grade= 76 } , 
           new clsStudent { StudentID = 5, StudentName = "Zaid0" , Grade= 58 }
        };

        public frmMain()
        {
            InitializeComponent();
        }

        private void RefreshStudentsList()
        {
            _dtStudents.Rows.Clear();

            //foreach (clsStudent Student in _students)
            //{
            //    _dtStudents.Rows.Add(Student.StudentID , Student.StudentName , Student.Grade);
            //}

            _students.ForEach(Student => _dtStudents.Rows.Add(Student.StudentID, Student.StudentName, Student.Grade));

            dgvStudents.DataSource = _dtStudents;

            dgvStudents.Columns[0].Width = 150;
            dgvStudents.Columns[1].Width = 250;
            dgvStudents.Columns[2].Width = 150;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //_dtStudents.Columns.Add("Student ID" );
            //_dtStudents.Columns.Add("Student Name" );
            //_dtStudents.Columns.Add("Grade" );

            RefreshStudentsList();
        }

        private void ResatDefualtValues()
        {
            lblMode.Text = (Mode == enMode.AddNew) ? "Add New Student" : "Edit Student";

            lblStudentID.Text = "N/A";
            txtName.Text = "";
            txtGrade.Text = "";

            btnAddEdit.Text = (Mode == enMode.AddNew) ? "Add": "Update";

        }

        private void btnAddNewStudent_Click(object sender, EventArgs e)
        {
            Mode = enMode.AddNew;

            ResatDefualtValues();

            // OpenCloesSubMenu();

            while (this.Size.Width < 865)
            {
                this.Size = new Size(this.Size.Width + 1, this.Size.Height);
            }

        }

        private void btnAddEdit_Click(object sender, EventArgs e)
        {
            if (Mode == enMode.AddNew)
            {
                int LastID = _students[_students.Count - 1].StudentID;
                _students.Add(new clsStudent { StudentID = 1 + LastID, StudentName = txtName.Text, Grade = int.Parse(txtGrade.Text) });

                lblStudentID.Text = _students[_students.Count - 1].StudentID.ToString();

            }
            else
            {
                int StudentID = Convert.ToInt32(dgvStudents.CurrentRow.Cells[0].Value);

                //clsStudent student = _students.Where(stu => stu.StudentID == StudentID).FirstOrDefault(); ;

                clsStudent student = _students.Find(stu => stu.StudentID == StudentID);

                if (student == null)
                    return;

                student.StudentName = txtName.Text;
                student.Grade = int.Parse(txtGrade.Text);

            }

            RefreshStudentsList();
        }

        private void tsmEdit_Click(object sender, EventArgs e)
        {
            Mode = enMode.Update;
            ResatDefualtValues();

            int StudentID = Convert.ToInt32(dgvStudents.CurrentRow.Cells[0].Value);

            lblStudentID.Text = StudentID.ToString();

            //clsStudent student = _students.Where(stu => stu.StudentID == StudentID).FirstOrDefault() ;

            clsStudent student = _students.Find(stu => stu.StudentID == StudentID);

            if (student == null)
                return;

            txtName.Text = student.StudentName;
            txtGrade.Text = student.Grade.ToString();


            //OpenCloesSubMenu();

            while (this.Size.Width < 865)
            {
                this.Size = new Size(this.Size.Width + 1, this.Size.Height);
            }

        }

        private void OpenCloesSubMenu()
        {
            //865, 774 637, 774

            Size sOpen = new Size(865, 774);
            Size sCloes = new Size(637, 774);

            if (this.Size.Width == sOpen.Width)
            {
                //this.Size = sCloes;

                while (this.Size.Width > 637)
                {
                    this.Size = new Size(this.Size.Width - 1, this.Size.Height);
                }
            }
            else
            {
                //this.Size = sOpen;

                while (this.Size.Width < 865)
                {
                    this.Size = new Size(this.Size.Width + 2, this.Size.Height);
                }

            }
        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            int StudentID = Convert.ToInt32(dgvStudents.CurrentRow.Cells[0].Value);

            _students.RemoveAll(student => student.StudentID == StudentID);

            RefreshStudentsList();
        }

        private void txtGrade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnCloesSubMenu_Click(object sender, EventArgs e)
        {
            OpenCloesSubMenu();
        }
    }
}
