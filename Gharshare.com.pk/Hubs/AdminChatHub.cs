using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Microsoft.AspNetCore.SignalR;
using Gharshare.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Gharshare.com.pk.Hubs
{
    public class AdminChatHub : Hub
    {        
        private IUnitofWork _unitofWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private static ConcurrentDictionary<string, string> _userConnections =
        new ConcurrentDictionary<string, string>();       
        public AdminChatHub(IUnitofWork iunitofWork, 
            IHttpContextAccessor httpContextAccessor, 
            UserManager<ApplicationUser> userManager)
        {
            _unitofWork = iunitofWork;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async Task<Dictionary<string, string>> GetUserConnections(string userId)
        {
            var connectionId = Context.ConnectionId;
            _userConnections.TryAdd(userId, connectionId);
            return _userConnections.ToDictionary(x => x.Key, x => x.Value);
        }
        public async Task<List<string>> GetUserIds(string userType)
        {
            // Get the list of users from the database
            List<ApplicationUser> users = new List<ApplicationUser>();
            if (userType== "Admin")
            {
                users = _userManager.Users.ToList();
            }
            else if(userType == "Tenant" || userType == "Landlord")
            {
                users = await _userManager.Users.
                    ToListAsync();
            }            
            
            var userList = users.Select(u => u.Id).ToList();
            return userList;
        }
        public async Task<Dictionary<string, string>> GetDefaultAdmin()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Json", optional: false);
            IConfiguration configuration = builder.Build();
            string defaultAdminId = configuration.GetValue<string>("DefaultAdmin:Id");
            string defaultAdmiName = configuration.GetValue<string>("DefaultAdmin:name");
            var defaultAdminData = new Dictionary<string, string>();
            defaultAdminData["Id"] = defaultAdminId;
            defaultAdminData["Name"] = defaultAdmiName;             
            return defaultAdminData.ToDictionary(x => x.Key, x => x.Value);
        }
       
        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);            
        }
        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
        public async Task SendMessage(string groupName, string user,string userType, string msg,
            string? fromUserID, string? toUserID,string sentOn)
        {
            try
            {
                var format = "MM/dd/yyyy, hh:mm:ss tt";
                var SentOn = DateTime.ParseExact(sentOn, format, CultureInfo.InvariantCulture, DateTimeStyles.None);
                Message message = new Message
                {
                    FromUserID = fromUserID,
                    ToUserID = toUserID,
                    Text = msg,
                    SentOn = Convert.ToDateTime(SentOn),
                    Sent = true,
                    Read = false,
                    Type = Message.MessageType.WithAdmin
                };
                _unitofWork.Message.Add(message);
                _unitofWork.Save();
                _unitofWork.Message.StopTracking(message);
                await Clients.Group(groupName).SendAsync("ReceiveMessage", user, userType, msg, sentOn, fromUserID, message.ID);
            }
            catch(Exception exp)
            {

            }            
        }
        public async Task UpdateMessage(bool isRead, string senderId, Int64 messageId)
        {
            var message = _unitofWork.Message.Get(x => x.ID == messageId);
            if (message != null)
            {
                message.Read = true;
                _unitofWork.Message.update(message);
                _unitofWork.Save();
            }
            await Clients.User(senderId).SendAsync("MessageUpdated", isRead);
        }
    }
}
