using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AllReady.Features.Notifications;
using MediatR;

namespace AllReady.Areas.Admin.Features.Notifications
{
    public class IterneraryTeamLeadAssignedHandler : IAsyncNotificationHandler<IteneraryTeamLeadAssigned>
    {
        private readonly IMediator _mediator;

        public IterneraryTeamLeadAssignedHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(IteneraryTeamLeadAssigned notification)
        {
            var plainTextMessage = BuildPlainTextMessage(notification);
            var htmlMessage = BuildHtmlMessage(notification);
            var smsMessage = BuildSmsMessage(notification);

            var command = new NotifyVolunteersCommand
            {
                ViewModel = new NotifyVolunteersViewModel()
                {
                    Subject = @"You have been assigned as the Team Lead on an itinerary",
                    EmailRecipients = new List<string> { notification.AssigneeEmail},
                    SmsRecipients = new List<string> {notification.AssigneePhone},
                    EmailMessage = plainTextMessage,
                    HtmlMessage = htmlMessage,
                    SmsMessage = smsMessage
                }
            };

            await _mediator.SendAsync(command);
        }

        private string BuildPlainTextMessage(IteneraryTeamLeadAssigned notification)
        {
            var sb = new StringBuilder();
            sb.AppendLine($@"You have been assigned as the Team Lead on the following itenerary: ""{notification.ItineraryName ?? "an itenerary"}""");
            sb.AppendLine($"To view the itinerary go to the following Url: {notification.IteneraryUrl}");
            return sb.ToString();
        }

        private string BuildHtmlMessage(IteneraryTeamLeadAssigned notification)
        {
            var sb = new StringBuilder();
            sb.AppendLine($@"You have been assigned as the Team Lead on the following itenerary: ""{notification.ItineraryName ?? "an itenerary"}""");
            sb.AppendLine($"To view the itinerary go to the following Url: {notification.IteneraryUrl}");
            return sb.ToString();
        }

        private string BuildSmsMessage(IteneraryTeamLeadAssigned notification)
        {
            return $@"You have been assigned as the Team Lead on the following itenerary: ""{notification.ItineraryName ?? "an itenerary"}""";
        }
    }
}
