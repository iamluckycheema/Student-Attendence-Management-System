using SQLite;
using Student_Attendence_Management_System.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    public partial class AdminStudent : Page
    {
        public AdminStudent()
        {
            InitializeComponent();
            LoadGridStudent();
        }
        List<Student> student;
        private void LoadGridStudent()
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
            {
                connection.CreateTable<Student>();
                student = connection.Table<Student>().ToList().OrderBy(c => c.Id).ToList();
            }
            if (student != null)
            {
                MembersDataGrid.ItemsSource = student;
            }
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox searchTextBox = sender as TextBox;
            var filterdList = student.Where(c => c.Name.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();
            MembersDataGrid.ItemsSource = filterdList;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text != null && RollNumberTextBox.Text != null && PasswordTextBox.Text!= null)
            {
                using (var db = new SQLiteConnection(App.DatabasePath))
                {
                    var student = new Student
                    {
                        Name = NameTextBox.Text,
                        RollNumber = RollNumberTextBox.Text,
                        Password = PasswordTextBox.Text
                    };
                    var studentP = db.Table<Student>().FirstOrDefault(s => s.RollNumber == student.RollNumber);

                    if (studentP == null)
                    {
                        db.CreateTable<Student>();
                        db.Insert(student);
                        LoadGridStudent();
                    }
                    else
                    {
                        MessageBox.Show("Can't add user matching Id Found!", "Add Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please fill all the fields first","Student Add Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
                {
                    connection.CreateTable<Student>();
                    foreach (var student in MembersDataGrid.Items)
                    {
                        connection.Update(student);
                    }
                }
                MessageBox.Show("Recordes Updated!", "Update", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            LoadGridStudent();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Student selectedStudent = (Student)MembersDataGrid.SelectedItem;
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
                {
                    connection.CreateTable<Student>();
                    connection.Delete(selectedStudent);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Record Deleted!", "Delete", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadGridStudent();
        }
    }
}
