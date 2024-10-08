﻿using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentRecapOmzetReport;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
using Com.DanLiris.Service.Purchasing.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.GarmentShipping.Monitoring
{
    [Produces("application/json")]
    [Route("v1/garment-shipping/monitoring/garment-recap-omzet")]
    [Authorize]
    public class GarmentRecapOmzetReportController : ControllerBase
    {
        //private string ApiVersion = "1.0.0";
        private readonly IGarmentRecapOmzetReportService _service;
        private readonly IIdentityProvider _identityProvider;

        public GarmentRecapOmzetReportController(IGarmentRecapOmzetReportService service, IIdentityProvider identityProvider)
        {
            _service = service;
            _identityProvider = identityProvider;
        }

        protected void VerifyUser()
        {
            _identityProvider.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;
            _identityProvider.Token = Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer ", "");
            _identityProvider.TimezoneOffset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
        }

        public class DateParm
        {
          public DateTime? DateFrom { get; set; }
          public DateTime? DateTo { get; set; }
        }

        [HttpGet]
        public IActionResult GetReport(DateTime? dateFrom, DateTime? dateTo, int page, int size, string Order = "{}")
        {
            int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
            string accept = Request.Headers["Accept"];
            try
            {

                VerifyUser();
                var data = _service.GetReportData(dateFrom, dateTo, offset);

                var info = new Dictionary<string, object>
                    {
                        { "count", data.Data.Count },
                        { "total", data.Total },
                    };

                return Ok(new
                {
                    data = data.Data,
                    info
                });
            }

            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("download")]
        public IActionResult GetXls(DateTime? dateFrom, DateTime? dateTo)
        {
            try
            {
                VerifyUser();
                byte[] xlsInBytes;
                int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : Convert.ToDateTime(dateFrom);
                DateTime DateTo = dateTo == null ? DateTime.Now : Convert.ToDateTime(dateTo);

                var xls = _service.GenerateExcel(dateFrom, dateTo, offset);

                string filename = String.Format("Buku Penjualan Export Garment - {0}.xlsx", DateTime.UtcNow.ToString("ddMMyyyy"));

                xlsInBytes = xls.ToArray();
                var file = File(xlsInBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
                return file;

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost("post-balance")]
        public async Task<IActionResult> PostToBalance([FromBody]DateParm dateParm)
        {
            try
            {
                VerifyUser();
                var result = await _service.PushToBeginingBalance(dateParm.DateFrom, dateParm.DateTo, _identityProvider.TimezoneOffset);

                return Created("/", new { data = result });
            }
            catch (ServiceValidationException ex)
            {
                var Result = new
                {
                    error = ResultFormatter.Fail(ex),
                    apiVersion = "1.0.0",
                    statusCode = HttpStatusCode.BadRequest,
                    message = "Data does not pass validation"
                };

                return new BadRequestObjectResult(Result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
