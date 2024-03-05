using System.Windows;
using System.Windows.Controls;
using RegistrationLetters.Client.API;
using RegistrationLetters.Domain.Models;

namespace RegistrationLetters.Client.View
{
    /// <summary>
    /// Логика взаимодействия для SendLetter.xaml
    /// </summary>
    public partial class SendLetter : Page
    {
        private readonly MailAPI _mailAPI;
        private readonly EmployeeAPI _employeeAPI;
        private readonly EmployeeModel _currentEmployee;

        public SendLetter()
        {
            InitializeComponent();

            _mailAPI = new MailAPI();
            _employeeAPI = new EmployeeAPI();

            PassingValues();
        }

        public SendLetter(EmployeeModel currentEmployee)
        {
            InitializeComponent();

            _mailAPI = new MailAPI();
            _employeeAPI = new EmployeeAPI();

            _currentEmployee = currentEmployee;

            PassingValues();
        }

        private async void PassingValues()
        {
            txtbckName.DataContext = _currentEmployee;

            var employees = await _employeeAPI.GetAllEmployees();

            foreach (var employeeModel in employees)
                cbxAdressee.Items.Add(employeeModel);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            var authorization = new Authorization();
            this.NavigationService.Navigate(authorization);
        }

        private void btnViewMail_Click(object sender, RoutedEventArgs e)
        {
            var incomingLetter = new IncomingLetters(_currentEmployee);
            this.NavigationService.Navigate(incomingLetter);
        }

        private async void btnSend_Click(object sender, RoutedEventArgs e)
        {
            var mailModel = new MailModel()
            {
                Title = tbxTitle.Text,
                Content = tbxContent.Text,
                Date = DateTime.Today,
                Addressee = (EmployeeModel)cbxAdressee.SelectedItem,
                Sender = _currentEmployee
            };

            var requestStatus = await _mailAPI.CreateNewMail(mailModel);

            if (requestStatus)
                SuccessfulSubmissionSendMail();
            else
                MessageBox.Show("Произошла ошибка при отправке письма!");

        }

        private void SuccessfulSubmissionSendMail()
        {
            MessageBox.Show("Письмо было успешно отправлено!");
            tbxContent.Clear();
            tbxTitle.Clear();
            cbxAdressee.SelectedIndex = -1;
        }
    }
}
