using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.SignalR.Client;

namespace Readme.Web.Api.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values    
        //http://localhost:63135/api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", Request.Host.Host + ":" + Request.Host.Port };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            SmtpClient client = new SmtpClient("smtp.sendgrid.net", 587);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("builk", "mairuusi");

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("lapadol@msn.com");
            mailMessage.To.Add("lapadol6109@gmail.com");
            mailMessage.Body = "<h1> body </h1> <br> <div style='background-color:green;width:540px;height:333px;'> qwer</div>";
            mailMessage.Subject = "subject";
            //mailMessage.AlternateViews = "AlternateViews";
            //mailMessage.DeliveryNotificationOptions = "x";
            mailMessage.IsBodyHtml = true;

            client.Send(mailMessage);
            return "value";
        }

        private static async Task<HubConnection> ConnectAsync(string baseUrl)
        {
            // Keep trying to until we can start
            while (true)
            {
                var connection = new HubConnectionBuilder()
                                .WithUrl(baseUrl)
                                .Build();
                try
                {
                    await connection.StartAsync();
                    return connection;
                }
                catch (Exception)
                {
                    await Task.Delay(1000);
                }
            }
        }

        public static void ShowTime(string data)
        {
            var aa = data;
        }
        // GET api/values/signalr
        [HttpGet("signalr")]
        public async Task Signalr()
        {
            HubConnection connection = await ConnectAsync("http://192.168.20.20:1000/NotificationHub");
            connection.On<string>("Send", ShowTime);
            await connection.InvokeAsync<object>("Send", "r rai wa mung");
            await connection.DisposeAsync();
        }


        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
