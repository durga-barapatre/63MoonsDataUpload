using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUpload.Domain.InterfaceRepositories
{
    public interface IADORepository
    {
        void bulkInsert(DataTable dt);
        int GetReturnFromSP(string ProcedureName, SqlParameter[] parameters = null);
        DataTable GetDataTable(string StoredProcedureName, SqlParameter[] parameters =null);
    }
}
