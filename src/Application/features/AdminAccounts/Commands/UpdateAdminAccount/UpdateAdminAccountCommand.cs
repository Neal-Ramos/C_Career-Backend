using Application.commons.DTOs;
using MediatR;

namespace Application.features.AdminAccounts.Commands.UpdateAdminAccount
{
    public class UpdateAdminAccountCommand: IRequest<AdminAccountDto>
    {
        public Guid AdminId {get; set;}
        public string Email {get; set;} = null!;
        public string UserName {get; set;} = null!;
        public string FirstName {get; set;} = null!;
        public string LastName {get; set;} = null!;
        public string? MiddleName {get; set;}
        public DateTime BirthDate {get; set;}
    }
}