using LinkDev.IKEA.DAL.Entites;
using LinkDev.IKEA.DAL.Entites.Identity;
using LinkDev.IKEA.PL.Helpers;
using LinkDev.IKEA.PL.ViewModels.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IMailSettings _mailSettings;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,IMailSettings mailSettings)
        {
			_userManager = userManager;
			_signInManager = signInManager;
			_mailSettings = mailSettings;
		}

        [HttpGet]
		public IActionResult SignUp()
		{

			return View(); 

		}

		[HttpPost]

		public async Task< IActionResult> SignUp(SignUpViewModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var user = await _userManager.FindByNameAsync(model.Username);

			if (user is { })
			{
				ModelState.AddModelError(nameof(SignUpViewModel.Username), "this username is aleardy in user for anothor account ");
				return View(model);
			}
		
				user = new ApplicationUser()
				{
					UserName = model.Username,
					Email = model.Email,
					FName = model.FirstName,
					LName = model.LastName,
					IAgree = model.IsAgree,

				};


				// create and hashpassword
				var result = await _userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					return RedirectToAction(nameof(SignIn));
				}
				else
				{
					foreach(var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
				}
		
			return View(model);
			
		}
		[HttpGet]
		public IActionResult SignIn()
		{
			return View();
		}

		[HttpPost]
		public async Task <IActionResult> SignIn(SignInViewModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest();
			
			var user = await _userManager.FindByEmailAsync(model.Email);
			if(user is { })
			{
				var flag = await _userManager.CheckPasswordAsync(user, model.Password);

				if (flag)
				{
					var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RemmberMe, true);

					if (result.IsNotAllowed)
						ModelState.AddModelError(string.Empty, "user account is not allowed");
					if (result.IsLockedOut)
						ModelState.AddModelError(string.Empty, "user account is locked out");

					if (result.Succeeded)
					{
						return RedirectToAction(nameof(HomeController.Index), "Home");
					}

				}
			}
			ModelState.AddModelError(string.Empty, "invalid login attemped");
			return View(model);

		}


		public async new Task <IActionResult> SignOut()
		{
		await _signInManager.SignOutAsync();

		return	RedirectToAction(nameof(SignIn));
		}

		public IActionResult ForgetPassword()
		{
			return View();
		}

		//[HttpPost]
		//public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel viewModel)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		var user = await _userManager.FindByEmailAsync(viewModel.Email);
		//		if (user is not null)
		//		{
		//			var token = await _userManager.GeneratePasswordResetTokenAsync(user);
		//			var ResetPasswordUrl = Url.Action("ResetPassword", "Account", new { email = viewModel.Email , token=token });

		//			var email = new Email()
		//			{
		//				Subject = "Reset Your Password",
		//				Reciepints = viewModel.Email,
		//				Body = ResetPasswordUrl!
		//			};

		//			_mailSettings.SendEmail(email); 

		//			return RedirectToAction(nameof(ChekYourInbox));

		//			// ... (rest of the code)
		//		}
		//		ModelState.AddModelError(string.Empty, "Invalid Email");
		//	}
		//	return View(viewModel);
		//}


		[HttpPost]
		public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(viewModel.Email);
				if (user is not null)
				{
					var token = await _userManager.GeneratePasswordResetTokenAsync(user);

					// Generate the absolute URL with host and scheme specified
					var resetPasswordUrl = Url.Action("ResetPassword", "Account",
						new { email = viewModel.Email, token = token },
						protocol: Request.Scheme,
						host: Request.Host.ToString());

					var email = new Email()
					{
						Subject = "Reset Your Password",
						Reciepints = viewModel.Email,
						Body = resetPasswordUrl!
					};

					_mailSettings.SendEmail(email);

					return RedirectToAction(nameof(ChekYourInbox));
				}
				ModelState.AddModelError(string.Empty, "Invalid Email");
			}
			return View(viewModel);
		}


		public IActionResult ChekYourInbox()
		{
			return View();
		}


		public IActionResult ResetPassword(string email,string token)
		{
			TempData["email"] = email;
			TempData["token"] = token;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				var email = TempData["email"] as string;

				var token = TempData["token"] as string;
				var user = await _userManager.FindByEmailAsync(email!);
				var result = await _userManager.ResetPasswordAsync(user, token, viewModel.Password);

				if (result.Succeeded)
				{
					return RedirectToAction(nameof(SignIn));
				}

				foreach (var item in result.Errors)
				{
					ModelState.AddModelError(string.Empty, item.Description);
				}

			}
			return View(viewModel);

		}


	}
}
