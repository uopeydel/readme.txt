using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Readme.Web.Api.Models.V1;
using Readme.Logic.UnitOfWork.Interface;
using Readme.Logic.DomainModel;

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
                IdSource = userData.IdSource,
                IdDestination = userData.IdDestination,
            };
            await LogicUnitOfWork.LogFriendService.AddFriend(LogFriendsMongoData);
            return Ok();
        }
    }
}
