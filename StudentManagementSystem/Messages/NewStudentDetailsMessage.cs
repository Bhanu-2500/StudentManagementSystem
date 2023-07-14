using CommunityToolkit.Mvvm.Messaging.Messages;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Messages
{
    class NewStudentDetailsMessage : ValueChangedMessage<Models.Student>
    {
        public NewStudentDetailsMessage(Student value) : base(value)
        {
        }
    }
}
