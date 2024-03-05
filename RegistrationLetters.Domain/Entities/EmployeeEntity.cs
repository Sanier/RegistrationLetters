using System.ComponentModel.DataAnnotations;

namespace RegistrationLetters.Domain.Entities
{
    /// <summary>
    /// The EmployeeEntity class represents an employee in the database.
    /// </summary>
    public class EmployeeEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [StringLength(100)]
        public string JobPosition { get; set; } = string.Empty;
    }
}
