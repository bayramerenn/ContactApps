using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReportingService.Application.Common.Interfaces;

namespace ReportingService.Application.Features.Reports.Queries
{
    public record GetAllReportQuery : IRequest<IEnumerable<GetAllReportResponse>>;

    public class GetAllReportQueryHandler : IRequestHandler<GetAllReportQuery, IEnumerable<GetAllReportResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllReportQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllReportResponse>> Handle(GetAllReportQuery request, CancellationToken cancellationToken)
        {
            return await _context.Reports
                .ProjectTo<GetAllReportResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}