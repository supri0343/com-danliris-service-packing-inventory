﻿using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Utilities
{
    public class HttpClientServiceTestCoverage
    {
        private const string url = "http://127.0.1.1/";
        private HttpClientService HttpClientService => new HttpClientService(new IdentityProvider());
        private HttpContent HttpContent => new StringContent("");

        [Fact]
        public async Task Get()
        {
            await Assert.ThrowsAsync<HttpRequestException>(() => HttpClientService.GetAsync(url));
        }
    }
}
