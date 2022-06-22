using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Test.Models;
using Test.Services;

namespace Test.Controllers
{
    [ApiController]
    [Route("report")]
    public class UserStatisticsController : ControllerBase
    {
        private readonly ILogger<UserStatisticsController> _logger;
        private readonly DatabaseController _db;
        private readonly RequestProcessingConfig _requestProcessingConfig;

        public UserStatisticsController(ILogger<UserStatisticsController> logger, DatabaseController db, RequestProcessingConfig requestProcessingConfig)
        {
            _logger = logger;
            _db = db;
            _requestProcessingConfig = requestProcessingConfig;
        }

        [HttpGet]
        [Route("info")]
        public async Task<IActionResult> GetUserStatistics(string guid)
        {
            try
            {
                if (!Guid.TryParse(guid, out Guid requestGuid))
                {
                    return BadRequest();
                }

                var request = await _db.GetRequestByGuid(requestGuid);
                if (request == null)
                {
                    return NotFound();
                }

                var timePassed = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - request.CreatedAt;

                if (timePassed < _requestProcessingConfig.MinProcessingTimeMilliseconds)
                {
                    return Ok(new StatisticsResponse()
                    {
                        Query = request.Guid,
                        Percent = Convert.ToInt32((Convert.ToDouble(timePassed) / Convert.ToDouble(_requestProcessingConfig.MinProcessingTimeMilliseconds)) * 100.0),
                        Result = null
                    });
                }

                return Ok(new StatisticsResponse()
                {
                    Query = request.Guid,
                    Percent = 100,
                    Result = new UserStatistics()
                    {
                        UserId = request.User.Guid,
                        CountSignIn = await _db.GetUserSignInsCount(request.User.Guid, request.From, request.To)
                    }
                }); ;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Cannot process GetUserStatistics: ");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("user_statistic")]
        public async Task<IActionResult> RequestUserStatistics(StatisticsRequest statisticRequest)
        {
            try
            {
                if (statisticRequest == null || statisticRequest.From > statisticRequest.To || !Guid.TryParse(statisticRequest.UserId, out Guid userId))
                {
                    return BadRequest();
                }

                var user = await _db.GetUserByGuid(userId);
                if (user == null)
                {
                    return BadRequest();
                }

                var request = new Request()
                {
                    Guid = Guid.NewGuid(),
                    From = statisticRequest.From,
                    To = statisticRequest.To,
                    User = user,
                    CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                };

                await _db.AddUserStatisticsRequest(request);

                return Ok(request.Guid);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Cannot process RequestUserStatistics: ");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }
}
