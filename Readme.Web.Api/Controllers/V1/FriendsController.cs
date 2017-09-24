using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Readme.Web.Api.Models.V1;
using Readme.Logic.UnitOfWork.Interface;
using Readme.Logic.DomainModel;
using MongoDB.Bson;

namespace Readme.Web.Api.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [EnableCors("AllowAll")]
    public class FriendsController : Controller
    {
        private readonly ILogicUnitOfWork LogicUnitOfWork;

        public FriendsController(ILogicUnitOfWork logicUnitOfWork)
        {
            LogicUnitOfWork = logicUnitOfWork;
        }

        [HttpPost("AddFriend")]
        public async Task<IActionResult> AddFriend([FromBody]LogFriendsModel userData)
        {

            var LogFriendsMongoData = new LogFriendsMongoDto()
            {
                IdSource = ObjectId.Parse(userData.IdSource) ,
                IdDestination = ObjectId.Parse(userData.IdDestination),
            };
            await LogicUnitOfWork.LogFriendService.AddFriend(LogFriendsMongoData);
            return Ok();
        }

        [HttpGet("GetFriendList/{_id}")]
        public async Task<IActionResult> GetFriendList(string _id)
        {
            var FriendList = await LogicUnitOfWork.LogFriendService.GetFriendList(ObjectId.Parse(_id));
            var FriendDataList = new LogUsersModel[] { };
            FriendDataList = FriendList.Select(s => new LogUsersModel
            {
                _id = s._id.ToString(),
                DisplayName = s.DisplayName,
                PictureUrl = s.PictureUrl,
                StatusMessage = s.StatusMessage
            }).ToArray();
            return Ok(FriendList);
        }
    }
}
