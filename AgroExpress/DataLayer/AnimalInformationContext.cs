using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AgroExpress.DataLayer.Models;
namespace AgroExpress.DataLayer
{
    public class AnimalInformationContext : DbContext
    {
        public AnimalInformationContext()
           : base("name=AnimalInformationContext")
        {
            Database.SetInitializer(new AnimalInformationContextDBInitilizer());
        }

        public virtual DbSet<AnimalType> AnimalTypes { get; set; }
        public virtual DbSet<AnimalInformation> AnimalInformations { get; set; }

        public virtual DbSet<AnimalWeight> AnimalWeight { get; set; }

        public virtual DbSet<CowHeat> CowHeat { get; set; }

        public virtual DbSet<Vaccination> Vaccinations { get; set; }


        public virtual DbSet<Medication> Medication { get; set; }
        public virtual DbSet<MedicineCourse> MedicineCourses { get; set; }

        public virtual DbSet<MilkProduction> MilkProduction { get; set; }

      
 

    }
    public class AnimalInformationContextDBInitilizer : CreateDatabaseIfNotExists<AnimalInformationContext>
    {
        protected override void Seed(AnimalInformationContext context)
        {

          

        }
    }
}