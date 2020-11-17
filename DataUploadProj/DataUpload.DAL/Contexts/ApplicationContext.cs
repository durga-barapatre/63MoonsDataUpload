using DataUpload.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DataUpload.DAL.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("name=DataUploadConnection")
        {
            Database.SetInitializer<ApplicationContext>(new DBInitializer());

        }
        public virtual DbSet<DataUploadEntity> DataUploadEntities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            // Get All the mapping configuration classes
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !String.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
                    type.BaseType.GetGenericTypeDefinition() == typeof(MappingConfigurtion<>));

            modelBuilder.Entity<DataUploadEntity>().ToTable("DataUploadEntity", "master");
            base.OnModelCreating(modelBuilder);
        }

        public class DBInitializer : DropCreateDatabaseIfModelChanges<ApplicationContext>
        {

            protected override void Seed(ApplicationContext context)
            {
                IList<DataUploadEntity> uploads = new List<DataUploadEntity>();
                uploads.Add(new DataUploadEntity()
                {
                    Id = 1,
                    TradeDate = DateTime.Now,
                    ClientCode = "Cl15",
                    Quantity = 1,
                    Price = 2500,
                    Value = 4

                });
                uploads.Add(new DataUploadEntity()
                {
                    Id = 2,
                    TradeDate = DateTime.Now,
                    ClientCode = "Cl16",
                    Quantity = 2,
                    Price = 3600,
                    Value = 7

                });

                foreach (DataUploadEntity upload in uploads)
                    context.DataUploadEntities.Add(upload);
                context.SaveChanges();

                base.Seed(context);
            }
        }
    }
}
