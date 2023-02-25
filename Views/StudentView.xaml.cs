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

namespace Student_Attendence_Management_System.Views
{

    public partial class StudentView : Window
    {
        public StudentView(Student student)
        {
            InitializeComponent();
            NameTextBlock.Text = student.Name;
            IDTextBlock.Text = student.RollNumber;
            LoadGrid();
        }

        List<Attendance> attendance;
        private void LoadGrid()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
                {
                    connection.CreateTable<Attendance>();
                    attendance = connection.Table<Attendance>().ToList().OrderBy(c => c.Date).ToList();
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

        }
    }

}
