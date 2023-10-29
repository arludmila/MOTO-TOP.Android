using global::System;
using global::System.Collections.Generic;
using global::System.Linq;
using global::System.Threading.Tasks;
using global::Microsoft.AspNetCore.Components;
using System.Net.Http;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using MAUIAndroidApp;
using MAUIAndroidApp.Shared;
using Entities.Core;
using Contracts.Utils;
using Newtonsoft.Json;

namespace MAUIAndroidApp.Pages
{
    public partial class ProductsList
    {
        private List<Product> products;
        protected override async Task OnInitializedAsync()
        {
            AndroidHttpClientService httpClientService = new AndroidHttpClientService();
            HttpClient httpClient = httpClientService.GetInsecureHttpClient();

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync("https://10.0.2.2:7215/api/products");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<Product>>(content);
                }
                else
                {
                    // Handle the error, e.g., log it or show a message to the user.
                }
            }
            catch (Exception ex)
            {
                // Handle the exception, e.g., log it or show a message to the user.
            }
        }




    }
}