using SQLite;
using Student_Attendence_Management_System.Database;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Student_Attendence_Management_System.Views
{
    public partial class AdminCourseReg : Page
    {
        public AdminCourseReg()
        {
            InitializeComponent();
            LoadGridEnrollment();
            LoadGridCourse();
            LoadGirdStudent();
        }

        List<CourseEnrollment> enrollments;
        private void LoadGridEnrollment()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
                {
                    connection.CreateTable<CourseEnrollment>();
                    enrollments = connection.Table<CourseEnrollment>().ToList().OrderBy(c => c.StudentId).ToList();
                }
                if (enrollments != null)
                {
                    MembersDataGrid.ItemsSource = enrollments;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //try
            //{
            //    using (var conn = new SQLiteConnection(App.DatabasePath))
            //    {
            //        conn.CreateTable<Course>();
            //        conn.CreateTable<Student>();
            //        conn.CreateTable<CourseEnrollment>();

            //        var query = conn.Table<Course>()
            //            .Join(
            //                conn.Table<CourseEnrollment>(),
            //                c => c.Id,
            //                ce => ce.CourseId,
            //                (c, ce) => new { ce.Id, Course = c.Name, ce.StudentId })
            //            .Join(
            //                conn.Table<Student>(),
            //                r => r.StudentId,
            //                s => s.RollNumber,
            //                (r, s) => new CourseRegView { Id=r.Id, CourseName=r.Course, StudentName=s.Name }
            //                )
            //            .ToList();
            //        MembersDataGrid.ItemsSource = query;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

        }

        List<Course> course;
        private void LoadGridCourse()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
                {
                    connection.CreateTable<Course>();
                    course = connection.Table<Course>().ToList().OrderBy(c => c.Id).ToList();
                    CourseDataGrid.ItemsSource = course;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        List<Student> student;
        private void LoadGirdStudent()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
                {
                    connection.CreateTable<Student>();
                    student = connection.Table<Student>().ToList().OrderBy(c => c.Id).ToList();
                }
                if (student != null)
                {
                    StudentDataGrid.ItemsSource = student;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox searchTextBox = sender as TextBox;
            var filterdList = enrollments.Where(c => c.StudentId.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();
            MembersDataGrid.ItemsSource = filterdList;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CourseTextBox.Text != null && RollTextBox.Text != null)
                {
                    using (var db = new SQLiteConnection(App.DatabasePath))
                    {
                        var check = db.Table<CourseEnrollment>().FirstOrDefault(s => s.StudentId == RollTextBox.Text);

                        if (check != null && check.CourseId == int.Parse(CourseTextBox.Text))
                        {
                            MessageBox.Show("Recored Already Exists!");
                        }
                        else
                        {
                            var course = new CourseEnrollment
                            {
                                StudentId = RollTextBox.Text,
                                CourseId = int.Parse(CourseTextBox.Text)
                            };

                            db.CreateTable<CourseEnrollment>();
                            db.Insert(course);
                            LoadGridEnrollment();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please fill all the fields first", "Add Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex){
                MessageBox.Show(ex.Message);
            }
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            CourseEnrollment selectedCourse = (CourseEnrollment)MembersDataGrid.SelectedItem;
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
                {
                    connection.CreateTable<Course>();
                    connection.Update(selectedCourse);
                }
                MessageBox.Show("Record Updated!", "Update", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            LoadGridEnrollment();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            CourseEnrollment selectedCourse = (CourseEnrollment)MembersDataGrid.SelectedItem;
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
                {
                    connection.CreateTable<CourseEnrollment>();
                    connection.Delete(selectedCourse);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Record Deleted!", "Delete", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadGridEnrollment();
        }
    }
}

