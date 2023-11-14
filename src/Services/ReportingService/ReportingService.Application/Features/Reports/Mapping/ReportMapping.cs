using AutoMapper;
using ReportingService.Application.Features.Reports.Queries;
using ReportingService.Domain.Entities;

namespace ReportingService.Application.Features.Reports.Mapping
{
    public class ReportMapping : Profile
    {
        public ReportMapping()
        {
            CreateMap<Report, GetAllReportResponse>();
        }
    }
}