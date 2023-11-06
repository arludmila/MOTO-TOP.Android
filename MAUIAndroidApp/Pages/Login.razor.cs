using Android.Locations;
using Contracts.DTOs;
using Contracts.DTOs.Entities;
using Newtonsoft.Json;
using System.Text;

namespace MAUIAndroidApp.Pages
{
  public partial class Login
  {
    private string email;
    private string password;

    private async Task LoginRequest()
    {
      var loginRequestDto = new LoginRequestDto()
      {
        Email = email,
        Password = password
      };
      
      string loginRequestDtoJson = JsonConvert.SerializeObject(loginRequestDto);
      AndroidHttpClientService httpClientService = new AndroidHttpClientService();
      HttpClient httpClient = httpClientService.GetInsecureHttpClient();

      using (httpClient)
      {
        // Set the base address of your API
        httpClient.BaseAddress = new Uri("https://10.0.2.2:7215/api/login/seller");

        // Specify the endpoint for adding clients


        // Set the content type and content
        var content = new StringContent(loginRequestDtoJson, Encoding.UTF8, "application/json");

        // Send the POST request
        var response = await httpClient.PostAsync(httpClient.BaseAddress, content);

        if (response.IsSuccessStatusCode)
        {

          var id = Convert.ToInt32(await response.Content.ReadAsStringAsync());
          if (id == 0)
          {
            return;
          }
          else
          {
            // TODO: guardar el ID en algun lugar!!!
            AppData.SellerId = id;
            NavManager.NavigateTo("/index");
          }
          
        }
        else
        {
          // TODO:??????????????????????
          throw new Exception();
        }
      }
    }
  }
}
