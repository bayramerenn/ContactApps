using ContactDirectoryService.Application.Features.ContactInformations.Queries;
using Event;
using Event.Models;
using Event.Services;
using MassTransit;
using MediatR;

namespace ContactDirectoryService.Application.Consumers
{
    public class CreateContractReportConsumer : IConsumer<CreateContractReportEvent>
    {
        private readonly ISender _sender;
        private readonly IQueueService _queueService;

        public CreateContractReportConsumer(ISender sender, IQueueService queueService)
        {
            _sender = sender;
            _queueService = queueService;
        }

        public async Task Consume(ConsumeContext<CreateContractReportEvent> context)
        {
            var result = await _sender.Send(new GetLocationReportQuery(context.Message.Id));

            if (result != null)
            {
                await _queueService.SendAsync(new SuccessContractReportEvent(context.Message.Id), QueueConstants.SUCCESS_CONRACT_REPORT);
            }
            else
            {
                await _queueService.SendAsync(new FailContractReportEvent(context.Message.Id), QueueConstants.FAIL_CONRACT_REPORT);
            }
        }
    }
}