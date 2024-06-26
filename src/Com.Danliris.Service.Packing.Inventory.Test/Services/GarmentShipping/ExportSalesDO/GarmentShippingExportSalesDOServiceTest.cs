﻿using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ExportSalesDO;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ExportSalesDO;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ExportSalesDO;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.ExportSalesDO
{
    public class GarmentShippingExportSalesDOServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingExportSalesDORepository repository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingExportSalesDORepository)))
                .Returns(repository);

            return spMock;
        }

        protected GarmentShippingExportSalesDOService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentShippingExportSalesDOService(serviceProvider);
        }

        protected GarmentShippingExportSalesDOViewModel ViewModel
        {
            get
            {
                return new GarmentShippingExportSalesDOViewModel
                {
                    items = new List<GarmentShippingExportSalesDOItemViewModel>()
                    {
                        new GarmentShippingExportSalesDOItemViewModel()
                    }
                };
            }
        }

        // Command 30-01-2024
        //[Fact]
        //public async Task Create_Success()
        //{
        //    var repoMock = new Mock<IGarmentShippingExportSalesDORepository>();
        //    repoMock.Setup(s => s.InsertAsync(It.IsAny<GarmentShippingExportSalesDOModel>()))
        //        .ReturnsAsync(1);
        //    repoMock.Setup(s => s.ReadAll())
        //        .Returns(new List<GarmentShippingExportSalesDOModel>().AsQueryable());

        //    var service = GetService(GetServiceProvider(repoMock.Object).Object);

        //    var result = await service.Create(ViewModel);

        //    Assert.NotEqual(0, result);
        //}

        [Fact]
        public void Read_Success()
        {
            var model = new GarmentShippingExportSalesDOModel("", "", 1, DateTimeOffset.Now, 1, "", "", "", "", 1, "", "", "", "",  new List<GarmentShippingExportSalesDOItemModel>());

            var repoMock = new Mock<IGarmentShippingExportSalesDORepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingExportSalesDOModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task ReadById_Success()
        {
            var items = new List<GarmentShippingExportSalesDOItemModel>() { new GarmentShippingExportSalesDOItemModel(1, "", "", "", 1, 1, "", 1, 1, 1, 1) };
            var model = new GarmentShippingExportSalesDOModel("", "", 1, DateTimeOffset.Now, 1, "", "", "", "", 1, "", "", "", "", items);

            var repoMock = new Mock<IGarmentShippingExportSalesDORepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        // Command 30-01-2024
        //[Fact]
        //public async Task Update_Success()
        //{
        //    var repoMock = new Mock<IGarmentShippingExportSalesDORepository>();
        //    repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<GarmentShippingExportSalesDOModel>()))
        //        .ReturnsAsync(1);

        //    var service = GetService(GetServiceProvider(repoMock.Object).Object);

        //    var result = await service.Update(1, ViewModel);

        //    Assert.NotEqual(0, result);
        //}

        // Command 30-01-2024
        //[Fact]
        //public async Task Delete_Success()
        //{
        //    var repoMock = new Mock<IGarmentShippingExportSalesDORepository>();
        //    repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
        //        .ReturnsAsync(1);

        //    var service = GetService(GetServiceProvider(repoMock.Object).Object);

        //    var result = await service.Delete(1);

        //    Assert.NotEqual(0, result);
        //}
    }
}
