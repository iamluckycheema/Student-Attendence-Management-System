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
    public partial class AdminTeacher : Page
    {
        public AdminTeacher()
        {
            InitializeComponent();
            LoadGridTeacher();
        }
        List<Teacher> teacher;
        private void LoadGridTeacher()
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
            {
                connection.CreateTable<Teacher>();
                teacher = connection.Table<Teacher>().ToList().OrderBy(c => c.Id).ToList();
            }
            if (teacher != null)
            {
                MembersDataGrid.ItemsSource = teacher;
            }
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox searchTextBox = sender as TextBox;
            var filterdList = teacher.Where(c => c.Name.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();
            MembersDataGrid.ItemsSource = filterdList;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text != null && RollNumberTextBox.Text != null && PasswordTextBox.Text != null)
            {
                using (var db = new SQLiteConnection(App.DatabasePath))
                {
                    var teacher = new Teacher
                    {
                        Name = NameTextBox.Text,
                        FacultyId = RollNumberTextBox.Text,
                        Password = PasswordTextBox.Text
                    };
                    var teacherP = db.Table<Teacher>().FirstOrDefault(s => s.FacultyId == teacher.FacultyId);

                    if (teacherP == null)
                    {
                        db.CreateTable<Teacher>();
                        db.Insert(teacher);
                        LoadGridTeacher();
                    }
                    else
                    {
                        MessageBox.Show("Can't add user matching Id Found!", "Add Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please fill all the fields first", "Student Add Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
                {
                    connection.CreateTable<Teacher>();
                    foreach (var teacher in MembersDataGrid.Items)
                    {
                        connection.Update(teacher);
                    }
                }
                MessageBox.Show("Recordes Updated!", "Update", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            LoadGridTeacher();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Teacher selectedTeacher = (Teacher)MembersDataGrid.SelectedItem;
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
                {
                    connection.CreateTable<Teacher>();
                    connection.Delete(selectedTeacher);
                    MessageBox.Show("Recordes Deleted!", "Delete", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            LoadGridTeacher();
        }
    }
}
