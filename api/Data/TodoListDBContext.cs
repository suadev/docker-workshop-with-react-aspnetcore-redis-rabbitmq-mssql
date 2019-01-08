using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class TodoListDBContext : DbContext
    {
        public TodoListDBContext(DbContextOptions<TodoListDBContext> options)
              : base(options)
        { }

        public DbSet<TodoListModel> Todos { get; set; }
    }
}