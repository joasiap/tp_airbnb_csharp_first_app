using Microsoft.EntityFrameworkCore;

namespace AirbnbAppli.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Utilisateur> Utilisateurs { get; set; }

        public DbSet<Logement> Logements { get; set; }

        public DbSet<Adresse> Adresses { get; set; }

        public DbSet<Departement> Departements { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Message> Messages { get; set; }
    }
}
