﻿using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse
{
    public class OutputPreWarehouseIndexViewModel : BaseViewModel
    {
        public ProductionOrder ProductionOrder { get; set; }
        public string CartNo { get; set; }
        public string Buyer { get; set; }
        public string Construction { get; set; }
        public string Unit { get; set; }
        public string Color { get; set; }
        public string Motif { get; set; }
        public string UomUnit { get; set; }
        public string Remark { get; set; }
        public string Grade { get; set; }
        public string Status { get; set; }
        public double Balance { get; set; }
        public string PackingInstruction { get; set; }
        public string PackagingType { get; set; }
        public decimal PackagingQty { get; set; }
        public string PackagingUnit { get; set; }
        public double AvalALength { get; set; }
        public double AvalBLength { get; set; }
        public double AvalConnectionLength { get; set; }
        public long DeliveryOrderSalesId { get; set; }
        public string DeliveryOrderSalesNo { get; set; }
        public string AvalType { get; set; }
        public string AvalCartNo { get; set; }
        public double AvalQuantityKg { get; set; }
        public string Description { get; set; }
        public string DeliveryNote { get; set; }
        public string Area { get; set; }
        public string DestinationArea { get; set; }
        public bool HasNextAreaDocument { get; set; }
        public int DyeingPrintingAreaInputProductionOrderId { get; set; }
        public int OutputId { get; set; }


        //public ProductionOrder ProductionOrder { get; set; }
        //public string ProductionOrderNo { get; set; }
        //public string CartNo { get; set; }
        //public string PackingInstruction { get; set; }
        //public string Construction { get; set; }
        //public string Unit { get; set; }
        //public string Buyer { get; set; }
        //public string Color { get; set; }
        //public string Motif { get; set; }
        //public string UomUnit { get; set; }
        //public double Balance { get; set; }
        //public bool HasNextAreaDocument { get; set; }
        //public string Grade { get; set; }
        //public string Remark { get; set; }
        //public string Status { get; set; }
        //public string PackagingType { get; set; }
        //public string PackagingUnit { get; set; }
        //public decimal PackagingQty { get; set; }
        //public double QtyOrder { get; set; }
        //public int DyeingPrintingAreaOutputId { get; set; }


    }
}