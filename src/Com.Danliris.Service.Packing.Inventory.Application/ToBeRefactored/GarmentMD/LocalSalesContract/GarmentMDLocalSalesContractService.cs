﻿using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalSalesContract;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalSalesContract;
using Com.Danliris.Service.Packing.Inventory.Data.Models.GarmentShipping.LocalSalesContract;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentMD;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.LogHistory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentMD.LocalSalesContract
{
    public class GarmentMDLocalSalesContractService : IGarmentMDLocalSalesContractService
    {
        private readonly IGarmentMDLocalSalesContractRepository _repository;
        private readonly IServiceProvider serviceProvider;
        protected readonly ILogHistoryRepository logHistoryRepository;

        public GarmentMDLocalSalesContractService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IGarmentMDLocalSalesContractRepository>();
            logHistoryRepository = serviceProvider.GetService<ILogHistoryRepository>();
            this.serviceProvider = serviceProvider;
        }

        private string GenerateNo(GarmentMDLocalSalesContractViewModel vm)
        {
            var year = DateTime.Now.ToString("yy");
            var month = DateTime.Now.ToString("MM");

            var prefix = $"DL/GMT/{(vm.transactionType.code ?? "").Trim().ToUpper()}/{month}/{year}";

            var lastInvoiceNo = _repository.ReadAll().Where(w => w.SalesContractNo.StartsWith(prefix))
                .OrderByDescending(o => o.SalesContractNo)
                .Select(s => int.Parse(s.SalesContractNo.Replace(prefix, "")))
                .FirstOrDefault();

            var invoiceNo = $"{prefix}{(lastInvoiceNo + 1).ToString("D4")}";

            return invoiceNo;
        }

        public GarmentMDLocalSalesContractViewModel MapToViewModel(GarmentMDLocalSalesContractModel model)
        {
            GarmentMDLocalSalesContractViewModel viewModel = new GarmentMDLocalSalesContractViewModel
            {
                Active = model.Active,
                Id = model.Id,
                CreatedAgent = model.CreatedAgent,
                CreatedBy = model.CreatedBy,
                CreatedUtc = model.CreatedUtc,
                DeletedAgent = model.DeletedAgent,
                DeletedBy = model.DeletedBy,
                DeletedUtc = model.DeletedUtc,
                IsDeleted = model.IsDeleted,
                LastModifiedAgent = model.LastModifiedAgent,
                LastModifiedBy = model.LastModifiedBy,
                LastModifiedUtc = model.LastModifiedUtc,

                salesContractNo = model.SalesContractNo,
                salesContractDate = model.SalesContractDate,
                transactionType = new TransactionType
                {
                    id = model.TransactionTypeId,
                    code = model.TransactionTypeCode,
                    name = model.TransactionTypeName
                },
                buyer = new Buyer
                {
                    Id = model.BuyerId,
                    Code = model.BuyerCode,
                    Name = model.BuyerName,
                    npwp = model.BuyerNPWP,
                    Address = model.BuyerAddress
                },
                isUseVat = model.IsUseVat,
                vat = new Vat
                {
                    id = model.VatId,
                    rate = model.VatRate,
                },
                sellerAddress = model.SellerAddress,
                sellerName = model.SellerName,
                sellerNPWP = model.SellerNPWP,
                sellerPosition = model.SellerPosition,

                comodityName = model.ComodityName,
                isLocalSalesDOCreated = model.IsLocalSalesDOCreated,
                quantity = model.Quantity,
                remainingQuantity = model.RemainingQuantity,
                uom = new UnitOfMeasurement
                {
                    Id = model.UomId,
                    Unit = model.UomUnit
                },

                remark = model.Remark,
                price = model.Price,

                subTotal = model.SubTotal,
            };

            return viewModel;
        }

        public GarmentMDLocalSalesContractModel MapToModel(GarmentMDLocalSalesContractViewModel vm)
        {
            vm.uom = vm.uom ?? new UnitOfMeasurement();
            vm.transactionType = vm.transactionType ?? new TransactionType();
            vm.buyer = vm.buyer ?? new Buyer();
            vm.vat = vm.vat ?? new Vat();

            return new GarmentMDLocalSalesContractModel(GenerateNo(vm), vm.salesContractDate.GetValueOrDefault(), vm.transactionType.id, vm.transactionType.code, vm.transactionType.name, vm.sellerName, vm.sellerPosition, vm.sellerAddress, vm.sellerNPWP, vm.buyer.Id, vm.buyer.Code, vm.buyer.Name, vm.buyer.Address, vm.buyer.npwp, vm.isUseVat, vm.vat.id, vm.vat.rate, vm.subTotal, vm.isLocalSalesDOCreated, vm.comodityName, vm.quantity, vm.remainingQuantity, vm.uom.Id.Value, vm.uom.Unit, vm.price, vm.remark) { Id = vm.Id };
        }

        public async Task<int> Create(GarmentMDLocalSalesContractViewModel viewModel)
        {
            var model = MapToModel(viewModel);

            //Add Log History
            await logHistoryRepository.InsertAsync("MERCHANDISER", "Create Sales Contract Lokal - " + model.SalesContractNo);

            int Created = await _repository.InsertAsync(model);

            return Created;
        }

        public ListResult<GarmentMDLocalSalesContractViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll();
            List<string> SearchAttributes = new List<string>()
            {
                "SalesContractNo", "BuyerCode", "BuyerName", "SellerName", "TransactionTypeCode", "TransactionTypeName"
            };
            query = QueryHelper<GarmentMDLocalSalesContractModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentMDLocalSalesContractModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentMDLocalSalesContractModel>.Order(query, OrderDictionary);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(model => MapToViewModel(model))
                .ToList();

            return new ListResult<GarmentMDLocalSalesContractViewModel>(data, page, size, query.Count());
        }

        public async Task<GarmentMDLocalSalesContractViewModel> ReadById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);

            var viewModel = MapToViewModel(data);

            return viewModel;
        }

        public async Task<int> Update(int id, GarmentMDLocalSalesContractViewModel viewModel)
        {
            var model = MapToModel(viewModel);

            //Add Log History
            await logHistoryRepository.InsertAsync("MERCHANDISER", "Update Sales Contract Lokal - " + model.SalesContractNo);

            return await _repository.UpdateAsync(id, model);
        }

        public async Task<int> Delete(int id)
        {
            var data = await _repository.ReadByIdAsync(id);

            //Add Log History
            await logHistoryRepository.InsertAsync("MERCHANDISER", "Delete Sales Contract Lokal - " + data.SalesContractNo);

            return await _repository.DeleteAsync(id);
        }
    }
}
