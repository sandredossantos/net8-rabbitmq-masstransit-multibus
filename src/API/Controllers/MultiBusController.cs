using Messaging.Events;
using Messaging.Producers;
using Messaging.Publishers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MultiBusController(ICreatedProducer createdProducer, IUpdatedProducer updatedProducer) : ControllerBase
    {
        private readonly ICreatedProducer _createdProducer = createdProducer;
        private readonly IUpdatedProducer _updatedProducer = updatedProducer;

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateAsync()
        {
            var createdEvent = new CreatedEvent 
            {
                Id = Guid.NewGuid()
            };

            await _createdProducer.ProduceAsync(createdEvent);

            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateAsync()
        {
            var updatedEvent = new UpdatedEvent 
            {
                Id = Guid.NewGuid() 
            };

            await _updatedProducer.ProduceAsync(updatedEvent);

            return Ok();
        }
    }
}
