using CommunityToolkit.Mvvm.Messaging.Messages;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Messages
{
    public class UpdateStudentDetailsMessage : ValueChangedMessage<Student>
    {
        public UpdateStudentDetailsMessage(Student value) : base(value)
        {
        }
    }
}
