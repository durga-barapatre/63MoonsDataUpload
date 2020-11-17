using DataUpload.App_Start;
using DataUpload.Application.Interfaces;
using DataUpload.Application.Models;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DataUpload.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DataUploadWebAPIController : ApiController
    {

        private readonly IDataUpload _dataUploadService;
      
        public DataUploadWebAPIController(IDataUpload dataUploadService)
        {
            this._dataUploadService = dataUploadService;
           
        }
        [HttpGet]
        public IEnumerable<DataUploadModel> GetAll()
        {
            //LogConfig.Configure();
            var logger = Log.ForContext<DataUploadWebAPIController>();
            logger.Information("GetAll");

            //Logger.Information("App is starting ...");
            var list = _dataUploadService.GetListDataUpload();
            return (IEnumerable<DataUploadModel>)list;
        }
        [HttpPost]
        [Route("api/UploadData")]
        public string UploadFile()
        {
            StreamReader Sr = null;
            HttpRequest Request = HttpContext.Current.Request;
            string UplFileName = string.Empty;
            //  HttpPostedFileBase[] httpPostedFile = Request.Files;
            var httpContext = HttpContext.Current;
            //try
            //{
            // Check for any uploaded file  
            if (httpContext.Request.Files.Count > 0)
            {
                //Loop through uploaded files  
                for (int i = 0; i < httpContext.Request.Files.Count; i++)
                {
                    UplFileName = Path.GetFileName(Request.Files[i].FileName);
                    HttpPostedFile httpPostedFile = httpContext.Request.Files[i];
                    if (httpPostedFile != null)
                    {
                        // Construct file save path  
                        var fileSavePath = Path.Combine(HostingEnvironment.MapPath("~/Uploaded/"), httpPostedFile.FileName);
                        // Save the uploaded file  
                        httpPostedFile.SaveAs(fileSavePath);


                        //FileInfo fileInfo = new FileInfo(UplFileName);
                        Sr = new StreamReader(fileSavePath);
                        DataTable oDataTable = null;
                        int RowCount = 0;
                        string[] ColumnNames = null;
                        string[] oStreamDataValues = null;
                        //using while loop read the stream data till end  
                        int result;
                        result = _dataUploadService.GetMaxId();
                        while (!Sr.EndOfStream)
                        {
                            

                            String oStreamRowData = Sr.ReadLine().Trim();
                            if (oStreamRowData.Length > 0)
                            {
                                oStreamDataValues = oStreamRowData.Split(',');
                                //Bcoz the first row contains column names, we will poluate 
                                //the column name by
                                //reading the first row and RowCount-0 will be true only once
                                if (RowCount == 0)
                                {
                                    RowCount = 1;

                                    ColumnNames = oStreamRowData.Split(',');
                                    oDataTable = new DataTable();
                                    //using foreach looping through all the column names
                                    oDataTable.Columns.Add("Id", typeof(int));
                                    foreach (string csvcolumn in ColumnNames)
                                    {
                                        DataColumn oDataColumn = null;
                                        if (csvcolumn == "TradeDate")
                                        {
                                            oDataColumn = new DataColumn(csvcolumn, typeof(DateTime));
                                        }
                                        else
                                        {
                                            oDataColumn = new DataColumn(csvcolumn, typeof(string));

                                        }
                                        //setting the default value of empty.string to newly created column
                                        // oDataColumn.DefaultValue = string.Empty;
                                        //adding the newly created column to the table


                                        oDataTable.Columns.Add(oDataColumn);
                                    }
                                }
                                else
                                {
                                    
                                    


                                    //creates a new DataRow with the same schema as of the oDataTable            
                                    DataRow oDataRow = oDataTable.NewRow();
                                    //using foreach looping through all the column names
                                    for (int c = 0; c < oDataTable.Columns.Count; c++)
                                    {
                                        if (oDataTable.Columns[c].ColumnName == "Id")
                                        {
                                            
                                                oDataRow[oDataTable.Columns[c].ColumnName] = result + c + 1;
                                                                                       
                                            
                                        }
                                        else if(oDataTable.Columns[c].ColumnName == "TradeDate")
                                        {
                                            oDataRow[oDataTable.Columns[c].ColumnName] = oStreamDataValues[c-1] == null ? DateTime.Now : Convert.ToDateTime(oStreamDataValues[c-1]);
                                        }
                                        else
                                        {
                                            oDataRow[oDataTable.Columns[c].ColumnName] = oStreamDataValues[c-1] == null ? string.Empty : oStreamDataValues[c-1].ToString();

                                        }
                                    }
                                    //adding the newly created row with data to the oDataTable       
                                    oDataTable.Rows.Add(oDataRow);
                                }
                            }
                            result++;

                        }
                        //close the oStreamReader object
                        Sr.Close();
                        //release all the resources used by the oStreamReader object
                        Sr.Dispose();
                      
                        _dataUploadService.BulkInsert(oDataTable);
                    }
                }
            }
            //}
            //catch (Exception ex)
            //{
            //    return ("File Posted Failed" + " " + ex.Message.ToString());
            //}

            return "File SuccessFully Posted";

            //List<DataUploadModel> lstDataUploadmodel = new List<DataUploadModel>();


        }


        [HttpPost]
        [Route("api/UploadDataFiles")]
        public string UploadFiles(HttpPostedFileBase[] files)
        {
            //  using (var formData = new MultipartFormDataContent())
            //                {
            foreach (var file in files)
            {
                byte[] fileData;
                using (var reader = new BinaryReader(file.InputStream))
                {
                    fileData = reader.ReadBytes(file.ContentLength);

                }

            }
            return "success";
        }
        public void Post([FromBody] DataUploadModel value)
        {

            _dataUploadService.Add(value);
        }
        // GET: api/DataUploadWebAPI
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/DataUploadWebAPI/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/DataUploadWebAPI
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/DataUploadWebAPI/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/DataUploadWebAPI/5
        public void Delete(int id)
        {
        }
    }
}
