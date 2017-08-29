using Microsoft.AspNet.SignalR;

namespace WebApplication3.Hubs
{
    public class NotificationHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}