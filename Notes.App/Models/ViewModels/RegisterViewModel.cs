namespace Notes.App.Models.ViewModels
{
    using SimpleMvc.Framework.Attributes.Property;
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel
    {
        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Username should be between 2 and 30 character long")]
        public string Username { get; set; }

        [Regex(@"(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}", ErrorMessage = "Password is not valid")]
        public string Password { get; set; }
    }
}
