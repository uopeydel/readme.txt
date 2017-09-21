using Readme.Common.Enum;
using Readme.DataAccess.MongoDB.Models;
using Readme.Logic.DomainModel;
using Readme.Logic.DomainModel.LineModels;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Readme.Logic.V1.Interface
{

    public interface ILogUserService
    {
        //Task<List<LogUsersDto>> GetUserByProject(string ProjectHook, string ProjectCode, int ProjectId);
        //Task<LogUsersDto> GetUserUID(string UID);
        //Task<LogUsersDto> CreateUser(LogUsersDto User);

        //Task<ObjectId> SaveNewUser(EventDto SourceEventModel, string ChannelAccessToken, string Hook, LineUserInfoDto UserProfile);
        //Task<ObjectId> TakeIdByUid(string UID, string Hook);
        //Task<bool> CheckActiveStatus(string UID, ActiveStatusLineLogEnum ActiveStatus);
        //Task UpdateActiveStatus(LogUsersDto Cus);
        //Task<bool> CheckHaveRealTenantPhoneNumber(string PhoneNumber, string ChannelAccessToken);
    }
}
