using System.Windows;
using System.Windows.Controls;
using RegistrationLetters.Client.API;
using RegistrationLetters.Domain.Models;

namespace RegistrationLetters.Client.View
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        private readonly EmployeeAPI _employeeAPI;

        public Authorization()
        {
            InitializeComponent();

            _employeeAPI = new EmployeeAPI();
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            var registration = new Registration();
            this.NavigationService.Navigate(registration);
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbFirstName.Text) && string.IsNullOrEmpty(txbLastName.Text))
                MessageBox.Show("Введите имя и фамилию");

            var currentEmployee = await _employeeAPI.GetEmployee(GetCurrentEmployee(txbFirstName.Text, txbLastName.Text));

            if (currentEmployee != null)
            {
                MessageBox.Show($"Пользователь успешно авторизован");

                var incomingLetters = new IncomingLetters(currentEmployee);
                this.NavigationService.Navigate(incomingLetters);
            }
            else
            {
                MessageBox.Show("Данного сотрудника нет в списке. Проверьте правильность имени и фамилии или пройдите регистрацию!");
            }
        }

        private EmployeeModel GetCurrentEmployee(string firstName, string lastName)
        {
            var employeeModel = new EmployeeModel()
            {
                FirstName = firstName,
                LastName = lastName
            };
            return employeeModel;
        }
    }
}
