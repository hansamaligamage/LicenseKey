using System.Data.Entity;

namespace KeyGeneratorService
{
    public class KeyGeneratorContext : DbContext
    {

        public KeyGeneratorContext () : base ("KeyGeneratorContext")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<KeyGeneratorContext>());
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Company> Companies { get; set; }

    }
}
