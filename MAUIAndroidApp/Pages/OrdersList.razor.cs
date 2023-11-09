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
using Contracts.ViewModels;
using Contracts.Utils;

namespace MAUIAndroidApp.Pages
{
    public partial class OrdersList
    {
    private List<OrderViewModel> orders;
    protected override async Task OnInitializedAsync()
    {
      //var deb = await ApiHelper.GetListAsync<Client>($"{ApiUrl.AzureUrl}clients");

      // AZURE
      var response = await ApiHelper.GetListAsync<OrderViewModel>($"{ApiUrl.AzureUrl}orders/getSellerOrders/{AppData.SellerId}");
      if (response == null)
      {
        //TODO:

      }
      else
      {
        orders = response.OrderByDescending(o => o.Date).ToList();
      }
      //

    }
  }
}
