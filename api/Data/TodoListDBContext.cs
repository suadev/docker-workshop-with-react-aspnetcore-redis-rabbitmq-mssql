using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class TodoListDBContext : DbContext
    {
        public TodoListDBContext(DbContextOptions<TodoListDBContext> options)
              : base(options)
        { }

        public DbSet<Todo> Todos { get; set; }
    }

    public class Todo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Description { get; set; }
        public DateTime DeadLine { get; set; }
        public byte Status { get; set; }
    }
}