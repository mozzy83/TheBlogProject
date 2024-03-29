﻿using Microsoft.AspNetCore.Identity.UI.Services;

namespace TheBlogProject.Services
{
    public interface IBlogEmailSender : IEmailSender
    {
        Task SendContactEmailAsync(string email, string name, string subject, string htmlMessage);
    }
}
