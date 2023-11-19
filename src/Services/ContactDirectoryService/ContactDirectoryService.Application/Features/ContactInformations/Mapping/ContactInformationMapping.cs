using AutoMapper;
using ContactDirectoryService.Application.Features.ContactInformations.Command;
using ContactDirectoryService.Application.Features.ContactInformations.Queries;
using ContactDirectoryService.Domain.Entities;

namespace ContactDirectoryService.Application.Features.ContactInformations.Mapping
{
    public class ContactInformationMapping : Profile
    {
        public ContactInformationMapping()
        {
            CreateMap<CreateContactInformationCommand, ContactInformation>();
            CreateMap<ContactInformation, GetContactInformationDetailResponse>();
        }
    }
}