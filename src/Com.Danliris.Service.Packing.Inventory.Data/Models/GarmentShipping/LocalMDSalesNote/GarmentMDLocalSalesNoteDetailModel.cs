﻿using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.GarmentShipping.LocalMDSalesNote
{
    public class GarmentMDLocalSalesNoteDetailModel : StandardEntity
    {
        public int LocalSalesNoteItemId { get; set; }
        public string BonNo { get; private set; }
        public double Quantity { get; private set; }
        public int UomId { get; private set; }
        public string UomUnit { get; private set; }
        public string BonFrom { get; private set; }
        public int ComodityId { get; private set; }
        public string ComodityCode { get; private set; }
        public string ComodityName { get; private set; }
        public string RONo { get; private set; }
        public ICollection<GarmentMDSalesNoteDetailItemModel> DetailItems { get; private set; }
        public GarmentMDLocalSalesNoteDetailModel()
        {
        }

        public GarmentMDLocalSalesNoteDetailModel(string bonNo, double quantity, int uomId, string uomUnit, string bonFrom,int comodityId,string comodityCode,string comodityName,string roNo, ICollection<GarmentMDSalesNoteDetailItemModel> detailItems)
        {
            BonNo = bonNo;
            Quantity = quantity;
            UomId = uomId;
            UomUnit = uomUnit;
            BonFrom = bonFrom;
            ComodityId = comodityId;
            ComodityCode = comodityCode;
            ComodityName = comodityName;
            RONo = roNo;
            DetailItems = detailItems;
        }
    }
}
