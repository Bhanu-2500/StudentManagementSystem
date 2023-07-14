using CommunityToolkit.Mvvm.Messaging.Messages;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Messages
{
    public class EditStudentDetailsMessage : ValueChangedMessage<Student>
    {
        public EditStudentDetailsMessage(Student value) : base(value)
        {
        }
    }
}
