using System;
using ListaTarefaCRUD.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ListaTarefaCRUD.Api.Infra;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> opt) : base(opt)
    {
    }

    public DbSet<Tarefa> Tarefas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //builder.Entity<Tarefa>()
        //    .ToTable("Tarefas");

        //builder.Entity<Tarefa>()
        //    .HasKey(x => x.Id);

        //builder.Entity<Tarefa>()
        //    .Property(x => x.Titulo)
        //    .HasColumnName("title");

        //builder.Entity<Tarefa>()
        //    .Property(x => x.Detalhes)
        //    .HasColumnName("detail");

        //builder.Entity<Tarefa>()
        //    .Property(x => x.Check)
        //    .HasColumnName("checklist");

        builder.Entity<Tarefa>()
            .HasOne(x => x.Usuario)
            .WithMany(x => x.Tarefas);
    }
}

