using Readme.Logic.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Readme.Logic.V1.Interface
{
    public interface ISignalRService
    {
        Task Test(string Value);
    }
}
