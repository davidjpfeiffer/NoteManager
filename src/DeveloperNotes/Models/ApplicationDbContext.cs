﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using DeveloperNotes.Models;
using Microsoft.Data.Entity.Metadata;

namespace DeveloperNotes.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasMany(n => n.Notes)
                .WithOne(n => n.Creator)
                .HasForeignKey(n => n.NoteId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder.Entity<ApplicationUser>()
            //    .HasMany(n => n.EditedNotes)
            //    .WithOne(n => n.LastEditedByUser)
            //    .HasForeignKey(n => n.LastEditedByUserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(n => n.Revisions)
                .WithOne(n => n.Creator)
                .HasForeignKey(n => n.RevisionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Note>()
                .HasMany(n => n.Revisions)
                .WithOne(n => n.Note)
                .HasForeignKey(n => n.RevisionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Note>()
                .HasOne(n => n.Creator)
                .WithMany(n => n.Notes)
                .HasForeignKey(n => n.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder.Entity<Note>()
            //    .HasOne(n => n.LastEditedByUser)
            //    .WithMany(n => n.Notes)
            //    .HasForeignKey(n => n.LastEditedByUserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Revision>()
                .HasOne(n => n.Creator)
                .WithMany(n => n.Revisions)
                .HasForeignKey(n => n.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Revision>()
                .HasOne(n => n.Note)
                .WithMany(n => n.Revisions)
                .HasForeignKey(n => n.NoteId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Note> Note { get; set; }

        public DbSet<Revision> Revisions { get; set; }
    }
}
