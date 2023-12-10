using Lab_2.Services.Services;
using Microsoft.AspNetCore.Mvc;

using WebApplication1.Models;
using MimeKit;
using MailKit.Net.Smtp;
using System.Diagnostics;
using Lab_2.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;

namespace Lab_2.Controllers
{
	public class HomeController : Controller
	{
        private readonly IStringLocalizer<HomeController> _localizer;

        private readonly IEmailSender _emailSender;
        private readonly ILogger<HomeController> _logger;
        public HomeController(IEmailSender emailSender, ILogger<HomeController> logger, IStringLocalizer<HomeController> localizer)
        {
            _emailSender = emailSender;
            _logger = logger;
            _logger.LogInformation("Logging get started!");
            _localizer = localizer;
        }

        public async Task<IActionResult> Index()
		{
            
            return View();
		}
        [HttpPost]
        public async Task<IActionResult> Index(ContactFormModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Send the email using the email service
                    await _emailSender.SendEmailAsync("recipient@example.com", "Subject", model.UsersText);

                    // Optionally, you can set a success message
                    ViewBag.Message = "Email sent successfully!";

                    // Log the success
                    _logger.LogInformation("Email sent successfully.");
                }
                catch (Exception ex)
                {
                    // Log any errors that occur during email sending
                    _logger.LogError(ex, "Error sending email.");
                }
            }
            _logger.LogError("Model is invalid!");
            return View(model);
        }

        [HttpPost]
        public IActionResult ChangeLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}