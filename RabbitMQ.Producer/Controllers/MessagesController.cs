using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Producer.Service;

namespace RabbitMQ.Producer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IProducerService _producerService;
        public MessagesController(IProducerService producerService)
        {
            _producerService = producerService;
        }
        [HttpPost("send")]
        public async Task<IActionResult> Send(string message)
        {
            _producerService.Send(message);
            return Ok();
        }

    }
}
