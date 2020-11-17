using DataUpload.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUpload.DAL.Contexts
{
    public abstract class MappingConfigurtion<T>: EntityTypeConfiguration<T> where T :BaseEntity
    {
        public MappingConfigurtion()
        {
            this.HasKey(x => x.Id);
        }
    }
}
