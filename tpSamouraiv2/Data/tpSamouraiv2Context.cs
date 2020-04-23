using BO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace tpSamouraiv2.Data
{
    public class tpSamouraiv2Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public tpSamouraiv2Context() : base("name=tpSamouraiv2Context")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
        //    modelBuilder.Entity<Samourai>().HasOptional(s => s.Arme).WithOptionalDependent(s => s.samourai);
            modelBuilder.Entity<Arme>().HasOptional(s => s.Samourai).WithOptionalDependent(s => s.Arme);
            modelBuilder.Entity<Samourai>().HasMany(s => s.ArtsMartiaux).WithMany();
            modelBuilder.Entity<Samourai>().Ignore(s => s.Potentiel);


            base.OnModelCreating(modelBuilder);
        }
           public System.Data.Entity.DbSet<BO.Samourai> Samourais { get; set; }

            public System.Data.Entity.DbSet<BO.Arme> Armes { get; set; }
        public System.Data.Entity.DbSet<BO.ArtMartial> ArtMartial { get; set; }


    }
}
