using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Configurations
{
    public class ParticipantConfiguration
    {
        public static void OnModelCreating(ModelBuilder builder)
        {
            // Table name and primarey key
            builder.Entity<Participant>().ToTable("Participants").HasKey(e => e.Id);
            builder.Entity<Participant>().Property(e => e.Id).ValueGeneratedOnAdd();

            // Column types
            builder.Entity<Participant>().Property(e => e.Name).HasMaxLength(100).IsRequired();
            builder.Entity<Participant>().Property(e => e.Email).HasMaxLength(100);
        }
    }
}
