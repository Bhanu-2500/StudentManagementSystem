using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Win32;
using StudentManagementSystem.Messages;
using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StudentManagementSystem.ViewModels
{
    public partial class EditUserWindowVM : ObservableObject
    {
        [ObservableProperty]
        public Student selectedStudent;
        [ObservableProperty]
        public bool isAllFieldsFilled;
        [ObservableProperty]
        public string gpaValue;
        [ObservableProperty]
        public int existingDepartment;

        public List<Department> Departments { get; set; }


        public EditUserWindowVM()
        {
            IsAllFieldsFilled = true;
            //Initailize the temporary department
            Departments = new List<Department>()
                {
                    new Department(1,"Electrical Engineering","C:\\Users\\bhanu\\Desktop\\StudentManagementSystem\\StudentManagementSystem\\Images\\electrical.png"),
                    new Department(2,"Computer Engineering","C:\\Users\\bhanu\\Desktop\\StudentManagementSystem\\StudentManagementSystem\\Images\\mechanical.png"),
                    new Department(3,"Mechanical Engineering","C:\\Users\\bhanu\\Desktop\\StudentManagementSystem\\StudentManagementSystem\\Images\\civil.png"),
                    new Department(4,"Civil Engineering","C:\\Users\\bhanu\\Desktop\\StudentManagementSystem\\StudentManagementSystem\\Images\\computer.png"),
                };

            //Subscribe to the EditStudentDetailsMessage message for get selected student
            WeakReferenceMessenger.Default.Register<EditStudentDetailsMessage>(this, OnStudentEdited);

        }
        //check whether gpa value is out of range, when GPA value changed
        partial void OnGpaValueChanged(string value)
        {
            if (!string.IsNullOrWhiteSpace(value) || !string.IsNullOrEmpty(value))
            {
                if (Double.Parse(value) > 4.2 || Double.Parse(value) < 0)
                {
                    MessageBox.Show("GPA value should be in range of 0 - 4.2", "Data Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void OnStudentEdited(object recipient, EditStudentDetailsMessage message)
        {
            SelectedStudent = message.Value;
            GpaValue = SelectedStudent.GpaValue.ToString();
            ExistingDepartment = Departments.IndexOf(Departments.Where(d=>d.Name==SelectedStudent.Department.Name).FirstOrDefault());
        }
        [RelayCommand]
        public void SelectProfileImage()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            if(dialog.FileName != null)
            {
                SelectedStudent.ImagePath = dialog.FileName.ToString();
            }
        }
        [RelayCommand]
        public void UpdateEditedStudentDetails()
        {
            //Check whether fields are empty
            if (
                string.IsNullOrEmpty(SelectedStudent.FirstName) || string.IsNullOrWhiteSpace(SelectedStudent.FirstName) ||
                string.IsNullOrEmpty(SelectedStudent.LastName) || string.IsNullOrWhiteSpace(SelectedStudent.LastName) ||
                string.IsNullOrEmpty(SelectedStudent.ImagePath) || string.IsNullOrWhiteSpace(SelectedStudent.ImagePath) ||
                string.IsNullOrEmpty(GpaValue) || string.IsNullOrWhiteSpace(GpaValue) ||
                (SelectedStudent.DoB== DateTime.MinValue) ||
                (SelectedStudent.Department == null))
            {
                IsAllFieldsFilled = false; 
            }
            else
            {
                IsAllFieldsFilled=true;
                SelectedStudent.GpaValue = Double.Parse(GpaValue);
                WeakReferenceMessenger.Default.Send<UpdateStudentDetailsMessage>(new UpdateStudentDetailsMessage(SelectedStudent));
            }
        }
        [RelayCommand]
        public void Cancel()
        {
            //Check whether fields are empty
            if (
                string.IsNullOrEmpty(SelectedStudent.FirstName) || string.IsNullOrWhiteSpace(SelectedStudent.FirstName) ||
                string.IsNullOrEmpty(SelectedStudent.LastName) || string.IsNullOrWhiteSpace(SelectedStudent.LastName) ||
                string.IsNullOrEmpty(SelectedStudent.ImagePath) || string.IsNullOrWhiteSpace(SelectedStudent.ImagePath) ||
                (SelectedStudent.DoB == DateTime.MinValue) ||
                (SelectedStudent.Department == null))
            {
                IsAllFieldsFilled = false; 
            }
            else
            {
                IsAllFieldsFilled = true;
                SelectedStudent.GpaValue = Double.Parse(GpaValue);
                WeakReferenceMessenger.Default.Send<CloseWindowMessage>(new CloseWindowMessage("edit"));
            }

        }
    }
}
