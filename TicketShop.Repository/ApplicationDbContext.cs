using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketShop.Domain.DomainModels;
using TicketShop.Domain.Identity;
namespace TicketShop.Repository
{
    public class ApplicationDbContext : IdentityDbContext<TicketUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Concert> Concerts { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        //   public virtual DbSet<ConcertOnTicket> ConcertOnTickets { get; set; }
        //  public virtual DbSet<TicketOfUser> TicketOfUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Concert>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Ticket>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
            builder.Entity<TicketUser>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
            /*
                        builder.Entity<ConcertOnTicket>()
                            .HasKey(z => new {z.ConcertId});

                        builder.Entity<ConcertOnTicket>()
                            .HasOne(z => z.Concert)
                            .WithMany(z => z.ConcertOnTickets)
                            .HasForeignKey(z => z.TicketId);

                        builder.Entity<ConcertOnTicket>()
                            .HasOne(z => z.Ticket)
                            .WithMany()
                            .HasForeignKey(z => z.ConcertId);
            */
            builder.Entity<Ticket>()
                   .HasOne(t => t.TicketUser)
                    .WithMany(u => u.Tickets)
                    .HasForeignKey(t => t.TicketUserId); // Foreign key pointing to TicketUser's primary key

            // Ensure TicketUser's primary key is of type string
            builder.Entity<TicketUser>()
                .HasKey(u => u.Id);

            builder.Entity<Ticket>()
                 .HasOne(t => t.Concert)
                  .WithMany(u => u.Tickets)
     .HasForeignKey(t => t.ConcertId); // Foreign key pointing to TicketUser's primary key

            // Ensure TicketUser's primary key is of type string
            builder.Entity<Concert>()
                .HasKey(u => u.Id);
        }
    }
}
