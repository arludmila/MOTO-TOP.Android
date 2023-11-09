using Contracts.Utils;
using Entities.Core;

namespace MAUIAndroidApp.Pages
{
  public partial class ClientsList
  {
    private List<Client> clients;
    protected override async Task OnInitializedAsync()
    {
      //var deb = await ApiHelper.GetListAsync<Client>($"{ApiUrl.AzureUrl}clients");


      // AZURE
      var response = await ApiHelper.GetListAsync<Client>($"{ApiUrl.AzureUrl}clients");
      if (response == null)
      {
        //TODO:

      }
      else
      {
        clients = response.OrderBy(c => c.LastName).ThenBy(c => c.FirstName).ToList();
      }
      //


      //AndroidHttpClientService httpClientService = new AndroidHttpClientService();
      //HttpClient httpClient = httpClientService.GetInsecureHttpClient();
      //try
      //{
      //    //HttpResponseMessage response = await httpClient.GetAsync($"{ApiUrl.AzureUrl}clients");

      //    HttpResponseMessage response = await httpClient.GetAsync("https://10.0.2.2:7215/api/clients");
      //    if (response.IsSuccessStatusCode)
      //    {
      //        string content = await response.Content.ReadAsStringAsync();
      //        clients = JsonConvert.DeserializeObject<List<Client>>(content);
      //    }
      //    else
      //    {
      //    // Handle the error, e.g., log it or show a message to the user.
      //    }
      //}
      //catch (Exception ex)
      //{
      //// Handle the exception, e.g., log it or show a message to the user.
      //}
    }
  }
}
