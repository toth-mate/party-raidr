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

            if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Debug.WriteLine("FAIL: Unauthorized!!!");
                await Shell.Current.GoToAsync("/profile");
            }

            return response;
        }
    }
}
