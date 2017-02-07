using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using System.Diagnostics;

namespace LoadData.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
  

   

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            #if DEBUG
             Database.Log = s => Debug.WriteLine(s);
            #endif
        }

        public virtual DbSet<KSS_ADRESS> KSS_ADRESS { get; set; }
        public virtual DbSet<KSS_ADRTYP> KSS_ADRTYP { get; set; }
        public virtual DbSet<KSS_AFFOMR> KSS_AFFOMR { get; set; }
        public virtual DbSet<KSS_KONTAKT> KSS_KONTAKT { get; set; }
        public virtual DbSet<KSS_KOSTNADSSTALLE> KSS_KOSTNADSSTALLE { get; set; }
        public virtual DbSet<Anstallning> KSS_ANSTALLNING { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

      
    }



    public partial class SqlServer : DbContext //, ISqlServer
    {
        //public ApplicationDbContext()
        //    : base("DefaultConnection", throwIfV1Schema: false)
        //{
        //}
        public SqlServer()
            : base("name=SqlServerConnection")
        {
        }

        //public virtual DbSet<Person> Person { get; set; }
        //public virtual DbSet<TmpFiles> TmpFiles { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Files>()
        //        .Property(e => e.Notes)
        //        .IsUnicode(false);
        //}
    }
}