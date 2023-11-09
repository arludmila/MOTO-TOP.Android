using Contracts.Utils;
using Contracts.ViewModels;
using Entities.Core;
using Newtonsoft.Json;

namespace MAUIAndroidApp.Pages
{
  public partial class ProductsList
  {
    private List<ProductViewModel> products;
    protected override async Task OnInitializedAsync()
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



      //AndroidHttpClientService httpClientService = new AndroidHttpClientService();
      //HttpClient httpClient = httpClientService.GetInsecureHttpClient();

      //try
      //{
      //  HttpResponseMessage response = await httpClient.GetAsync("https://10.0.2.2:7215/api/products/view-models");

      //  if (response.IsSuccessStatusCode)
      //  {
      //    string content = await response.Content.ReadAsStringAsync();
      //    products = JsonConvert.DeserializeObject<List<ProductViewModel>>(content);
      //  }
      //  else
      //  {
      //    // Handle the error, e.g., log it or show a message to the user.
      //  }
      //}
      //catch (Exception ex)
      //{
      //  // Handle the exception, e.g., log it or show a message to the user.
      //}
    }




  }
}
