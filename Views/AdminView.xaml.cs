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
using System.Windows.Shapes;

namespace Student_Attendence_Management_System.Views
{
    public partial class AdminView : Window
    {
        public AdminView(Admin admin)
        {
            InitializeComponent();
            NameTextBox.Text = admin.Name;
            IDTextBox.Text = admin.AdminId;
            Main.Content = new AdminStudent();
            Student.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7b5cd6"));
            Student.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"));
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

        private void Student_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new AdminStudent();

            ResetColor();
            Student.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7b5cd6"));
            Student.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"));
        }

        private void Teacher_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new AdminTeacher();

            ResetColor();
            Teacher.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7b5cd6"));
            Teacher.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"));
        }

        private void Courses_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new AdminCourse();

            ResetColor();
            Courses.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7b5cd6"));
            Courses.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"));
        }

        private void CourseReg_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new AdminCourseReg();

            ResetColor();
            CourseReg.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7b5cd6"));
            CourseReg.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"));
        }

        void ResetColor()
        {
            Student.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#623ed0"));
            Student.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#d0c0ff"));

            Teacher.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#623ed0"));
            Teacher.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#d0c0ff"));

            Courses.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#623ed0"));
            Courses.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#d0c0ff"));

            CourseReg.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#623ed0"));
            CourseReg.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#d0c0ff"));
        }
    }

}
