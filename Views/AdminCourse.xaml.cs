using SQLite;
using Student_Attendence_Management_System.Database;
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

namespace Student_Attendence_Management_System.Views
{
    public partial class AdminCourse : Page
    {
        public AdminCourse()
        {
            InitializeComponent();
            LoadGridCourse();
            LoadGirdTeacher();
        }

        List<Course> course;
        private void LoadGridCourse()
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
            {
                connection.CreateTable<Course>();
                course = connection.Table<Course>().ToList().OrderBy(c => c.Id).ToList();
                MembersDataGrid.ItemsSource = course;
            }
        }

        List<Teacher> teacher;
        private void LoadGirdTeacher()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
                {
                    connection.CreateTable<Teacher>();
                    teacher = connection.Table<Teacher>().ToList().OrderBy(c => c.Id).ToList();
                }
                if (teacher != null)
                {
                    TeachersDataGrid.ItemsSource = teacher;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox searchTextBox = sender as TextBox;
            var filterdList = course.Where(c => c.Name.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();
            MembersDataGrid.ItemsSource = filterdList;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text != null && TeacherTextBox.Text != null)
            {
                try
                {
                    using (var db = new SQLiteConnection(App.DatabasePath))
                    {
                        var course = new Course
                        {
                            Name = NameTextBox.Text,
                            TeacherID = TeacherTextBox.Text,
                        };

                        db.CreateTable<Course>();
                        db.Insert(course);
                        LoadGridCourse();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please fill all the fields first", "Student Add Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Course selectedCourse = (Course)MembersDataGrid.SelectedItem;
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
            LoadGridCourse();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Course selectedCourse = (Course)MembersDataGrid.SelectedItem;
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
                {
                    connection.CreateTable<Course>();
                    connection.Delete(selectedCourse);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Record Deleted!", "Delete", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadGridCourse();
        }
    }
}
