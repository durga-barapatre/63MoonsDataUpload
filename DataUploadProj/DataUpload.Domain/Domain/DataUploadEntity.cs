﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUpload.Domain.Domain
{
    public class DataUploadEntity:BaseEntity
    {
        #region Properties
        public DateTime? TradeDate { get; set; }
        public string ClientCode { get; set; }
        public int Quantity { get; set; }
        public double? Price { get; set; }
        public double? Value { get; set; }
        #endregion
    }
}
