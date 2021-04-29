using Microsoft.EntityFrameworkCore;
using Receive.Domain;
using System;
using System.Collections.Generic;
using System.Text;


namespace Receive.Data
{
    public class MensagemContext: DbContext
    {
        DbSet<Mensagem> Mensagem { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=CPDBOOK19;Database=RabbitmqTeste;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mensagem>()
               .ToTable("Mensagem");
            modelBuilder.Entity<Mensagem>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Mensagem>()
                .Property(p => p.CorpoMensagem)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
