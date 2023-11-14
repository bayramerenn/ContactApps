using AutoMapper;
using ContactDirectoryService.Application.Features.Contract.Commands.CreateContact;
using ContactDirectoryService.Application.Features.Contract.Commands.UpdateContact;
using ContactDirectoryService.Domain.Entities;

namespace ContactDirectoryService.Application.Features.Contract.Mapping
{
    public class ContactMapping : Profile
    {
        public ContactMapping()
        {
            CreateMap<CreateContactCommand, Contact>();
            CreateMap<UpdateContactCommand, Contact>();
        }
    }
}