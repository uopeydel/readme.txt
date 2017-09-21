﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

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
            return "value";
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