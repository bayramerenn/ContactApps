using AutoMapper;
using ContactDirectoryService.Application.Features.Contract.Commands.CreateContact;
using ContactDirectoryService.Domain.Entities;

namespace ContactDirectoryService.Application.Features.Contract.Mapping
{
    public class ContactMapping : Profile
    {
        public ContactMapping()
        {
            CreateMap<CreateContactCommand, Contact>();
        }
    }
}