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
    public class DevEventsController : ControllerBase
    {
        private readonly DevEventsDbContext _context;
        public DevEventsController(DevEventsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllDevEvents()
        {
            var devEvents = _context.DevEvents.Where(d => !d.IsDeleted).ToList();

            return Ok(devEvents);
        }

        [HttpGet("{id}")]
        public IActionResult GetDevEventById(Guid id)
        {
            var devEvent = _context.DevEvents.SingleOrDefault(d => d.Id == id);

            if (devEvent == null) 
            {
                return NotFound($"Message: Evento de id{id} não encontrado!");
            }

            return Ok(devEvent);
        }

        [HttpPost("/salvar")]
        public IActionResult SaveDevEvent([FromBody] DevEvent devEvent)
        {
            _context.DevEvents.Add(devEvent);

            return CreatedAtAction(nameof(GetDevEventById), new { id = devEvent.Id }, devEvent);
        }

        [HttpPut("/atualizar/{id}")]
        public IActionResult UpdateDevEvent(Guid id, [FromBody] DevEvent input)
        {
            var devEvent = _context.DevEvents.SingleOrDefault(d => d.Id == id);

            if (devEvent == null)
            {
                return NotFound($"Message: Evento de id{id} não encontrado!");
            }

            devEvent.Update(input.Title, input.Description, input.StartDate, input.EndDate);

            return NoContent();
        }

        [HttpDelete("/deletar/{id}")]
        public IActionResult DeleteDevEvent(Guid id)
        {
            var devEvent = _context.DevEvents.SingleOrDefault(d => d.Id == id);

            if (devEvent == null)
            {
                return NotFound($"Message: Evento de id{id} não encontrado!");
            }

            devEvent.Delete();

            return NoContent();
        }

        [HttpPost("{id}/palestrante")]
        public IActionResult SaveSpeakerInDevEvent(Guid id, [FromBody] DevEventSpeaker speaker)
        {
            var devEvent = _context.DevEvents.SingleOrDefault(d => d.Id == id);

            if (devEvent == null)
            {
                return NotFound($"Message: Evento de id{id} não encontrado!");
            }

            devEvent.Speakers.Add(speaker);

            return NoContent();
        }
    }
}