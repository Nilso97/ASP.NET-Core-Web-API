using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP._NET_Core_Web_API.Entities;
using ASP._NET_Core_Web_API.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace ASP._NET_Core_Web_API.Controllers
{
    [ApiController]
    [Route("api/dev-events")]
    public class DevEventController : ControllerBase
    {
        private readonly DevEventsDbContext _context;

        public DevEventController(DevEventsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllDevEvents()
        {
            var devEvents = _context.Events.Where(e => !e.IsDeleted).ToList();

            return Ok(devEvents);
        }

        [HttpGet("{id}")]
        public IActionResult GetEventById(Guid id)
        {
            var devEvent = _context.Events.SingleOrDefault(e => e.Id == id);

            if (devEvent == null)
            {
                return NotFound($"Message: Evento de id{id} não encontrado!");
            }

            return Ok(devEvent);
        }

        [HttpPost]
        public IActionResult SaveEvent([FromBody] DevEvent devEvent)
        {
            _context.Events.Add(devEvent);

            return CreatedAtAction(nameof(GetEventById), new { id = devEvent.Id }, devEvent);
        }

        [HttpPut("/atualizar/{id}")]
        public IActionResult UpdateEvent(Guid id, [FromBody] DevEvent input)
        {
            var devEvent = _context.Events.SingleOrDefault(e => e.Id == id);

            if (devEvent == null)
            {
                return NotFound($"Message: Evento de id{id} não encontrado!");
            }

            devEvent.Update(input.Title, input.Description, input.StartDate, input.EndDate);

            return NoContent();
        }

        [HttpDelete("/deletar/{id}")]
        public IActionResult DeleteEvent(Guid id)
        {
            var devEvent = _context.Events.SingleOrDefault(e => e.Id == id);

            if (devEvent == null)
            {
                return NotFound($"Message: Evento de id{id} não encontrado!");
            }

            devEvent.Delete();

            return NoContent();
        }

        [HttpPost("{id}/palestrante")]
        public IActionResult SaveSpeakerInEvent(Guid id, DevEventSpeaker speaker)
        {
            var devEvent = _context.Events.SingleOrDefault(d => d.Id == id);

            if (devEvent == null)
            {
                return NotFound($"Message: Evento de id{id} não encontrado!");
            }

            devEvent.Speakers.Add(speaker);

            return NoContent();
        }
    }
}