using System.Windows;
using System.Windows.Controls;
using RegistrationLetters.Client.API;
using RegistrationLetters.Domain.Models;

namespace RegistrationLetters.Client.View
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        private readonly EmployeeAPI _employeeAPI;

        public Registration()
        {
            InitializeComponent();

            _employeeAPI = new EmployeeAPI();
        }

        private async void btnRegistration_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbxFirstName.Text) ||
                string.IsNullOrWhiteSpace(tbxLastName.Text) ||
                string.IsNullOrWhiteSpace(tbxJobPosition.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            var newEmployee = new EmployeeModel()
            {
                FirstName = tbxFirstName.Text,
                LastName = tbxLastName.Text,
                JobPosition = tbxJobPosition.Text
            };

            var flag = await _employeeAPI.CreateNewEmployee(newEmployee);

            if (flag)
            {
                MessageBox.Show($"Пользователь успешно создан!");
                var incomingLetters = new IncomingLetters(newEmployee);
                this.NavigationService.Navigate(incomingLetters);
            }
            else
            {
                MessageBox.Show("Произошла ошибка при создании нового пользователя!");
            }
        }

        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var auth = new Authorization();
            this.NavigationService.Navigate(auth);
        }
    }
}
