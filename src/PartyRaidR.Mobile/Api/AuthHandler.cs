using CommunityToolkit.Maui.Alerts;
using System.Diagnostics;

namespace PartyRaidR.Mobile.Api
{
    public class AuthHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string? token = await SecureStorage.GetAsync("access_token");

            if (!string.IsNullOrEmpty(token))
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await base.SendAsync(request, cancellationToken);
            //Debug.WriteLine("wdadaw " + await response.Content.ReadAsStringAsync());

            if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Debug.WriteLine("FAIL: Unauthorized!!!");
                await Shell.Current.GoToAsync("/profile");
            }
            else if (!response.IsSuccessStatusCode)
            {
                var snackbar = Snackbar.Make(await response.Content.ReadAsStringAsync(), visualOptions: new()
                {
                    BackgroundColor = Color.FromRgb(220, 53, 69),
                    TextColor = Color.FromRgb(255, 255, 255),
                    ActionButtonTextColor = Color.FromRgb(255, 255, 255)
                });
                await snackbar.Show();
            }

            return response;
        }
    }
}
