

using DataUpload.Domain.InterfaceRepositories;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataUpload.DAL.Repositories
{
    public class ADORepository : IADORepository
    {

        private string connectionString = ConfigurationManager.ConnectionStrings["DataUploadConnection"].ConnectionString;
        public int GetReturnFromSP(string ProcedureName, SqlParameter[] parameters = null)
        {
            SqlConnection connection = null;
            int result = 0;
            try
            {
                connection = new SqlConnection(connectionString);

                using (SqlCommand cmd = new SqlCommand(ProcedureName, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    result = Convert.ToInt32(returnParameter.Value);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        public DataTable GetDataTable(String StoredProcedureName, SqlParameter[] parameters = null)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                using (SqlCommand cmd = new SqlCommand(StoredProcedureName, connection))
                {
                    DataTable dt = new DataTable();
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    SqlDataAdapter DA = new SqlDataAdapter(cmd);

                    if (DA != null)
                    {
                        DA.Fill(dt);
                    }
                    connection.Close();
                    return dt;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void bulkInsert(DataTable oDataTable)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataUploadConnection"].ConnectionString);
            using (SqlBulkCopy blkCopy = new SqlBulkCopy(conn))
            {
                conn.Open();

                blkCopy.DestinationTableName = "master.DataUploadEntity";
                foreach (var column in oDataTable.Columns)
                    blkCopy.ColumnMappings.Add(column.ToString(), column.ToString());
                blkCopy.WriteToServer(oDataTable);
            }
        }
    }
}
