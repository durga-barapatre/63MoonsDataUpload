using DataUpload.DAL.Contexts;
using DataUpload.Domain.Domain;
using DataUpload.Domain.InterfaceRepositories;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataUpload.DAL.Repositories
{
    public class DataUploadRepository : Repository<DataUploadEntity>, IDataUploadRepository
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IADORepository _adoRepository;
        public DataUploadRepository(ApplicationContext applicationContext, ADORepository adoRepository) : base(applicationContext)
        {
            _applicationContext = applicationContext;
            _adoRepository = adoRepository;
        }

        public void BulkInsert(DataTable dt)
        {
            _adoRepository.bulkInsert(dt);
        }

        public int GetMaxId( )
        {
            int result = 0;
            try
            {
                result = _adoRepository.GetReturnFromSP("[master].[PROC_GetMaxId]");
            }
            catch (Exception ex)
            {
                
            }
            
            return result;
        }

    }
}
