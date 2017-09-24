using Readme.Logic.V1.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Readme.Logic.UnitOfWork.Interface
{
    public interface ILogicUnitOfWork
    {
        
        ILineAccountService LineAccountService { get; set; }
        
        ILogUserService LogUserService { get; set; }
        ILogMessagesService LogMessagesService { get; set; }
        ILogFriendService LogFriendService { get; set; }

        ILineService LineService { get; set; }
        ISignalRService SignalRService { get; set; }
        
    }
}
