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
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Student_Attendence_Management_System.Database;
using SQLite;
using System.Diagnostics.Contracts;
using Student_Attendence_Management_System.DataViews;

namespace Student_Attendence_Management_System.Views
{

    public partial class StudentView : Window
    {
        Student student1;
        StudentAttendenceCourses course1;
        public StudentView(Student student)
        {
            InitializeComponent();
            NameTextBlock.Text = student.Name;
            IDTextBlock.Text = student.RollNumber;
            student1=student;
            populateCourses();
        }

        List<Attendance> attendance;
        private void LoadGrid()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
                {
                    connection.CreateTable<Attendance>();
                    attendance = connection.Table<Attendance>().Where(s => s.StudentId == student1.RollNumber && s.CourseId == course1.CourseID).ToList().OrderBy(c => c.Date).ToList();
                }
                if (attendance != null)
                {
                    MembersDataGrid.ItemsSource = attendance;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            CourseNameTextBlock.Text = course1.CourseName;
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }

        }

        private bool IsMaximized = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount == 2)
            {
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximized = true;
                }
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox searchTextBox = sender as TextBox;
            var filterdList = attendance.Where(c => c.Date.ToString().ToLower().Contains(searchTextBox.Text.ToLower()))
                .Where(s => s.StudentId == student1.RollNumber && s.CourseId == course1.CourseID).ToList();
            MembersDataGrid.ItemsSource = filterdList;
        }

        void populateCourses()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
                {
                    connection.CreateTable<CourseEnrollment>();
                    connection.CreateTable<Course>();
                    var query = connection.Table<Course>()
                        .Join(connection.Table<CourseEnrollment>(),
                        course => course.Id,
                        enrollment => enrollment.CourseId,
                        (course, enrollment) => new {course, enrollment }
                        )
                        .Where(s => s.enrollment.StudentId == student1.RollNumber);
                    foreach (var record in query)
                    {
                        var newRecord = new StudentAttendenceCourses()
                        {
                            CourseID = record.course.Id,
                            CourseName = record.course.Name
                        };
                        CourseSelect.Items.Add(newRecord);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CourseSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StudentAttendenceCourses selectedCourse = (StudentAttendenceCourses)CourseSelect.SelectedItem;
            course1 = selectedCourse;
            LoadGrid();
        }
    }

}
