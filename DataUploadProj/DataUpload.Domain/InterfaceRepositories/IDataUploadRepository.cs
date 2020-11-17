using DataUpload.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUpload.Domain.InterfaceRepositories
{
    public interface IDataUploadRepository:IRepository<DataUploadEntity>
    {
        int GetMaxId();
        void BulkInsert(DataTable dt);
    }
}
