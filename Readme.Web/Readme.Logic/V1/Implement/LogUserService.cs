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
using Microsoft.EntityFrameworkCore;
using Readme.Logic.DomainModel.LineModels;
using System.ComponentModel.DataAnnotations;

namespace Readme.Logic.V1.Implement
{
    public class LogUserService : ILogUserService
    {
        private IMongoDBUnitOfWork MongoDBUnitOfWork;
        private IEntityUnitOfWork EntityUnitOfWork;

        public LogUserService(IEntityUnitOfWork EntityUnitOfWork, IMongoDBUnitOfWork MongoDBUnitOfWork)
        {
            this.MongoDBUnitOfWork = MongoDBUnitOfWork;
            this.EntityUnitOfWork = EntityUnitOfWork;
        }

        public async Task CreateUser(LogUsersMongoDto LogUserData)
        {
            var context = new ValidationContext(LogUserData, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(LogUserData, context, validationResults, true);

            var UserData = new LogUsers()
            {
                CreateTimeStamp = DateTime.Now,
                DisplayName = LogUserData.DisplayName,
                Email = LogUserData.Email,
                PictureUrl = LogUserData.PictureUrl,
                StatusMessage = LogUserData.StatusMessage,
                _id = ObjectId.GenerateNewId()
            };
            await MongoDBUnitOfWork.MongoDBRepository.GetCollectionLogUsers().InsertOneAsync(UserData);
        }

        public async Task<LogUsersMongoDto> GetUser(ObjectId _id, string Email, string UID)
        {
            var UserData = await MongoDBUnitOfWork.MongoDBRepository.GetCollectionLogUsers()
                .Find(x => x._id == _id || x.Email.Equals(Email) || x.UID.Equals(UID)).FirstOrDefaultAsync();
            LogUsersMongoDto LogUsersData = new LogUsersMongoDto()
            {
                Email = UserData.Email,
                _id = UserData._id,
                CreateTimeStamp = UserData.CreateTimeStamp,
                DisplayName = UserData.StatusMessage,
                PictureUrl = UserData.PictureUrl,
                StatusMessage = UserData.StatusMessage,
                UID = UserData.UID,
            };
            return LogUsersData;
        }

        #region MyRegion


        //public async Task<List<LogUsersDto>> GetUserByProject(string ProjectHook, string ProjectCode, int ProjectId)
        //{
        //    string[] ListHook = await EntityUnitOfWork.LineAccountRepository.GetAll(
        //            x =>
        //            (
        //            ProjectId > 0
        //            &&
        //            x.ProjectId == ProjectId
        //            )
        //            ||
        //            (
        //            !string.IsNullOrEmpty(ProjectCode)
        //            &&
        //            x.Project.Code == ProjectCode
        //            )
        //            ).Select(
        //            x => x.ParamsHook
        //            ).ToArrayAsync();

        //    var UserDataList = await MongoDBUnitOfWork.MongoDBRepository.GetCollectionLogUsers().Find(
        //        x =>
        //        (
        //        !string.IsNullOrEmpty(ProjectHook)
        //        &&
        //        x.param_hook == ProjectHook
        //        )
        //        ||
        //        ListHook.Contains(x.param_hook)
        //        ).ToListAsync();

        //    return UserDataList.Select(s => new LogUsersDto()
        //    {
        //        _id = s._id,
        //        uid = s.uid,
        //        displayName = s.displayName,
        //        param_hook = s.param_hook,
        //        pictureUrl = s.pictureUrl,
        //        activeStatus = (ActiveStatusLineLogEnum)s.activeStatus,
        //        followTimeStamp = s.followTimeStamp.ToLocalTime(),
        //        statusMessage = s.statusMessage
        //    }).ToList();
        //}

        //public async Task<LogUsersDto> GetUserUID(string UID)
        //{
        //    var UserData = await MongoDBUnitOfWork.MongoDBRepository.GetCollectionLogUsers().Find(x => x.uid == UID).FirstOrDefaultAsync();
        //    LogUsersDto LogUsersData = new LogUsersDto()
        //    {
        //        param_hook = UserData.param_hook,
        //        activeStatus = (ActiveStatusLineLogEnum)UserData.activeStatus,
        //        displayName = UserData.displayName,
        //        followTimeStamp = UserData.followTimeStamp.ToLocalTime(),
        //        pictureUrl = UserData.pictureUrl,
        //        statusMessage = UserData.statusMessage,
        //        uid = UserData.uid,
        //        _id = UserData._id
        //    };
        //    return LogUsersData;
        //}

        //public async Task<LogUsersDto> CreateUser(LogUsersDto User)
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


        //public async Task<ObjectId> SaveNewUser(EventDto SourceEventModel, string ChannelAccessToken, string Hook, LineUserInfoDto UserProfile)
        //{
        //    var UserDataTemp = new LogUsersDto();

        //    //save new profile
        //    if (SourceEventModel.source.type == "developer")
        //    {
        //        var ProjectDataTemp = await EntityUnitOfWork.ProjectRepository.GetAll(x =>
        //        x.LineAccount.Any(
        //            y =>
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

        //public async Task<ObjectId> TakeIdByUid(string UID, string Hook)
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

        //public async Task<bool> CheckActiveStatus(string UID, ActiveStatusLineLogEnum ActiveStatus)
        //{
        //    var UserData = await MongoDBUnitOfWork.MongoDBRepository.GetCollectionLogUsers().Find(x => x.activeStatus == (int)ActiveStatus && x.uid == UID).FirstOrDefaultAsync();
        //    return UserData.activeStatus == (int)ActiveStatus;
        //}

        //public async Task UpdateActiveStatus(LogUsersDto Cus)
        //{
        //    var Filtermongo = new BsonDocument("_id", Cus._id);
        //    var Updatemongo = Builders<LogUsers>.Update.Set("activeStatus", (int)ActiveStatusLineLogEnum.Follow);
        //    var Result = await MongoDBUnitOfWork.MongoDBRepository.GetCollectionLogUsers().FindOneAndUpdateAsync(Filtermongo, Updatemongo);
        //    //if (Result != null)
        //    //{
        //    //}
        //}

        //public async Task<bool> CheckHaveRealTenantPhoneNumber(string PhoneNumber, string ChannelAccessToken)
        //{
        //    return await EntityUnitOfWork.TenantUnitRepository.GetAll(
        //        g =>
        //        g.Tenant.PhoneNo == PhoneNumber
        //        &&
        //        g.Tenant.TenantUnit.Any(
        //            a=> a.Unit.Project.LineAccount.Any(
        //                aa => aa.ChannelAccessToken == ChannelAccessToken
        //                )
        //                &&
        //                a.IsActive
        //                )
        //        ).AnyAsync();
        //}
        #endregion
    }
}
