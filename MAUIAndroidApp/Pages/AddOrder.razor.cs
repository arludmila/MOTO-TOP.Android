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
using Newtonsoft.Json;
using Contracts.ViewModels;

namespace MAUIAndroidApp.Pages
{
    public partial class AddOrder
    {
        private List<Client> clients;
        private List<ProductViewModel> products;
        protected async Task GetProducts()
        {
            AndroidHttpClientService httpClientService = new AndroidHttpClientService();
            HttpClient httpClient = httpClientService.GetInsecureHttpClient();

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync("https://10.0.2.2:7215/api/products/view-models");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<ProductViewModel>>(content);
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
        protected override async Task OnInitializedAsync()
        {
            await GetClients();
            await GetProducts();
        }
        protected async Task GetClients()
        {
            AndroidHttpClientService httpClientService = new AndroidHttpClientService();
            HttpClient httpClient = httpClientService.GetInsecureHttpClient();
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync("https://10.0.2.2:7215/api/clients");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    clients = JsonConvert.DeserializeObject<List<Client>>(content);
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