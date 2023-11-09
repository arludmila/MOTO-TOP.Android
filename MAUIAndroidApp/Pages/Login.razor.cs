
using Contracts.DTOs;
using Contracts.DTOs.Entities;
using Contracts.Utils;
using Newtonsoft.Json;
using System.Text;

namespace MAUIAndroidApp.Pages
{
  public partial class Login
  {
    private string email;
    private string password;
    private bool loadingSpinnerDisplay = false;

    private async Task LoginRequest()
    {
      // Show the loading spinner
      loadingSpinnerDisplay = true;
      StateHasChanged();
      await Task.Delay(10);

      //
      await SendLoginRequest();
      loadingSpinnerDisplay = false;
      await InvokeAsync(StateHasChanged);
    }
    private async Task SendLoginRequest()
    {
      var loginRequestDto = new LoginRequestDto()
      {
        Email = email,
        Password = password
      };

      // AZURE
      var response = await ApiHelper.PostAsync($"{ApiUrl.AzureUrl}login/seller", loginRequestDto);
      if (response.Contains("error") || response.Contains("failed"))
      {
        //TODO:

      }
      else
      {
        var id = Convert.ToInt32(response);
        if (id == 0)
        {
          loadingSpinnerDisplay = false;
          StateHasChanged();
          return;
        }
        else
        {
          // TODO: guardar el ID en algun lugar!!!
          AppData.SellerId = id;
          NavManager.NavigateTo("/index");
        }
      }
    }
  }
}
