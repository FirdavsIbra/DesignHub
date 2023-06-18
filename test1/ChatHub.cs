using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace API
{
    public class ChatHub: Hub
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChatHub(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override async Task OnConnectedAsync()
        {
            if (_httpContextAccessor.HttpContext?.Request.Path.Value.Contains("/chatHub") == true)
            {
                var segments = _httpContextAccessor.HttpContext.Request.Path.Value.Split('/');
                if (segments.Length >= 4)
                {
                    var controllerName = segments[2];
                    var clientId = segments[3];
                    if (controllerName == "chat" && !string.IsNullOrEmpty(clientId))
                    {
                        await Groups.AddToGroupAsync(Context.ConnectionId, clientId);
                    }
                }
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (_httpContextAccessor.HttpContext?.Request.Path.Value.Contains("/chatHub") == true)
            {
                var segments = _httpContextAccessor.HttpContext.Request.Path.Value.Split('/');
                if (segments.Length >= 4)
                {
                    var controllerName = segments[2];
                    var clientId = segments[3];
                    if (controllerName == "chat" && !string.IsNullOrEmpty(clientId))
                    {
                        await Groups.RemoveFromGroupAsync(Context.ConnectionId, clientId);
                    }
                }
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string clientId, string message)
        {
            await Clients.Group(clientId).SendAsync("ReceiveMessage", clientId, message);
        }
    }
}
