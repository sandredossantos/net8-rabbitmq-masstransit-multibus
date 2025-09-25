using Messaging.Events;
using Messaging.Publishers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MultiBusController(
        IEventPublisher<CreatedEvent> createdPublisher,
        IEventPublisher<UpdatedEvent> updatedPublisher) : ControllerBase
    {
        private readonly IEventPublisher<CreatedEvent> _createdPublisher = createdPublisher;
        private readonly IEventPublisher<UpdatedEvent> _updatedPublisher = updatedPublisher;

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateAsync()
        {
            var createdEvent = new CreatedEvent { Id = Guid.NewGuid() };
            await _createdPublisher.PublishAsync(createdEvent);
            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateAsync()
        {
            var updatedEven = new UpdatedEvent { Id = Guid.NewGuid() };
            await _updatedPublisher.PublishAsync(updatedEven);
            return Ok();
        }
    }
}
