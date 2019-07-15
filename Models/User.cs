using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LoginandRegistration.Models
    {
        public class User
        {
        [Key]
        public int UserId { get; set; }
        [Required (ErrorMessage ="First Name can not be blank.")]
        [MinLength(2, ErrorMessage ="First Name must 2 characters or more.")]
        public string FirstName { get; set; }
        [Required (ErrorMessage ="Last Name can not be blank.")]
        [MinLength(2, ErrorMessage = "Last Name must 2 characters or more.")]
        public string LastName { get; set; }
        [EmailAddress]
        [Required (ErrorMessage ="Email Address can not be blank.")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [MinLength(8, ErrorMessage = "Password must be 8 characters or longer!")]
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        // Will not be mapped to your users table!
        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm { get; set; }
        }    
    }