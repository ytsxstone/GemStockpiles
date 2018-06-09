using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Controllers;
using JFJT.GemStockpiles.Commons.Dto;
using JFJT.GemStockpiles.Users;
using JFJT.GemStockpiles.Users.Dto;
using Microsoft.AspNetCore.Mvc;

namespace JFJT.GemStockpiles.Web.AppApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : AbpController
    {
        private readonly IUserAppService _userAppService;
        public ValuesController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        // GET api/values
        [HttpGet]
        public async Task<PagedResultDto<UserDto>> Get()
        {
            return await _userAppService.GetAll(new PagedResultRequestExtendDto { MaxResultCount = 20, SkipCount = 0 });
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
