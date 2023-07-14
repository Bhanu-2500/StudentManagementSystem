using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Win32;
using StudentManagementSystem.Messages;
using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace StudentManagementSystem.ViewModels
{
    public partial class AddNewStudentWindowVM : ObservableObject
    {

        [ObservableProperty]
        public string newStudentFirstName;
        [ObservableProperty]
        public string newStudentLastName;
        [ObservableProperty]
        public string newStudentImagePath;
        [ObservableProperty]
        public DateTime newStudentDoB;
        [ObservableProperty]
        public double newStudentGpaValue;
        [ObservableProperty]
        public bool isAllFieldsFilled;
        [ObservableProperty]
        public Department selectedDepartment;
        public List<Department> Departments
        {
            get
            {
               return new List<Department>()
                {
                    new Department(1,"Electrical Engineering","C:\\Users\\bhanu\\Desktop\\StudentManagementSystem\\StudentManagementSystem\\Images\\electrical.png"),
                    new Department(2,"Computer Engineering","C:\\Users\\bhanu\\Desktop\\StudentManagementSystem\\StudentManagementSystem\\Images\\mechanical.png"),
                    new Department(3,"Mechanical Engineering","C:\\Users\\bhanu\\Desktop\\StudentManagementSystem\\StudentManagementSystem\\Images\\civil.png"),
                    new Department(4,"Civil Engineering","C:\\Users\\bhanu\\Desktop\\StudentManagementSystem\\StudentManagementSystem\\Images\\computer.png"),
                };
            }
        }

        //Constructor
        public AddNewStudentWindowVM()
        {
            IsAllFieldsFilled = true;
        }

        partial void OnNewStudentGpaValueChanged(double value)
        {
            if(value < 0)
            {
                MessageBox.Show("GPA value cannot be negative","Value error",MessageBoxButton.OK, MessageBoxImage.Error);
                NewStudentGpaValue = 0;
            }else if(value > 4.2) 
            {
                MessageBox.Show("GPA value cannot exceed 4.2", "Value error", MessageBoxButton.OK, MessageBoxImage.Error);
                NewStudentGpaValue = 4.2;
            }
        }

        partial void OnNewStudentDoBChanged(DateTime value)
        {
            if(value > DateTime.Now)
            {
                MessageBox.Show("Birthday cannot be future value", "Value error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        public void SelectImage()
        {
            //This OpenFileDialog will be able user to choose student profile picture and get it's path as a string
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select Student's Profile picture";
            openFileDialog.ShowDialog();
            if(openFileDialog.FileName !=null)
            {
                NewStudentImagePath = openFileDialog.FileName.ToString();
                IsAllFieldsFilled=true;
            }
            else
            {
                IsAllFieldsFilled=false;
            }
        }
        [RelayCommand]
        public void SaveNewStudent()
        {   
            // With any empty field user cannot make student objects
            if(string.IsNullOrEmpty(NewStudentFirstName) || string.IsNullOrEmpty(NewStudentLastName) || string.IsNullOrEmpty(NewStudentImagePath) || NewStudentDoB ==DateTime.MinValue || NewStudentGpaValue ==0 || SelectedDepartment == null)
            {
                //Nitify User using Alert message box about Some fields are empty
                MessageBox.Show("Fill all fields!","Warning",MessageBoxButton.OK,MessageBoxImage.Error);
                IsAllFieldsFilled = false;
            }
            else
            {
                Student student = new Student();
                student.FirstName = NewStudentFirstName;
                student.LastName = NewStudentLastName;
                student.ImagePath = NewStudentImagePath;
                student.DoB = NewStudentDoB;
                student.GpaValue = NewStudentGpaValue;
                student.Department = SelectedDepartment;

                //notify MainWindow viewmodel and pass new student into that view model
                WeakReferenceMessenger.Default.Send<NewStudentDetailsMessage>(new NewStudentDetailsMessage(student));
            }
        }
        [RelayCommand]
        public void CloseAddStudentWindow()
        {
            //Notify Main Window to close AddStudent Window
            WeakReferenceMessenger.Default.Send<CloseWindowMessage>(new CloseWindowMessage("new"));
        }
    }
}
