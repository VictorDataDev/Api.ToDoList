using Api.ToDoList.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.ToDoList.Infrastructure.DataPersistence
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options) { }

        public DbSet<ToDo> ToDos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ToDo>()
                .HasKey(t => t.Id);
        }
    }
}
