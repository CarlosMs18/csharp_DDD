

using CleanArchitecture.Application.Models;

namespace CleanArchitecture.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendMail(Email email);
    }
}
