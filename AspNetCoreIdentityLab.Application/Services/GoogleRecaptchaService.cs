using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspNetCoreIdentityLab.Application.Services
{
    public class GoogleRecaptchaService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;

        public GoogleRecaptchaService(IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            _configuration = configuration;
            _clientFactory = clientFactory;
        }

        public async Task<bool> recaptchaIsValid(string remoteIpAddress, string recaptchaResponse)
        {
            var client = _clientFactory.CreateClient();

            try
            {
                var parameters = new Dictionary<string, string>
                {
                    {"secret", _configuration["reCAPTCHA:SecretKey"]},
                    {"response", recaptchaResponse},
                    {"remoteip", remoteIpAddress}
                };

                HttpResponseMessage response = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", new FormUrlEncodedContent(parameters));
                response.EnsureSuccessStatusCode();

                string apiResponse = await response.Content.ReadAsStringAsync();
                dynamic apiJson = JObject.Parse(apiResponse);
                if (apiJson.success != true)
                {
                    return false;
                }
            }
            catch (HttpRequestException ex)
            {
                // Something went wrong with the API. Let the request through.
                throw ex;
            }

            return true;
        }
    }
}
