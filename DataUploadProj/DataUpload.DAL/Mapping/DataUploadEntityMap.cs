using DataUpload.DAL.Contexts;
using DataUpload.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUpload.DAL.Mapping
{
    public class DataUploadEntityMap:MappingConfigurtion<DataUploadEntity>
    {
        public DataUploadEntityMap()
        {
            this.ToTable("DataUploadEntity", "Master");
            this.Property(x => x.TradeDate);
            this.Property(x => x.ClientCode).HasMaxLength(15);
            this.Property(x => x.Quantity);
            this.Property(x => x.Price);
            this.Property(x => x.Value);
        }
    }
}
