using DataUpload.Application.Models;
using DataUpload.Domain.Domain;

namespace DataUpload.Application.Mapper
{
    public static class EntityMapper
    {
        public static DataUploadEntity MapToDataUploadEntity(this DataUploadModel dataUploadModOBj)
        {
            return new DataUploadEntity()
            {
                Id = dataUploadModOBj.Id,
                TradeDate = dataUploadModOBj.TradeDate,
                ClientCode = dataUploadModOBj.ClientCode,
                Quantity = dataUploadModOBj.Quantity,
                Price = dataUploadModOBj.Price,
                Value = dataUploadModOBj.Value

            };
        }

        public static DataUploadModel MapToDataUploadEntity(this DataUploadEntity dataUploadEntObj)
        {
            return new DataUploadModel()
            {
                Id = dataUploadEntObj.Id,
                TradeDate = dataUploadEntObj.TradeDate,
                ClientCode = dataUploadEntObj.ClientCode,
                Quantity = dataUploadEntObj.Quantity,
                Price = dataUploadEntObj.Price,
                Value = dataUploadEntObj.Value

            };
        }

    }
}
