using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Controllers
{
    [ApiController]
    [Route("report")]
    public class UserStatisticsController : ControllerBase
    {
        private readonly ILogger<UserStatisticsController> _logger;

        public UserStatisticsController(ILogger<UserStatisticsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("info")]
        public void Get()
        {
            
        }

        [HttpPost]
        [Route("user_statistic")]
        public void Post()
        {

        }
    }
}
