using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Readme.DataAccess.EntityFramework.Models
{
    public partial class ReadmeContext : DbContext
    {
        public virtual DbSet<LineAccount> LineAccount { get; set; }
        public virtual DbSet<LineMember> LineMember { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-UGQL28B;Database=Readme;Persist Security Info=True;User ID=sa;Password=123456789");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LineAccount>(entity =>
            {
                entity.ToTable("LINE_ACCOUNT");

                entity.Property(e => e.LineAccountId).HasColumnName("line_account_id");

                entity.Property(e => e.LineAccountName)
                    .HasColumnName("line_account_name")
                    .HasColumnType("nchar(10)");
            });

            modelBuilder.Entity<LineMember>(entity =>
            {
                entity.ToTable("LINE_MEMBER");

                entity.Property(e => e.LineMemberId).HasColumnName("line_member_id");

                entity.Property(e => e.LineAccountId).HasColumnName("line_account_id");

                entity.Property(e => e.LineMemberName).HasColumnName("line_member_name");

                entity.HasOne(d => d.LineAccount)
                    .WithMany(p => p.LineMember)
                    .HasForeignKey(d => d.LineAccountId)
                    .HasConstraintName("FK_LINE_MEMBER_LINE_ACCOUNT");
            });
        }
    }
}
