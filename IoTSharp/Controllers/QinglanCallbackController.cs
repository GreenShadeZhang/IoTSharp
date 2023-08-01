using System;
using IoTSharp.Contracts;
using IoTSharp.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace IoTSharp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QinglanCallbackController : ControllerBase
    {
        private readonly ILogger<QinglanCallbackController> _logger;
        public QinglanCallbackController(ILogger<QinglanCallbackController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        public  Task<ApiResult> PersonCount([FromBody] QinglanPersonCountDto dto)
        {
            _logger.LogInformation($"call back start at time：{DateTime.Now.ToString()}");
            _logger.LogInformation(System.Text.Json.JsonSerializer.Serialize(dto));
            return Task.FromResult(new ApiResult()
            {
                Code = 200,
                Msg = ""
            });
        }
    }
}
