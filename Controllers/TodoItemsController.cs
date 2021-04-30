using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Controllers
{
    [ApiController]
    [Route("[controller]")] // https://localhost:5001/TodoItems
    public class TodoItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TodoItemsController> _logger;

        public TodoItemsController(ApplicationDbContext context, ILogger<TodoItemsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost] // Incluir novo Registro
        public TodoItem Post(TodoItem model)
        {
            _context.TodoItems.Add(model);
            _context.SaveChanges();

            return model;
        }

        [HttpGet] // Obter uma lista
        public List<TodoItem> GetList()
        {
            var todoItems = _context.TodoItems.ToList();
            return todoItems;
        }

        [HttpPut] // Alterar
        public async Task<ActionResult> PutItem(TodoItem model)
        {
            var todoItem = await _context.TodoItems.FindAsync(model.Id);
            if (todoItem == null)
                return NotFound("Registro não encontrado na base de dados.");

            todoItem.Name = model.Name;
            todoItem.IsComplete = model.IsComplete;

            _context.SaveChanges();

            return Ok(true);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
                return NotFound("Registro não encontrado na base de dados.");

            _context.Remove(todoItem);
            _context.SaveChanges();

            return Ok(true);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetItem(long id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }
    }
}