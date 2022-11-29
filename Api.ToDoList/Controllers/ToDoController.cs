using Api.ToDoList.Application.Entities;
using Api.ToDoList.Infrastructure.DataPersistence;
using Api.ToDoList.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Reflection;

namespace Api.ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoDbContext _context;

        public ToDoController(ToDoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [Route("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            ToDo? todo = await _context.ToDos.SingleOrDefaultAsync(t => t.Id == id);

            if (todo == null)
                return NotFound();

            return Ok(todo);
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult> Post([FromBody] ToDoRequest request)
        {
            var todo = new ToDo(0, request.Title, request.Description);

            await _context.ToDos.AddAsync(todo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = todo.Id }, request);
        }
    }
}
