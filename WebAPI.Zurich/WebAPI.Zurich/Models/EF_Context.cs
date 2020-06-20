
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WebAPI.Zurich.Models
{
    public class EF_Context : DbContext
    {
        public DbSet<Segurado> segurado { get; set; }
        public DbSet<Seguro> seguro { get; set; }
        public DbSet<Veiculo> veiculo { get; set; }

        public EF_Context() : base("ConnectionStringBanco")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}