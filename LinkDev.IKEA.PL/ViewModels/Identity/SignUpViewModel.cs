using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Identity
{
	public class SignUpViewModel
	{
		[Display(Name = "First Name")]

		public string FirstName { get; set; } = null!;

		[Display(Name = "Last Name")]

		public string LastName { get; set; } = null!;
		public string Username { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;

		[DataType(DataType.Password)]
        public string Password { get; set; } = null!;

		[Display(Name ="Confirm Password")]
		[DataType(DataType.Password)]
        [Compare("Password" ,ErrorMessage ="Confirm password does not match with Password")]
		public string ConfirmedPassword { get; set; } = null!;


		public bool IsAgree { get; set; }


    }
}
