using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Identity
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage ="Password is Requird")]
		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;


		[Required(ErrorMessage = "Confirm Password is Requird")]
		[Display(Name = "Confirm Password")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Confirm password does not match with Password")]
		public string ConfirmedPassword { get; set; } = null!;
	}
}
