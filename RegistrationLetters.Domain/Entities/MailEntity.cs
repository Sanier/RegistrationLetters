using System.ComponentModel.DataAnnotations;

namespace RegistrationLetters.Domain.Entities
{
    /// <summary>
    /// The MailEntity class represents an employee in the database.
    /// </summary>
    public class MailEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(300)]
        public string Title { get; set; } = string.Empty;

        [StringLength(5000)]
        public string Content { get; set; } = string.Empty;

        public string Date { get; set; } = string.Empty;

        [StringLength(100)]
        public int AddresseeId { get; set; }

        [StringLength(100)]
        public int SenderId { get; set; }
    }
}
