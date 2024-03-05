namespace RegistrationLetters.Domain.Models
{
    public class MailModel
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public EmployeeModel Addressee { get; set; }
        public EmployeeModel Sender { get; set; }
    }
}
