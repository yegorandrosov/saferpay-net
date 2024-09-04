using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SaferPay.Models;

namespace SaferPay
{
    public interface ISaferPayClient
    {
        Task<TResponse> SendAsync<TResponse, TRequest>(string path, TRequest request)
            where TRequest : RequestBase
            where TResponse : ResponseBase;
    }

    public class SaferPayClient : ISaferPayClient
    {
        private readonly HttpClient httpClient;
        private readonly SaferPaySettings settings;

        public SaferPayClient(HttpClient httpClient, SaferPaySettings settings)
        {
            this.httpClient = httpClient;
            this.settings = settings;
        }

        protected virtual string GenerateRequestId() => Guid.NewGuid().ToString("n");

        protected virtual RequestHeader CreateRequestHeader() => new RequestHeader
        {
            CustomerId = settings.CustomerId,
            SpecVersion = SaferPayApiConstants.Version,
            RequestId = GenerateRequestId(),
            RetryIndicator = 0
        };

        public virtual async Task<TResponse> SendAsync<TResponse, TRequest>(string path, TRequest request)
            where TRequest : RequestBase
            where TResponse : ResponseBase
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            
            request.RequestHeader = CreateRequestHeader();

            var jsonPayload = JsonConvert.SerializeObject(request);
            var baseUri = settings.Environment == ESaferPayEnvironment.Test
                ? "https://test.saferpay.com/api/"
                : "https://www.saferpay.com/api";
            var endpointUri = new Uri(new Uri(baseUri), path);

            var message = new HttpRequestMessage(HttpMethod.Post, endpointUri)
            {
                Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
            };

            message.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes($"{settings.Username}:{settings.Password}")));
            message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.SendAsync(message, HttpCompletionOption.ResponseContentRead);
            var responseText = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseText);
                throw new SaferPayException(response.StatusCode, errorResponse);
            }

            return JsonConvert.DeserializeObject<TResponse>(responseText);
        }
    }
}