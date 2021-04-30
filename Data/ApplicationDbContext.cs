using Microsoft.EntityFrameworkCore; // ORM - Object Relational Model
using TodoList.Models;

namespace TodoList.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}