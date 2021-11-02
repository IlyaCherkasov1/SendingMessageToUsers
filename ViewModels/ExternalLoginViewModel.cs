using System.ComponentModel.DataAnnotations;

namespace SendingMessageToUsers.ViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        public string NameIdentifier { get; set; }

        [Required]
        public string GivenName { get; set; }
        [Required]
        public string ReturnUrl { get; set; }
        [Required]

        public string Email { get;  set; }
    } 
}
