using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Microsoft.AspNetCore.SignalR;
using Gharshare.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Gharshare.com.pk.Hubs
{
    public class ChatHub : Hub
    {
        //public async Task SendMessage(string user, string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", user, message);
        //}
        private IUnitofWork _unitofWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ChatHub(IUnitofWork iunitofWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitofWork = iunitofWork;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
        public async Task SendMessage(string groupName, string user, string msg,
            string? fromUserID, string? toUserID,string sentOn)
        {
            //await Clients.Group(groupName).SendAsync("ReceiveMessage", user, msg, sentOn, fromUserID);try{
            try
            {
                var format = "MM/dd/yyyy, hh:mm:ss tt";
                var SentOn = DateTime.ParseExact(sentOn, format, CultureInfo.InvariantCulture, DateTimeStyles.None);
                Message message = new Message
                {
                    FromUserID = fromUserID,
                    ToUserID = toUserID,
                    Text = msg,
                    SentOn = Convert.ToDateTime(sentOn),
                    Sent = true,
                    Read = false,
                    Type = Message.MessageType.WithUser
                };
                _unitofWork.Message.Add(message);
                _unitofWork.Save();
                await Clients.Group(groupName).SendAsync("ReceiveMessage", user, msg, sentOn, fromUserID, message.ID);
            }
            catch(Exception ex)
            {

            }
            
        }
        public async Task UpdateMessage(bool isRead, string senderId, Int64 messageId)
        {
            var message = _unitofWork.Message.Get(x => x.ID == messageId);
            message.Read = true;
            _unitofWork.Message.update(message);
            _unitofWork.Save();
            await Clients.User(senderId).SendAsync("MessageUpdated", isRead);
        }
    }
}
