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
using Contracts.DTOs.Entities;
using Entities.Enums;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Maui.Controls;

namespace MAUIAndroidApp.Pages
{
    public partial class AddClient
    {
        private List<string> cities = new List<string>();
        private string firstName;
        private string lastName;
        private string province;
        private string city;
        private string address;
        private string phoneNumber;
        private PersonDocType documentType;
        private string documentNumber;
        private string email;
        private void ProvinceChanged(ChangeEventArgs e)
        {
            string selectedProvince = e.Value.ToString();
            cities = GetCitiesForProvince(selectedProvince);
            province = selectedProvince;
        }
        private void DocTypeChanged(ChangeEventArgs e)
        {
            switch (e.Value)
            {
                case "0":
                    documentType = PersonDocType.DNI;
                    break;
                case "1":
                    documentType = PersonDocType.CUIT;
                    break;
                case "2":
                    documentType = PersonDocType.DNI;
                    break;
                default:
                    break;
            }
            
        }
        private List<string> GetCitiesForProvince(string province)
        {
            if (province == "Corrientes")
            {
                return new List<string>
                {
                    "Corrientes",
                    "Goya",
                    "Mercedes",
                    "Curuzú Cuatiá",
                    "Esquina",
                    "Ituzaingó",
                    "Paso de los Libres",
                    "Santo Tomé",
                    "Monte Caseros",
                    "Bella Vista"
                };
            }
            else if (province == "Chaco")
            {
                return new List<string>
                {
                    "Resistencia",
                    "Roque Sáenz Peña",
                    "Villa Ángela",
                    "Charata",
                    "Juan José Castelli",
                    "General José de San Martín",
                    "Barranqueras"
                };
            }
            else if (province == "Misiones")
            {
                return new List<string>
                {
                    "Posadas",
                    "Puerto Iguazú",
                    "Eldorado",
                    "Oberá",
                    "Apostoles",
                    "San Vicente",
                    "Garupá",
                    "Montecarlo",
                    "Alem",
                    "Jardín América"
                };
            }
            else
            {
                return new List<string>();
            }
        }
        private async Task CreateClient()
        {
            var clientDto = new ClientDto()
            {
                FirstName = firstName, 
                LastName = lastName,
                Location = $"{province}, {city}, {address}",
                PhoneNumber = phoneNumber,
                DocumentType = documentType,
                DocumentNumber = documentNumber,
                Email = email,
            };
            string clientDtoJson = JsonConvert.SerializeObject(clientDto);
            AndroidHttpClientService httpClientService = new AndroidHttpClientService();
            HttpClient httpClient = httpClientService.GetInsecureHttpClient();

            using (httpClient)
            {
                // Set the base address of your API
                httpClient.BaseAddress = new Uri("https://10.0.2.2:7215/api/clients");

                // Specify the endpoint for adding clients
                

                // Set the content type and content
                var content = new StringContent(clientDtoJson, Encoding.UTF8, "application/json");

                // Send the POST request
                var response = await httpClient.PostAsync(httpClient.BaseAddress, content);

                if (response.IsSuccessStatusCode)
                {
                    
                    NavManager.NavigateTo("/clientslist");
                }
                else
                {
                    // Handle the error, e.g., log it or show an error message.
                }
            }
        }
        
    }
}