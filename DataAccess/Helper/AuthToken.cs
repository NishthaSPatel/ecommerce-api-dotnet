using System.Security.Claims;
using System.Text;
using DataAccess.Models.Helper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#nullable disable

namespace DataAccess.Helper
{
    public static class AuthToken
    {
        public static async Task<string> GetToken(IConfiguration _configuration, HttpClient _client)
        {
            AuthLogin authLogin = new();
            if (_configuration["Auth0:Domain"] == "corefunctionality.us.auth0.com")
            {
                authLogin.grant_type = _configuration["Auth0:Prod_AuthGrantType"];
                authLogin.client_id = _configuration["Auth0:Prod_AuthClientId"];
                authLogin.client_secret = _configuration["Auth0:Prod_AuthClientSecret"];
                authLogin.audience = _configuration["Auth0:Audience"];
            }
            else
            {
                authLogin.grant_type = _configuration["Auth0:AuthGrantType"];
                authLogin.client_id = _configuration["Auth0:AuthClientId"];
                authLogin.client_secret = _configuration["Auth0:AuthClientSecret"];
                authLogin.audience = _configuration["Auth0:Audience"];
            }

            var request = JsonConvert.SerializeObject(authLogin);
            var requestContent = new StringContent(request, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"https://{_configuration["Auth0:Domain"]}/oauth/token", requestContent);

            var accessToken = JObject.Parse(response.Content.ReadAsStringAsync().Result);

            return accessToken["access_token"].ToString();
        }

        public static string GetUserName(this ClaimsPrincipal user, string claimType)
        {
            return user.Claims.FirstOrDefault(x => x.Type == claimType)?.Value;
        }
    }
}