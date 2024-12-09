using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Identity
{
    public class SignInViewModel
    {
		[Required(ErrorMessage = "Email is Requered")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; } = null!;
		[Required(ErrorMessage = "Password is Requerd")]
		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;

		public bool RemmberMe { get; set; }
	}
}
