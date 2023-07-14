using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Win32;
using StudentManagementSystem.Messages;
using StudentManagementSystem.Models;
using StudentManagementSystem.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace StudentManagementSystem.ViewModels
{
    public partial class MainWindowVM : ObservableObject
    {
        // Students list for get and hold the student data from temporary database. this list act as main student list
        public List<Student> Students { get; set; }

        //Declaring the list of existing department
        public List<Department> Departments { get; set; }

        // FillteringStudents list for search function only.
        [ObservableProperty]
        public ObservableCollection<Student> fillteredStudents;
        [ObservableProperty]
        public Visibility notItemFoundTextVisibility;
        [ObservableProperty]
        public string searchText;

        //When user in editing one student details, To avoid user press other buttons unexpectedly
        [ObservableProperty]
        public bool studentListViewEnable;


        //Declaring Add new Student Window & edit Student window 
        public Window AddNewStudentWindow { get; set; }
        public Window EditExistingWindow { get; set; }


        public MainWindowVM()
        {
            //Defining Departments and Students Temporary data
            Departments = new List<Department>()
            {
                new Department(1,"Electrical Engineering","C:\\Users\\bhanu\\Desktop\\StudentManagementSystem\\StudentManagementSystem\\Images\\electrical.png"),
                new Department(2,"Computer Engineering","C:\\Users\\bhanu\\Desktop\\StudentManagementSystem\\StudentManagementSystem\\Images\\mechanical.png"),
                new Department(3,"Mechanical Engineering","C:\\Users\\bhanu\\Desktop\\StudentManagementSystem\\StudentManagementSystem\\Images\\civil.png"),
                new Department(4,"Civil Engineering","C:\\Users\\bhanu\\Desktop\\StudentManagementSystem\\StudentManagementSystem\\Images\\computer.png"),
            };
            Students = new List<Student>() {
                    new Student()
                    {
                        FirstName = "Jelani",
                        LastName = "Wise",
                        ImagePath ="C:\\Users\\bhanu\\Desktop\\StudentManagementSystem\\StudentManagementSystem\\Images\\1.png",
                        DoB = DateTime.Now,
                        Department = Departments[0],
                        GpaValue = 3,
                    },
                    new Student()
                    {
                        FirstName = "Cade",
                        LastName = "Nielsen",
                        ImagePath ="C:\\Users\\bhanu\\Desktop\\StudentManagementSystem\\StudentManagementSystem\\Images\\2.png",
                        DoB = DateTime.Now,
                        Department = Departments[1],
                        GpaValue = 3,
                    },
                    new Student()
                    {
                        FirstName = "Oscar",
                        LastName = "Rocha",
                        ImagePath ="C:\\Users\\bhanu\\Desktop\\StudentManagementSystem\\StudentManagementSystem\\Images\\3.png",
                        DoB = DateTime.Now,
                        Department = Departments[2],
                        GpaValue = 3,
                    },
                    new Student()
                    {
                        FirstName = "Nehru",
                        LastName = "Burks",
                        ImagePath ="C:\\Users\\bhanu\\Desktop\\StudentManagementSystem\\StudentManagementSystem\\Images\\10.png",
                        DoB = DateTime.Now,
                        Department = Departments[3],
                        GpaValue = 3,
                    },
                    new Student()
                    {
                        FirstName = "Virginia",
                        LastName = "Dunn",
                        ImagePath ="C:\\Users\\bhanu\\Desktop\\StudentManagementSystem\\StudentManagementSystem\\Images\\4.png",
                        DoB = DateTime.Now,
                        Department = Departments[0],
                        GpaValue = 3,
                    },
                    new Student()
                    {
                        FirstName = "Bhanuka",
                        LastName = "Anjana",
                        ImagePath ="C:\\Users\\bhanu\\Desktop\\StudentManagementSystem\\StudentManagementSystem\\Images\\5.png",
                        DoB = DateTime.Now,
                        Department = Departments[2],
                        GpaValue = 3,
                    },
                    new Student()
                    {
                        FirstName = "Kelsie",
                        LastName = "Baird",
                        ImagePath ="C:\\Users\\bhanu\\Desktop\\StudentManagementSystem\\StudentManagementSystem\\Images\\6.png",
                        DoB = DateTime.Now,
                        Department = Departments[1],
                        GpaValue = 3,
                    },
                    new Student()
                    {
                        FirstName = "Dante",
                        LastName = "Maxwell",
                        ImagePath ="C:\\Users\\bhanu\\Desktop\\StudentManagementSystem\\StudentManagementSystem\\Images\\7.png",
                        DoB = DateTime.Now,
                        Department = Departments[3],
                        GpaValue = 3,
                    },
                    new Student()
                    {
                        FirstName = "Decker",
                        LastName = "Blythe",
                        ImagePath ="C:\\Users\\bhanu\\Desktop\\StudentManagementSystem\\StudentManagementSystem\\Images\\8.png",
                        DoB = DateTime.Now,
                        Department = Departments[3],
                        GpaValue = 3,
                    },
                    new Student()
                    {
                        FirstName = "Kieran",
                        LastName = "Mosley",
                        ImagePath ="C:\\Users\\bhanu\\Desktop\\StudentManagementSystem\\StudentManagementSystem\\Images\\9.png",
                        DoB = DateTime.Now,
                        Department = Departments[1],
                        GpaValue = 3,
                    },
                    new Student()
                    {
                        FirstName = "Latifah",
                        LastName = "Lucas",
                        ImagePath ="C:\\Users\\bhanu\\Desktop\\StudentManagementSystem\\StudentManagementSystem\\Images\\4.png",
                        DoB = DateTime.Now,
                        Department = Departments[0],
                        GpaValue = 3,
                    },
            };

            FillteredStudents = new ObservableCollection<Student>(Students);
            NotItemFoundTextVisibility = Visibility.Hidden;

            //Register Message to receive student object after user created
            WeakReferenceMessenger.Default.Register<NewStudentDetailsMessage>(this, OnAddingNewStudent);

            //Register Message to receive close add-new-student window
            WeakReferenceMessenger.Default.Register<CloseWindowMessage>(this, OnCloseAddNewStudentWindow);

            //Register Message to receive update Edited Student Details
            WeakReferenceMessenger.Default.Register<UpdateStudentDetailsMessage>(this, OnStudentDetailsUpdated);
        }
        //Event-Handler for the change the details for the updated details of the student
        private void OnStudentDetailsUpdated(object recipient, UpdateStudentDetailsMessage message)
        {
            if (message != null)
            {
                EditExistingWindow.Close();

                //Apply to the updated details to the related student
                Students.Where(s=>s.Id==message.Value.Id).FirstOrDefault().FirstName = message.Value.FirstName;
                Students.Where(s=>s.Id==message.Value.Id).FirstOrDefault().LastName = message.Value.LastName;
                Students.Where(s=>s.Id==message.Value.Id).FirstOrDefault().DoB = message.Value.DoB;
                Students.Where(s=>s.Id==message.Value.Id).FirstOrDefault().Department = message.Value.Department;
                Students.Where(s=>s.Id==message.Value.Id).FirstOrDefault().ImagePath = message.Value.ImagePath;
                Students.Where(s=>s.Id==message.Value.Id).FirstOrDefault().GpaValue = message.Value.GpaValue;

                FillteredStudents = new ObservableCollection<Student>(Students.ToList());

            }
        }

        //Filltering out the Displayed student list when text of the search box changed
        partial void OnSearchTextChanged(string value)
        {
            if (!string.IsNullOrEmpty(value) || !string.IsNullOrWhiteSpace(value))
            {
                FillteredStudents = new ObservableCollection<Student>(Students.Where(s => s.FirstName.ToLower().StartsWith(value.ToLower()) || s.LastName.ToLower().StartsWith(value.ToLower())).ToList()); ;
                if (FillteredStudents.Count == 0 && (!string.IsNullOrEmpty(value) || !string.IsNullOrWhiteSpace(value)))
                {
                    NotItemFoundTextVisibility = Visibility.Visible;
                }
            }
            if (string.IsNullOrEmpty(value))
            {
                NotItemFoundTextVisibility = Visibility.Hidden;
                FillteredStudents = new ObservableCollection<Student>(Students);
            }
        }
        [RelayCommand]
        public void EditStudent(int studentId)
        {
            EditExistingWindow = new EditUserWindow();
            //Declaring & Defining new student for selected student
            Student student = new Student
                (
                FillteredStudents.Where(s => s.Id == studentId).FirstOrDefault().Id,
                FillteredStudents.Where(s => s.Id == studentId).FirstOrDefault().FirstName,
                FillteredStudents.Where(s => s.Id == studentId).FirstOrDefault().LastName,
                FillteredStudents.Where(s => s.Id == studentId).FirstOrDefault().ImagePath,
                FillteredStudents.Where(s => s.Id == studentId).FirstOrDefault().DoB,
                FillteredStudents.Where(s => s.Id == studentId).FirstOrDefault().Department,
                FillteredStudents.Where(s => s.Id == studentId).FirstOrDefault().GpaValue);

            //Send the selected student's details into student's details editing windowVM
            WeakReferenceMessenger.Default.Send<EditStudentDetailsMessage>(new EditStudentDetailsMessage(student));
            EditExistingWindow.ShowDialog();
        }
        [RelayCommand]
        public void DeleteStudent(int studentId)
        {   
            //Getting conform if user really want to delete that student using alert message box
            if(MessageBox.Show("Do you want remove this student!", "Conformation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Student student = Students.Where(s => s.Id == studentId).First();
                Students.Remove(student);
                FillteredStudents = new ObservableCollection<Student> (Students);
            }
        }
        [RelayCommand]
        public void BackToStudentListing()
        {
        }
        [RelayCommand]
        public void AddNewStudent()
        {
            AddNewStudentWindow = new AddNewStudent();
            AddNewStudentWindow.ShowDialog();
        }
        [RelayCommand]
        public void WindowControl(string command)
        {
            if(command == "minimize")
            {
                App.Current.MainWindow.WindowState = System.Windows.WindowState.Minimized;
            }
            if(command == "close")
            {
                App.Current.Shutdown();
            }
        }

        //EventHandler for NewStudentDetailsMessage
        private void OnAddingNewStudent(object recipient, NewStudentDetailsMessage message)
        {
            AddNewStudentWindow.Close();
            Students.Add(message.Value);
            FillteredStudents = new ObservableCollection<Student>(Students);
        }
        //EventHandler for Close Add-New-Student-Window
        private void OnCloseAddNewStudentWindow(object recipient, CloseWindowMessage message)
        {
            if (message.Value == "new")
            {
                AddNewStudentWindow.Close();
            }else if (message.Value == "edit")
            {
                EditExistingWindow.Close();
            }

        }


    }
}
