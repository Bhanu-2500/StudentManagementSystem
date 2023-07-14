using CommunityToolkit.Mvvm.Messaging.Messages;

namespace StudentManagementSystem.Messages
{
    public class CloseWindowMessage : ValueChangedMessage<string>
    {
        public CloseWindowMessage(string value) : base(value)
        {
        }
    }
}
