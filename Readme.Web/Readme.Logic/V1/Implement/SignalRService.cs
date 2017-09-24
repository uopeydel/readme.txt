using System.Collections.Generic;
using System.Threading.Tasks;
using Readme.Logic.DomainModel;
using Readme.Logic.V1.Interface;
using Readme.DataAccess.EntityFramework.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;
using Readme.Common.Enum;
using System.Linq;
using Readme.DataAccess.Dapper.UnitOfWork.Interface;
using System;
using Readme.DataAccess.EntityFramework.Models;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Readme.Logic.V1.Implement
{
    public class SignalRService : ISignalRService
    {
        private IEntityUnitOfWork EntityUnitOfWork;

        public SignalRService(IEntityUnitOfWork EntityUnitOfWork)
        {
            this.EntityUnitOfWork = EntityUnitOfWork;
        }

        //public async Task Test(string Value) {
            
        //}

        private async Task PostToOldApiForNoti(string JsonObjSerialize, string DomainName, string ApiUrl)
        {
            //var Readme = "http://localhost:54021";// Readme
            var KjApp = "http://localhost:50795";// kjapp

            var DomainUrl = DomainName == "localhost" ? KjApp : DomainName;

            HttpClient Client = new HttpClient();
            //client.DefaultRequestHeaders.Add("Authorization", "Bearer "+ " Token")";"
            StringContent StringContent = new StringContent(JsonObjSerialize, Encoding.UTF8, "application/json");
            HttpResponseMessage Response = await Client.PostAsync(DomainUrl + ApiUrl, StringContent);
            Response.EnsureSuccessStatusCode();
        }


    }
}
