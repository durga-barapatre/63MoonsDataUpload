using DataUpload.Application.Models;
using DataUpload.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUpload.Application.Interfaces
{
   public  interface IDataUpload
    {
        int GetMaxId();
        void BulkInsert(DataTable dt);
        void Add(DataUploadModel entity);
        List<DataUploadModel> GetListDataUpload();
    }
}
