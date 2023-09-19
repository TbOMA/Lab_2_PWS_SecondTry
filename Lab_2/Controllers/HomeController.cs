using Lab_2.Services.Services;
using Microsoft.AspNetCore.Mvc;

using WebApplication1.Models;
using MimeKit;
using MailKit.Net.Smtp;
using System.Diagnostics;
using Lab_2.Models;

namespace WebApplication1.Controllers
{
	public class HomeController : Controller
	{
		private readonly IEmailSender _emailSender;
        public HomeController( IEmailSender emailSender)
        {
            _emailSender = emailSender;
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
                // Send the email using the email service
                await _emailSender.SendEmailAsync("recipient@example.com", "Subject", model.UsersText);

                // Optionally, you can set a success message
                ViewBag.Message = "Email sent successfully!";
            }

            return View(model);
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