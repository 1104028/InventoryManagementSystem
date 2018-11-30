namespace AgroExpress.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Helpers;
    using AgroExpress.DataLayer.Models;
    //using AgroExpress.Help;
    public class AgroExpressContext : DbContext
    {

        public AgroExpressContext()
            : base("name=AgroExpressContext")
        {
            Database.SetInitializer(new AgroExpressContextDBInitilizer());
        }
        #region
        // User Table
        public virtual DbSet<UserLogin> UserLoginInfo { get; set; }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<DeliveryBoy> DeliveryBoy { get; set; }

        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Partner> Partners { get; set; }

        public virtual DbSet<FirmManager> FirmManger { get; set; }

        public virtual DbSet<SalePointRelation> SalePointRelations { get; set; }
        // user table end
        #endregion

        #region

        public virtual DbSet<SalePoint> SalePoints { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<SubArea> SubAreas { get; set; }
        #endregion



        #region
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<BillingHistory> BillingHistory { get; set; }

        #endregion

        #region
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Production> Productions { get; set; }
        public virtual DbSet<SalePointProductConsume> SalePointProductConsumes { get; set; }
        public virtual DbSet<SalePointProductStock> SalePointProductStocks { get; set; }
        #endregion


        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<MilkSummary> MilkSummary { get; set; }

        public virtual DbSet<SMSConfiguration> SMSconfig { get; set; }
    }
    public class AgroExpressContextDBInitilizer : DropCreateDatabaseIfModelChanges<AgroExpressContext>
    {
        protected override void Seed(AgroExpressContext context)
        {
           

            context.UserLoginInfo.Add(new UserLogin
            {
                UserName = "Admin",
                UserType = 1,
                Password = "1234567"
            });
            context.SaveChanges();

            var user = context.UserLoginInfo.Where(a => a.UserName == "Admin").FirstOrDefault();

            context.Admins.Add(new Admin
            {
                FullName = "Rochi",
                Mobile="01711697952",
                Address="Shantinagar, Dhaka",
                Email="agroexpressbd@gmail.com",
                LoginUserID=user.PkUserID,
                
            });
            context.SMSconfig.Add(new SMSConfiguration
            {
               
                Masking = "",
                APIKey = "4d9f2ff0-7"

            });

            context.SaveChanges();
        }
    }
}