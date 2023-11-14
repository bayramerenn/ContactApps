using AutoMapper;
using ContactDirectoryService.Application.Features.Contacts.Commands;
using ContactDirectoryService.Application.Features.Contacts.Queries;
using ContactDirectoryService.Domain.Entities;

namespace ContactDirectoryService.Application.Features.Contacts.Mapping
{
    public class ContactMapping : Profile
    {
        public ContactMapping()
        {
            CreateMap<CreateContactCommand, Contact>();
            CreateMap<UpdateContactCommand, Contact>();
            CreateMap<Contact, GetContactDetailResponse>();
            CreateMap<Contact, GetContactListResponse>();
            CreateMap<ContactInformation, ContactInformationResponse>();
        }
    }
}