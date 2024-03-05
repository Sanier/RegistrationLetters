using System.Windows.Controls;
using RegistrationLetters.Client.API;
using RegistrationLetters.Domain.Models;

namespace RegistrationLetters.Client.View
{
    /// <summary>
    /// Логика взаимодействия для IncomingLetters.xaml
    /// </summary>
    public partial class IncomingLetters : Page
    {
        private readonly MailAPI _mailAPI;
        private readonly EmployeeModel _currentEmployee;

        public IncomingLetters()
        {
            InitializeComponent();

            _mailAPI = new MailAPI();

            PassingValues();
            LoadMail();
        }

        public IncomingLetters(EmployeeModel currentEmployee)
        {
            InitializeComponent();

            _mailAPI = new MailAPI();
            _currentEmployee = currentEmployee;

            PassingValues();
            LoadMail();
        }

        public async void LoadMail()
        {
            var mails = await _mailAPI.GetMail(_currentEmployee);

            foreach(var mail in mails)
                listViewMails.Items.Add(mail);
        }

        private void PassingValues()
        {
            txtbckName.DataContext = _currentEmployee;
        }

        private void btnExit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var authorization = new Authorization();
            this.NavigationService.Navigate(authorization);
        }

        private void btnSendMail_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var sendLetter = new SendLetter(_currentEmployee);
            this.NavigationService.Navigate(sendLetter);
        }
    }
}
