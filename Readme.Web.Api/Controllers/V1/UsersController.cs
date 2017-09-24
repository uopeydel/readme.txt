using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Readme.Logic.UnitOfWork.Interface;
using Readme.Web.Api.Models.V1;
using Readme.Logic.DomainModel;
using MongoDB.Bson;


namespace Readme.Web.Api.Controllers.V1
{

    [Route("api/V1/[controller]")]
    [EnableCors("AllowAll")]
    public class UsersController : Controller
    {
        private readonly ILogicUnitOfWork LogicUnitOfWork;

        public UsersController(ILogicUnitOfWork logicUnitOfWork)
        {
            LogicUnitOfWork = logicUnitOfWork;
        }

        [HttpGet("GetUserByEmail/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var UserData = await LogicUnitOfWork.LogUserService.GetUser(ObjectId.Empty, email, "");
            return Ok(UserData);
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var UserData = await LogicUnitOfWork.LogUserService.GetUser(ObjectId.Parse(id), "", "");
            return Ok(UserData);
        }

        [HttpGet("GetUserByUID/{uid}")]
        public async Task<IActionResult> GetUserByDisplayName(string uid)
        {
            var UserData = await LogicUnitOfWork.LogUserService.GetUser(ObjectId.Empty, "", uid);
            return Ok(UserData);
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody]LogUsersModel userData)
        {

            var LogUsersMongoData = new LogUsersMongoDto()
            {
                DisplayName = userData.DisplayName,
                Email = userData.Email,
                PictureUrl = userData.PictureUrl,
                StatusMessage = userData.StatusMessage,
                UID = userData.UID,
            };
            await LogicUnitOfWork.LogUserService.CreateUser(LogUsersMongoData);
            return Ok();
        }

    }
}
