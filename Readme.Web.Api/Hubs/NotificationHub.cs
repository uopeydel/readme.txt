using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Readme.Web.Api.Hubs.Enum;
//using Readme.Logic.UnitOfWork.Interface;

namespace Readme.Web.Api.Hubs
{
    public class NotificationHub : Hub
    {
        //private readonly ILogicUnitOfWork LogicUnitOfWork;
        private static List<UserMap> UserMapDataList = new List<UserMap>();
        public NotificationHub(/*ILogicUnitOfWork logicUnitOfWork*/)
        {
            //LogicUnitOfWork = logicUnitOfWork;
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var UserDisconnectIndex = UserMapDataList.FindIndex(a => a.ConnectionId == Context.ConnectionId);
            if (UserDisconnectIndex != -1)
            {
                UserMapDataList.RemoveAt(UserDisconnectIndex);
            }
            await base.OnDisconnectedAsync(exception);
        }

        public async Task RegisterUserMap(int userId, ClientTypeEnum clientType)
        {
            if (!UserMapDataList.Any(a => a.UserId == userId))
            {
                Notification NotiObj = new Notification();
                //TODO
                //var NotiObj = await LogicUnitOfWork.RequestOrderService.GetNotiObjRQ(userId,clientType);
                var User = new UserMap(Context.ConnectionId, userId, clientType);
                UserMapDataList.Add(User);
                await Clients.Client(Context.ConnectionId).InvokeAsync("RegisterUserMap", NotiObj);
            }
        }

        public async Task NotificationNewJob(List<Notification> NotiObjList)
        {
            if (NotiObjList == null || NotiObjList.Count == 0)
            {
                return;
            }
            var UserDataInNotiList = UserMapDataList
                .Where(
                w =>
                NotiObjList.Any(
                    a =>
                    a.ClientType == w.ClientType
                    &&
                    a.UserId == w.UserId
                    ))
                .ToList();

            foreach (var NotiObj in NotiObjList)
            {
                var UserData = UserDataInNotiList.Where(
                    w =>
                    w.UserId == NotiObj.UserId
                    &&
                    w.ClientType == NotiObj.ClientType
                    ).FirstOrDefault();

                if (UserData != null)
                {
                    await Clients.Client(UserData.ConnectionId).InvokeAsync("NotificationNewJob", NotiObj);
                }
            }
        }

    }
}