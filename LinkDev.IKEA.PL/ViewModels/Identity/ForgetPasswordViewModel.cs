using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Identity
{
	public class ForgetPasswordViewModel
	{
		[Required(ErrorMessage ="Email is Requered")]
		[EmailAddress(ErrorMessage ="Invalid Email")]
		public string Email { get; set; } = null!;
	
	}
}
