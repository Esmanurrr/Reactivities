using MediatR;
using Microsoft.AspNetCore.SignalR;
using Reactivities.Application.Activities;
using Reactivities.Application.Comments;
using System.Runtime.InteropServices;

namespace Reactivities.API.SignalR
{
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendComment(Application.Comments.Create.Command command)
        {
            var comment = await _mediator.Send(command);

            await Clients.Group(command.ActivityId.ToString())//just for the person has activityId
                .SendAsync("ReceiveComment", comment.Value);
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var activityId = httpContext.Request.Query["activityId"];
            await Groups.AddToGroupAsync(Context.ConnectionId, activityId);//add comment for spesific activity
            var result = await _mediator.Send(new Application.Comments.List.Query { ActivityId = Guid.Parse(activityId) });//list comments
            await Clients.Caller.SendAsync("LoadComments", result.Value);

        }
    }
}
