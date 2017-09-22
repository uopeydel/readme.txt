using Readme.DataAccess.EntityFramework.UnitOfWork.Interface;
using Readme.DataAccess.MongoDB.Models;
using Readme.DataAccess.MongoDB.UnitOfWork.Interface;
using Readme.Logic.DomainModel;
using Readme.Logic.V1.Interface;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Linq;
using Readme.Common.Enum;
using MongoDB.Bson;
using Newtonsoft.Json;
using Readme.Logic.DomainModel.LineModels;
using Microsoft.EntityFrameworkCore;

namespace Readme.Logic.V1.Implement
{
    public class LogMessagesService : ILogMessagesService
    {
        private IMongoDBUnitOfWork MongoDBUnitOfWork;
        private IEntityUnitOfWork EntityUnitOfWork;

        public LogMessagesService(IEntityUnitOfWork EntityUnitOfWork, IMongoDBUnitOfWork MongoDBUnitOfWork)
        {
            this.MongoDBUnitOfWork = MongoDBUnitOfWork;
            this.EntityUnitOfWork = EntityUnitOfWork;
        }


        #region MyRegion


        //public async Task<List<LogMessagesDto>> GetMessagesByUserId(int userId)
        //{
        //    var LogMessagesData = await MongoDBUnitOfWork.MongoDBRepository.GetCollectionLogMessages()
        //        .Find(x => x.user_Readme_id == userId)
        //        .ToListAsync();

        //    return LogMessagesData.Select(s => new LogMessagesDto()
        //    {
        //        message_json = s.message_json,
        //        message_type = (MessageTypeLineLogEnum)s.message_type,
        //        sent_timestamp = s.sent_timestamp.ToLocalTime(),
        //        user_id_destination = s.user_id_destination,
        //        user_id_source = s.user_id_source,
        //        user_Readme_id = s.user_Readme_id,
        //        _id = s._id
        //    }).ToList();
        //    //return await MongoDBUnitOfWork.MongoDBRepository.GetCollectionLogMessages().Find(x => x.user_Readme_id == userId).ToListAsync<LogUsers>();
        //}

        //public async Task<LogMessagesDto> CreateMessages(LogMessagesDto Messages)
        //{
        //    LogMessages LogMessagesData = new LogMessages()
        //    {
        //        _id = ObjectId.GenerateNewId(),
        //        user_Readme_id = Messages.user_Readme_id,
        //        user_id_source = Messages.user_id_source,
        //        user_id_destination = Messages.user_id_destination,
        //        sent_timestamp = Messages.sent_timestamp,
        //        message_json = Messages.message_json,
        //        message_type = (int)Messages.message_type
        //    };

        //    await MongoDBUnitOfWork.MongoDBRepository.GetCollectionLogMessages().InsertOneAsync(LogMessagesData);

        //    return Messages;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="channelAccessToken"></param>
        ///// <param name="sourceUID">its project/uid hook or "" or null</param>
        ///// <param name="destinationUID"></param>
        ///// <param name="Serialize">Serialize string from Json</param>
        ///// <param name="typeUserSource">example "developer"</param>
        //public EventDto GenSourceEventModel(
        //    string ChannelAccessToken,
        //    string SourceUID,
        //    string DestinationUID,
        //    string Serialize,
        //    string TypeUserSource,
        //    int? FromUserMemberId
        //    )
        //{
        //    var MessageModel = JsonConvert.DeserializeObject<MessageDto[]>(Serialize);
        //    //lineSrv save log
        //    var SourceEventModel = new EventDto()
        //    {
        //        fromUserId = FromUserMemberId,
        //        type = "message",
        //        source = new SourceDto() { userId = SourceUID, type = TypeUserSource },
        //        timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
        //        message = new MessageDto() { type = MessageModel[0].type, text = Serialize }
        //    };

        //    return SourceEventModel;
        //}


        //public async Task<List<LogMessagesDto>> TakeLog(FilterLogMessageDto FilterLogMessage)
        //{
        //    var LogMessageData = new List<LogMessagesDto>();
        //    var ProjectCodeList = EntityUnitOfWork.ProjectRepository.GetAll(x => !string.IsNullOrEmpty(x.Code)).Select(x => x.Code).ToArray();
        //    var LineLogs = await MongoDBUnitOfWork.MongoDBRepository.GetCollectionLogMessages().Find(x =>
        //         FilterLogMessage.LastTimestamp < x.sent_timestamp &&
        //         (FilterLogMessage.SentTimeStampMax == null || (x.sent_timestamp > FilterLogMessage.SentTimeStampMax)) &&//กรองตามเวลา
        //         (FilterLogMessage.SentTimeStampMin == null || (x.sent_timestamp < FilterLogMessage.SentTimeStampMin)) &&//กรองตามเวลา
        //         (
        //         (FilterLogMessage.UserIdCustomer != ObjectId.Empty && (x.user_id_destination == FilterLogMessage.UserIdCustomer || x.user_id_source == FilterLogMessage.UserIdCustomer))
        //         //(filter.user_Id_destination > 0 && (x.user_Id_destination == filter.user_Id_destination)) ||//ไอดีลูกค้า
        //         //(filter.user_Id_source > 0 && (x.user_Id_source == filter.user_Id_source))//ไอดีเรา
        //         ) // && (/*string.IsNullOrEmpty(filter.type) || */(x.user_Id_destination > filter.user_Id_destination))//กรองอะไรนะ ..
        //     ).ToListAsync();  //OrderByDescending(x => x.message_Id).Take(filter.maxMessage).OrderBy(x => x.message_Id).ToList();

        //    LineLogs.OrderByDescending(x => x.sent_timestamp).Take(FilterLogMessage.MaxMessage).OrderBy(x => x.sent_timestamp).ToList();

        //    var UserIdSourcelist = LineLogs.Select(x => x.user_id_source).ToArray();
        //    var UserSenderList = await MongoDBUnitOfWork.MongoDBRepository.GetCollectionLogUsers().Find(
        //        x => UserIdSourcelist.Contains(x._id)).ToListAsync();

        //    if (LineLogs.Count > 0)
        //    {
        //        foreach (var LineLog in LineLogs)
        //        {
        //            var TempLog = new LogMessagesDto();
        //            TempLog._id = LineLog._id;
        //            TempLog.sent_timestamp = LineLog.sent_timestamp;
        //            TempLog.message_json = LineLog.message_json; //JsonConvert.DeserializeObject<MessageDto[]>(linelog.message_json);
        //            TempLog.message_type = (MessageTypeLineLogEnum)LineLog.message_type;
        //            TempLog.user_id_destination = LineLog.user_id_destination;
        //            TempLog.user_id_source = LineLog.user_id_source;


        //            var IsProjectCodeContainSender = ProjectCodeList.Any(a => UserSenderList.Any(x => x.uid == a));
        //            var IsHookContainUid = UserSenderList.Any(a => UserSenderList.Any(b => a.param_hook == b.uid));
        //            if (LineLog.user_Readme_id > 0)
        //            {
        //                TempLog.senderType =
        //                    IsProjectCodeContainSender
        //                    ||
        //                    IsHookContainUid
        //                    ?
        //                    SenderType.System : SenderType.Customer;
        //            }
        //            else
        //            {
        //                TempLog.senderType = SenderType.User;
        //            }


        //            LogMessageData.Add(TempLog);
        //        }
        //        //update status to OnChat
        //        var FilterMongo = new BsonDocument("_id", FilterLogMessage.UserIdCustomer);
        //        var UpdateMongo = Builders<LogUsers>.Update.Set("activeStatus", (int)ActiveStatusLineLogEnum.OnChat);
        //        var Result = await MongoDBUnitOfWork.MongoDBRepository.GetCollectionLogUsers().FindOneAndUpdateAsync(FilterMongo, UpdateMongo);
        //        //if (Result != null)
        //        //{
        //        //}
        //    }
        //    return LogMessageData;
        //}


        ///// <summary>
        ///// Log from user type to line developer
        ///// </summary>
        ///// <param name="SourceEventModel"></param>
        ///// <param name="ChannelAccessToken"></param>
        ///// <param name="Destination"> Webhook url[3]</param>
        //public async Task PrepairAndCreateLogLineMessage(EventDto SourceEventModel, string ChannelAccessToken, string Destination)
        //{
        //    //var UserProfile = await GetUserInfo(SourceEventModel.source.userId, ChannelAccessToken);

        //    var ProjectHooklineTokens = await GetLineAccount(0, "", ChannelAccessToken);

        //    var Hook = ProjectHooklineTokens != null ? ProjectHooklineTokens.ParamsHook : "";

        //    var UserIdSource = await TakeIdByUid(SourceEventModel.source.userId, Hook);
        //    var UserIdDestination = await TakeIdByUid(Destination, Hook);

        //    if (UserIdSource == ObjectId.Empty)
        //    {
        //        UserIdSource = await SaveNewUser(SourceEventModel, ChannelAccessToken, Hook);

        //        if (UserIdDestination == ObjectId.Empty)
        //        {
        //            var DesEventModel = new EventDto();
        //            DesEventModel.source = new SourceDto();
        //            DesEventModel.source.userId = Destination;
        //            DesEventModel.source.type =
        //                SourceEventModel.source.type == "developer" ?
        //                "user" : "developer";
        //            DesEventModel.timestamp = SourceEventModel.timestamp;

        //            UserIdDestination = await SaveNewUser(DesEventModel, ChannelAccessToken, Hook);
        //        }
        //    }

        //    if (SourceEventModel.type == "message") // event message
        //    {
        //        if (SourceEventModel.message.type == "text" ||
        //            SourceEventModel.message.type == "location" ||
        //            SourceEventModel.message.type == "template")
        //        {
        //            var LogMessageData = new LogMessagesDto()
        //            {
        //                user_Readme_id = SourceEventModel.fromUserId ?? 0,
        //                user_id_source = UserIdSource,
        //                user_id_destination = UserIdDestination,
        //                sent_timestamp = DateTime.Now,//new DateTime(1970, 1, 1).AddMilliseconds(SourceEventModel.timestamp + (3600000 * 7)),//7 == offset GMT
        //                message_json = SourceEventModel.message.text,
        //                message_type = (MessageTypeLineLogEnum)Enum.Parse(typeof(MessageTypeLineLogEnum), SourceEventModel.message.type), // MessageObjectType.Text

        //            };

        //            try
        //            {
        //                await CreateMessages(LogMessageData);
        //            }
        //            catch (Exception e)
        //            {
        //                //TODO: ไม่ควร throw ตรงนี้ ควรหาอะไรมาไว้เก็บ log error /ส่งเมล มาเก็บไว้ว่า error แต่ต้องมีตรวจสอบว่าติด catch Exception หรือไม่
        //                var a = e;
        //            }
        //        }
        //    }
        //    else if (SourceEventModel.type == "postback") // event postback
        //    {

        //    }

        //}

        //private async Task<LineUserInfoDto> GetUserInfo(string UID, string ChannelAccessToken)
        //{
        //    try
        //    {
        //        System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
        //        client.DefaultRequestHeaders.Clear();
        //        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ChannelAccessToken);
        //        var data = await client.GetByteArrayAsync("https://api.line.me/v2/bot/profile/" + UID);
        //        return JsonConvert.DeserializeObject<LineUserInfoDto>(Encoding.UTF8.GetString(data));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //private async Task<LineAccountModelDto> GetLineAccount(int ProjectId, string ProjectHook, string ChannelAccessToken)
        //{
        //    var ResultData = await EntityUnitOfWork.LineAccountRepository.GetAll(
        //        g =>
        //        (
        //        ProjectId > 0
        //        &&
        //        g.ProjectId == ProjectId
        //        )
        //        ||
        //        (
        //        !string.IsNullOrEmpty(ProjectHook)
        //        &&
        //        g.ParamsHook == ProjectHook
        //        )
        //        ||
        //        (
        //        !string.IsNullOrEmpty(ChannelAccessToken)
        //        &&
        //        g.ChannelAccessToken == ChannelAccessToken
        //        )
        //        )
        //        .Select(
        //        s => new LineAccountModelDto
        //        {
        //            AddFriendId = s.AddFriendId,
        //            AddFriendUrl = s.AddFriendUrl,
        //            AddFriendUrlQR = s.AddFriendUrlQr,
        //            LineAccountId = s.LineAccountId,
        //            LineAccountName = s.LineAccountName,
        //            ParamsHook = s.ParamsHook,
        //            ParamsSegment = s.ParamsSegment,
        //            ProjectId = s.ProjectId

        //        }).FirstOrDefaultAsync();

        //    return ResultData;
        //}
        //private async Task<ObjectId> TakeIdByUid(string UID, string Hook)
        //{
        //    var FirstData = await MongoDBUnitOfWork.MongoDBRepository.GetCollectionLogUsers().Find(x => x.uid == UID && x.param_hook == Hook).FirstOrDefaultAsync();

        //    if (FirstData != null)
        //    {
        //        return FirstData._id;
        //    }
        //    else
        //    {
        //        return ObjectId.Empty;
        //    }
        //}
        //private async Task<ObjectId> SaveNewUser(EventDto SourceEventModel, string ChannelAccessToken, string Hook)
        //{
        //    var UserProfile = new LineUserInfoDto();
        //    var UserDataTemp = new LogUsersDto();

        //    //save new profile
        //    if (SourceEventModel.source.type == "developer")
        //    {
        //        var ProjectDataTemp = await EntityUnitOfWork.ProjectRepository.GetAll(x =>
        //        x.LineAccount.Any(y =>
        //            //ตามปกติ ถ้าถูกส่งมาจาก hook ระบบ messemger chat ค่าของ userId จะ เป็น hook
        //            y.ParamsHook == SourceEventModel.source.userId ||
        //           //กรณีเข้าเงื่อนไขล่าง คือ userId ที่ถูกงส่งมาไม่ใช่ hook จะต้องเทียบสองอย่าง คือต้องเป็น Code ด้วย //กรณีนี้จะเกิดขึ้นเมื่อเป็นการส่งข้อความอัตโนมัติ จากระบบ เช่นงานเสร็จเเล้ว
        //           (y.ChannelAccessToken == ChannelAccessToken && x.Code == SourceEventModel.source.userId)
        //           )
        //        ).FirstOrDefaultAsync();

        //        if (ProjectDataTemp != null)
        //        {
        //            UserProfile.displayName = ProjectDataTemp.Name;
        //            UserProfile.pictureUrl = ProjectDataTemp.LogoFilename;
        //            UserProfile.statusMessage = ProjectDataTemp.Address + " " + ProjectDataTemp.ZoneName + " " + ProjectDataTemp.TotalArea;
        //        }
        //        else
        //        {
        //            UserProfile.displayName = SourceEventModel.source.userId;
        //        }
        //        UserProfile.userId = SourceEventModel.source.userId;
        //    }
        //    else
        //    {
        //        UserProfile = await GetUserInfo(SourceEventModel.source.userId, ChannelAccessToken);
        //    }

        //    UserDataTemp.param_hook = Hook;
        //    UserDataTemp.displayName = UserProfile.displayName;
        //    UserDataTemp.pictureUrl = UserProfile.pictureUrl;
        //    UserDataTemp.uid = UserProfile.userId;
        //    UserDataTemp.statusMessage = UserProfile.statusMessage;
        //    UserDataTemp.followTimeStamp = DateTime.Now;//new DateTime(1970, 1, 1).AddMilliseconds(sourceEventModel.timestamp + (3600000 * 7));//7 == offset GMT
        //    UserDataTemp.activeStatus = ActiveStatusLineLogEnum.Follow;
        //    try
        //    {
        //        await CreateUser(UserDataTemp);
        //    }
        //    catch
        //    {
        //        return ObjectId.Empty;
        //    }
        //    return await TakeIdByUid(SourceEventModel.source.userId, UserDataTemp.param_hook);
        //}
        //private async Task<LogUsersDto> CreateUser(LogUsersDto User)
        //{
        //    var LogUserData = new LogUsers()
        //    {
        //        _id = ObjectId.GenerateNewId(),
        //        activeStatus = (int)User.activeStatus,
        //        displayName = User.displayName,
        //        followTimeStamp = User.followTimeStamp,
        //        param_hook = User.param_hook,
        //        pictureUrl = User.pictureUrl,
        //        statusMessage = User.statusMessage,
        //        uid = User.uid
        //    };
        //    await MongoDBUnitOfWork.MongoDBRepository.GetCollectionLogUsers().InsertOneAsync(LogUserData);
        //    return User;
        //}
        #endregion

    }
}
