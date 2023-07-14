
namespace StudentManagementSystem.Models
{
    public class Department
    {
        int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public Department(int id, string name,string img)
        {
            Id = id;
            Name = name;
            ImagePath = img;
        }
    }
}
