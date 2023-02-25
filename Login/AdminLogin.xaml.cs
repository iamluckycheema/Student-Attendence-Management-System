using SQLite;
using Student_Attendence_Management_System.Database;
using Student_Attendence_Management_System.Views;
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

namespace Student_Attendence_Management_System.Login
{
    public partial class AdminLogin : Window
    {
        public AdminLogin()
        {
            InitializeComponent();
            try
            {
                using (var db = new SQLiteConnection(App.DatabasePath))
                {
                    var admin = new Admin
                    {
                        Name = "Lucky Ali",
                        AdminId = "20011556-038",
                        Password = "1234"
                    };
                    var adminP = db.Table<Admin>().FirstOrDefault(s => s.AdminId == admin.AdminId);

                    if (adminP == null)
                    {
                        db.CreateTable<Admin>();
                        db.Insert(admin);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void textStudentID_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtStudentID.Focus();
        }

        private void txtStudentID_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtStudentID.Text) && txtStudentID.Text.Length > 0)
            {
                textStudentID.Visibility = Visibility.Collapsed;
            }
            else
            {
                textStudentID.Visibility = Visibility.Visible;
            }
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtPassword.Focus();
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Password) && txtPassword.Password.Length > 0)
            {
                textPassword.Visibility = Visibility.Collapsed;
                }
            else
            {
                textPassword.Visibility = Visibility.Visible;
            }
        }

        private void StudentSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Password) && !string.IsNullOrEmpty(txtStudentID.Text))
            {
                using (var conn = new SQLiteConnection(App.DatabasePath))
                {
                    var admin = conn.Table<Admin>().FirstOrDefault(s => s.AdminId == txtStudentID.Text);

                    if (admin != null && admin.Password == txtPassword.Password)
                    {
                        AdminView view = new AdminView(admin);
                        view.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Password or Admin Id is incorrect!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Fill all the Details!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
            this.Close();
        }
    }
}
