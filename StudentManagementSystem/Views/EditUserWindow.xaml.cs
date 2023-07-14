using StudentManagementSystem.ViewModels;
using System.Windows;

namespace StudentManagementSystem.Views
{
    /// <summary>
    /// Interaction logic for EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        public EditUserWindow()
        {
            InitializeComponent();
            DataContext = new EditUserWindowVM();
        }
    }
}
