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
using Contracts.DTOs.Relationships;
using Contracts.DTOs.Entities;
using System.Text;

namespace MAUIAndroidApp.Pages
{
    public partial class AddOrder
    {
        private List<Client> clients;
        private List<ProductViewModel> products;

        private int clientId;
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
        private int selectedProductId;
        private string selectedProductDescription;
        private decimal selectedProductPrice;
        private int selectedProductStock;
        private int selectedProductQuantity;

        private List<OrderProductDto> orderLines = new List<OrderProductDto>();

        private void AddLine()
        {
            var selectedProduct = products.FirstOrDefault(p => p.Id == selectedProductId);

            if (selectedProduct != null && selectedProductQuantity > 0)
            {
                orderLines.Add(new OrderProductDto
                {
                    ProductId = selectedProduct.Id,
                    Quantity = selectedProductQuantity,
                    Price = selectedProduct.SellingPrice
                });

                // Clear the selected product and quantity
                selectedProductId = 0;
                selectedProductDescription = "";
                selectedProductPrice = 0;
                selectedProductStock = 0;
                selectedProductQuantity = 0;
            }
        }
        // 
        private async Task CreateDetailedOrder()
        {
            Random random = new Random();
            var detailedOrderDto = new OrderWithDetailsDto()
            {
                ClientId = clientId,
                // TODO: ARREGLAR ACA --> ESTO ES PORQUE EL LOGIN DEBERIA DARME EL ID DEL SELLER LOGEADO!!!
                SellerId = random.Next(1, 9),
                OrderDetails = orderLines,
                Date = DateTime.Now,
            };
            string clientDtoJson = JsonConvert.SerializeObject(detailedOrderDto);
            AndroidHttpClientService httpClientService = new AndroidHttpClientService();
            HttpClient httpClient = httpClientService.GetInsecureHttpClient();

            using (httpClient)
            {
                // Set the base address of your API
                httpClient.BaseAddress = new Uri("https://10.0.2.2:7215/api/orders/detailed");

                // Specify the endpoint for adding clients

                // Set the content type and content
                var content = new StringContent(clientDtoJson, Encoding.UTF8, "application/json");

                // Send the POST request
                var response = await httpClient.PostAsync(httpClient.BaseAddress, content);

                if (response.IsSuccessStatusCode)
                {
                    // TODO: hacer una vista de las orders del seller, y redireccionar ahi!!!
                    // NavManager.NavigateTo("/clientslist");
                }
                else
                {
                    // Handle the error, e.g., log it or show an error message.
                }
            }

        }
    }
}