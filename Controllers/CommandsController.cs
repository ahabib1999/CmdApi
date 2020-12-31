using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CmdApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CmdApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private CommandContext _context;

        public CommandsController(CommandContext context)
        {
            _context = context;
        }

        [HttpGet]

        public ActionResult<IEnumerable<Command>> GetCommands()
        {
            return _context.CommandItems;
        }

        [HttpGet("{id}")]

        public ActionResult<Command> GetCommandItem(int id)
        {
            var CommandItem = _context.CommandItems.Find(id);

            if (CommandItem == null)
            {
                return NotFound();
            }

            return CommandItem;
        }

        [HttpPost]

        public ActionResult<Command> PostCommandItem(Command command)
        {
            _context.CommandItems.Add(command);
            _context.SaveChanges();

            return CreatedAtAction("GetCommandItem", new Command { Id = command.Id }, command);
        }

        [HttpPut]

        public ActionResult PutCommandItem(int id, Command command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            _context.Entry(command).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]

        public ActionResult<Command> DeleteCommandItem(int id)
        {
            var CommandItem = _context.CommandItems.Find(id);

            if (CommandItem == null)
            {
                return NotFound();
            }

            _context.CommandItems.Remove(CommandItem);
            _context.SaveChanges();

            return CommandItem;
        }
    }
}