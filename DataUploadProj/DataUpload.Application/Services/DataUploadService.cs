using DataUpload.Application.Interfaces;
using DataUpload.Application.Mapper;
using DataUpload.Application.Models;
using DataUpload.DAL.Repositories;
using DataUpload.Domain.Domain;
using DataUpload.Domain.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUpload.Application.Services
{
    public class DataUploadService : IDataUpload
    {
        private readonly IRepository<DataUploadEntity> _dataUploadRepos;
        private readonly IDataUploadRepository _dataUploadRepository;
        public DataUploadService(IRepository<DataUploadEntity> dataUploadRepos,
            IDataUploadRepository dataUploadRepository)
        {
            this._dataUploadRepos = dataUploadRepos;
            this._dataUploadRepository = dataUploadRepository;
        }

        public void Add(DataUploadModel entity)
        {
            DataUploadEntity uploadEntity = entity.MapToDataUploadEntity();
            _dataUploadRepos.Add(uploadEntity);
        }

        public void Add(DataUploadEntity entity)
        {
            throw new NotImplementedException();
        }

       

        public void BulkInsert(DataTable dt)
        {
            _dataUploadRepository.BulkInsert(dt);
        }

        public IEnumerable<DataUploadEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<DataUploadModel> GetListDataUpload()
        {

            var datas = _dataUploadRepos.GetAll();
            List<DataUploadModel> model = new List<DataUploadModel>();
            foreach (var data in datas)
            {
                model.Add(data.MapToDataUploadEntity());
            }
            return model;
           // throw new NotImplementedException();
        }

        public int GetMaxId()
        {
            return _dataUploadRepository.GetMaxId();
        }

        public void Update(DataUploadEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
