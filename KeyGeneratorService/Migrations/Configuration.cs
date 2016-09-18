namespace KeyGeneratorService.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<KeyGeneratorService.KeyGeneratorContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(KeyGeneratorService.KeyGeneratorContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Companies.AddOrUpdate(new Company {Id = 1, Code = 1111, Name = "Shan Motors" }, new Company { Id = 2, Code = 1112,  Name = "Malith Motors" });
            context.SaveChanges();

            Company company = context.Companies.FirstOrDefault(c => c.Name == "Shan Motors");

            context.Users.AddOrUpdate(new User { Id = 1, UserName = "hansamali", Password = "123", CompanyId = company.Id, UserGuid = Guid.NewGuid(), ExpiryDate = DateTime.Now.AddMonths(1) },
                new User { Id = 2, UserName = "buddhika", Password = "123", CompanyId = company.Id, UserGuid = Guid.NewGuid() });
            context.SaveChanges();

        }
    }
}
