using System;
using System.ComponentModel.DataAnnotations;

namespace Patcha.Security
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
