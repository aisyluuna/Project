using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QueueForChildren.Data.Entities;
using QueueForChildren.Data.Entities.FNS;
using QueueForChildren.Data.Entities.Zags;
using QueueForChildren.Data.Identity;

namespace QueueForChildren.Web.DataContext
{
    public sealed class QueueDbContext : IdentityDbContext<User>
    {
        public QueueDbContext(DbContextOptions<QueueDbContext> options)
        : base(options)
        {           
        }

        public DbSet<Address> Address { get; set; }
        public DbSet<Child> Child { get; set; }
        public DbSet<Kindergarten> Kindergarten { get; set; }
        public DbSet<KindergartenFillInfo> KindergartenFillInfo { get; set; }
        public DbSet<KindergartenLanguage> KindergartenLanguage { get; set; }
        public DbSet<Parent> Parent { get; set; }
        public DbSet<QueueToKindergarten> QueueToKindergarten { get; set; }
        public DbSet<QueueToSchool> QueueToSchool { get; set; }
        public DbSet<School> School { get; set; }
        public DbSet<SchoolFillInfo> SchoolFillInfo { get; set; }
        public DbSet<SchoolLanguage> SchoolLanguage { get; set; }

        public DbSet<Applicant> Applicant { get; set; }
        public DbSet<Estate> Estate { get; set; }
        public DbSet<ZagsChild> ZagsChild { get; set; }
        public DbSet<ZagsParent> ZagsParent { get; set; }
        

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=queue_for_children;Username=postgres;Password=294495");
        // }
    }
}
