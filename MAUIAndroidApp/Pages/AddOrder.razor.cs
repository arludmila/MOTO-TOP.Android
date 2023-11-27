  using Contracts.DTOs.Entities;
  using Contracts.DTOs.Relationships;
  using Contracts.Utils;
  using Contracts.ViewModels;
  using Entities.Core;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
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
        // AZURE
        var response = await ApiHelper.GetListAsync<ProductViewModel>($"{ApiUrl.AzureUrl}products/view-models");
        if (response == null)
        {
            //TODO:

        }
        else
        {
          products = response;
        }
        //

      }
      protected override async Task OnInitializedAsync()
      {
        await GetClients();
        await GetProducts();
      }
      protected async Task GetClients()
      {
        // AZURE
        var response = await ApiHelper.GetListAsync<Client>($"{ApiUrl.AzureUrl}clients");
        if (response == null)
        {
          //TODO:

        }
        else
        {
            var seller = await ApiHelper.GetAsync<Seller>($"{ApiUrl.AzureUrl}sellers/{AppData.SellerId}");

            clients = response
              .Where(c => c.Location.Contains($"{seller.Zone},"))
              .OrderBy(c => c.LastName).ThenBy(c => c.FirstName).ToList();
          }
        //

         }
      private int selectedProductId;
      private string selectedProductDescription;
      private double selectedProductPrice;
      private int selectedProductStock;
      private int selectedProductQuantity;

      private List<OrderProductDto> orderLines = new List<OrderProductDto>();

      private void AddLine()
      {
        var existingLine = orderLines.FirstOrDefault(line => line.ProductId == selectedProductId);

        if (existingLine != null)
        {
          // Product already exists in orderLines, update the quantity
          existingLine.Quantity += selectedProductQuantity;
        }
        else
        {
          // Product doesn't exist in orderLines, add a new line
          var selectedProduct = products.FirstOrDefault(p => p.Id == selectedProductId);

          if (selectedProduct != null && selectedProductQuantity > 0)
          {
            orderLines.Add(new OrderProductDto
            {
              ProductId = selectedProduct.Id,
              ProductName = $"{selectedProduct.CategoryName}: {selectedProduct.Name}",
              Quantity = selectedProductQuantity,
              Price = selectedProduct.SellingPrice,
            });
          }
        }

        // Clear the selected product and quantity
        selectedProductId = 0;
        selectedProductDescription = "";
        selectedProductPrice = 0;
        selectedProductStock = 0;
        selectedProductQuantity = 0;

        StateHasChanged();
      }
      private void ClearOrderLines()
      {
        orderLines.Clear();
      }
      // 
      private async Task CreateDetailedOrder()
      {
        if (orderLines.Count() == 0)
        {
          // TODO: tirar mensaje?

          return;
        }
        var detailedOrderDto = new OrderWithDetailsDto()
        {
          ClientId = clientId,
          SellerId = AppData.SellerId,
          OrderDetails = orderLines,
          Date = DateTime.Now,
        };
        // AZURE
        var response = await ApiHelper.PostAsync($"{ApiUrl.AzureUrl}orders/detailed", detailedOrderDto);
        if (response.Contains("error") || response.Contains("failed"))
        {
          //TODO:

        }
        else
        {
          NavManager.NavigateTo("/orders");
        }
        //

      }
    private void UpdateSelectedProductDetails(ChangeEventArgs e)
    {
      var selectedOption = products.FirstOrDefault(p => p.Id == Convert.ToInt32(e.Value));
      if (selectedOption != null)
      {
        selectedProductDescription = selectedOption.Description;
        selectedProductPrice = selectedOption.SellingPrice;
        selectedProductStock = selectedOption.Quantity;
      }
    }
  }
  }
