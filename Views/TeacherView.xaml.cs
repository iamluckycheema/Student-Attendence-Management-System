using SQLite;
using Student_Attendence_Management_System.Database;
using Student_Attendence_Management_System.DataViews;
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

namespace Student_Attendence_Management_System.Views
{
    public partial class TeacherView : Window 
    { 
        Teacher teacher1;
        Course course1;
        public TeacherView(Teacher teacher)
        {
            InitializeComponent();
            NameTextBlock.Text = teacher.Name;
            IDTextBlock.Text = teacher.FacultyId;
            teacher1 = teacher;
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
                    attendance = connection.Table<Attendance>().Where(s => s.CourseId == course1.Id).Distinct().ToList().OrderBy(c => c.Date).ToList();
                }
                if (attendance != null)
                {
                    MembersDataGrid.ItemsSource = attendance;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            CourseNameTextBlock.Text = course1.Name;
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
        if (e.ClickCount == 2)
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
                .Where(s => s.CourseId == course1.Id).ToList();
            MembersDataGrid.ItemsSource = filterdList;
        }

        void populateCourses()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
                {
                    connection.CreateTable<Course>();
                    var query = connection.Table<Course>().Where(s => s.TeacherID == teacher1.FacultyId).ToList().OrderBy(c => teacher1.FacultyId).ToList();
                    CourseSelect.ItemsSource= query;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CourseSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Course selectedCourse = (Course)CourseSelect.SelectedItem;
            course1 = selectedCourse;
            LoadGrid();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
