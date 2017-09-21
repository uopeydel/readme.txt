using Readme.DataAccess.EntityFramework.UnitOfWork.Interface;
using Readme.Logic.DomainModel;
using Readme.Logic.V1.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Readme.Logic.DomainModel.LineModels;
using Readme.DataAccess.EntityFramework.Models;

namespace Readme.Logic.V1.Implement
{
    public class LineAccountService : ILineAccountService
    {
        private IEntityUnitOfWork EntityUnitOfWork;

        public LineAccountService(IEntityUnitOfWork EntityUnitOfWork)
        {
            this.EntityUnitOfWork = EntityUnitOfWork;
        }

        //public async Task<int> GetProjectIdByHook(string ProjectHook)
        //{
        //    var Result = 0;
        //    try
        //    {
        //        var LineAccount = await EntityUnitOfWork.LineAccountRepository.GetSingleAsync(x => x.ParamsHook == ProjectHook);
        //        if (LineAccount != null)
        //        {
        //            Result = LineAccount.ProjectId;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        //TODO: ไม่ควร throw ตรงนี้ ควรหาอะไรมาไว้เก็บ log error /ส่งเมล มาเก็บไว้ว่า error แต่ต้องมีตรวจสอบว่าติด catch Exception หรือไม่
        //        var a = e;
        //        //throw e;
        //    }
        //    return Result;
        //}

        //public async Task<LineAccountModelDto> GetLineAccount(int ProjectId, string ProjectHook, string ChannelAccessToken)
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
        //            ProjectId = s.ProjectId,
        //            ChannelAccessToken = s.ChannelAccessToken,
        //            ProjectCode = s.Project.Code

        //        }).FirstOrDefaultAsync();

        //    return ResultData;
        //}

        //public async Task<string> GetChannelAccessToken(string ProjectHook)
        //{
        //    var Result = "";
        //    try
        //    {
        //        var LineAccount = await EntityUnitOfWork.LineAccountRepository.GetSingleAsync(x => x.ParamsHook == ProjectHook);
        //        if (LineAccount != null)
        //        {
        //            Result = LineAccount.ChannelAccessToken;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        //TODO: ไม่ควร throw ตรงนี้ ควรหาอะไรมาไว้เก็บ log error /ส่งเมล มาเก็บไว้ว่า error แต่ต้องมีตรวจสอบว่าติด catch Exception หรือไม่
        //        var a = e;
        //    }
        //    return Result;
        //}

        //public async Task MemberFollowInProjectLineChannel(string ChannelAccessToken, string UID)
        //{
        //    var LineAtAccount = await EntityUnitOfWork.LineAccountRepository.GetAll(
        //        x =>
        //        x.ChannelAccessToken == ChannelAccessToken
        //        ).FirstOrDefaultAsync();

        //    if (LineAtAccount != null)
        //    {
        //        var FollowMenber = new LineMember();
        //        FollowMenber.LineAccountId = LineAtAccount.LineAccountId;
        //        FollowMenber.LineMemberUid = UID;
        //        FollowMenber.FollowTimeStamp = DateTime.Now.TimeOfDay;
        //        FollowMenber.Active = true;

        //        await EntityUnitOfWork.LineMemberRepository.AddAsync(FollowMenber);
        //    }
        //    else
        //    {
        //        //TODO: ไม่ควร throw ตรงนี้ ควรหาอะไรมาไว้เก็บ log error /ส่งเมล มาเก็บไว้ว่า error แต่ต้องมีตรวจสอบว่าติด catch Exception หรือไม่
        //        //throw new Exception("ChannelAccessToken Not Found.");
        //    }
        //}

        //public async Task MemberUnFollowInProjectLineChannel(string ChannelAccessToken, string UID)
        //{
        //    var LineAtAccount = await EntityUnitOfWork.LineAccountRepository.GetAll(
        //        x =>
        //        x.ChannelAccessToken == ChannelAccessToken
        //        &&
        //        x.LineMember.Any(y => y.LineMemberUid == UID
        //        )
        //        ).FirstOrDefaultAsync();

        //    if (LineAtAccount != null)
        //    {
        //        var MemberLine = await EntityUnitOfWork.LineMemberRepository.GetAll(
        //            g => g.LineMemberUid == UID
        //            &&
        //            g.LineAccountId == LineAtAccount.LineAccountId
        //            ).FirstOrDefaultAsync();

        //        if (MemberLine != null)
        //        {
        //            MemberLine.UnfollowTimeStamp = DateTime.Now.TimeOfDay;
        //            MemberLine.Active = false;


        //            EntityUnitOfWork.LineMemberRepository.Update(MemberLine);
        //        }
        //        else
        //        {
        //            //TODO: ไม่ควร throw ตรงนี้ ควรหาอะไรมาไว้เก็บ log error /ส่งเมล มาเก็บไว้ว่า error แต่ต้องมีตรวจสอบว่าติด catch Exception หรือไม่
        //            //throw new Exception("Member in Channel Not Found.");
        //        }
        //    }
        //    else
        //    {
        //        //TODO: ไม่ควร throw ตรงนี้ ควรหาอะไรมาไว้เก็บ log error /ส่งเมล มาเก็บไว้ว่า error แต่ต้องมีตรวจสอบว่าติด catch Exception หรือไม่
        //        //throw new Exception("ChannelAccessToken Not Found.");
        //    }
        //}


    }
}
