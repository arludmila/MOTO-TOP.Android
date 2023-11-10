
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
      loadingSpinnerDisplay = true;
      StateHasChanged();

      await SendLoginRequest();

      loadingSpinnerDisplay = false;
      StateHasChanged();
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
        // TODO: Handle error
      }
      else
      {
        var id = Convert.ToInt32(response);
        if (id == 0)
        {
          // TODO: Handle the case where login failed
          return;
        }
        else
        {
          AppData.SellerId = id;
          NavManager.NavigateTo("/index");
        }
      }
    }
  }
}
