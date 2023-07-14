using System;

namespace StudentManagementSystem.Models
{
    public class Student
    {
        private static int idstatic;
        private int _id;

        //Student Id cannot create or update or delete. It increases when students objects create.
        public int Id { get 
            {
                return _id;
            }
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImagePath { get; set; }
        public DateTime DoB { get; set; }
        public Department Department { get; set; }
        private double _gpaValue;

        //Constructor
        public Student()
        {
            idstatic++;
            _id = idstatic;
        }
        //Overload Constructor
        public Student(int id,string firstName, string lastName, string imagePath, DateTime doB, Department department,double gpa)
        {
            _id = id;
            FirstName = firstName;
            LastName = lastName;
            ImagePath = imagePath;
            DoB = doB;
            Department = department;
            _gpaValue = gpa;
        }

        // Gpa Value should be in range of 0 - 4. If it is not throw an out of range exception. 
        public double GpaValue { 
            get { return _gpaValue; } 
            set {
                if(value<=4.2 && value >= 0)
                {
                    _gpaValue = value;
                }
            }
        }
    }
}
