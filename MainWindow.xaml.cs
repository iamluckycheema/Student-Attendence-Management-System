using SQLite;
using Student_Attendence_Management_System.Database;
using Student_Attendence_Management_System.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Student_Attendence_Management_System
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            using (var db = new SQLiteConnection(App.DatabasePath))
            {
                db.CreateTable<Admin>();
                db.CreateTable<Student>();
                db.CreateTable<Course>();
                db.CreateTable<Attendance>();
                db.CreateTable<CourseEnrollment>();
                db.CreateTable<Teacher>();
            }
        }

        private void Student_Click(object sender, RoutedEventArgs e)
        {
            StudentLogin loginStudent = new StudentLogin();
            loginStudent.ShowDialog();
        }

        private void Teacher_Click(object sender, RoutedEventArgs e)
        {
            TeacherLogin loginTeacher = new TeacherLogin();
            loginTeacher.ShowDialog();

        }

        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            AdminLogin loginAdmin = new AdminLogin();
            loginAdmin.ShowDialog();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
