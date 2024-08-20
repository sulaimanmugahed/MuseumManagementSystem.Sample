using MuseumManagementSystem.Application.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumManagementSystem.Application.Contracts.Infrastructure
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(Email email);
    }
}
