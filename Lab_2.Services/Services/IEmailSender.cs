﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2.Services.Services
{
	public interface IEmailSender
	{
		Task SendEmailAsync(string email, string subject, string message);

	}
}
