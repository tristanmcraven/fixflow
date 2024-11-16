using fixflow.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace fixflow.Utility
{
    public static class ApiClient
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static readonly string apiPath = "http://localhost:5108/api/";

        private static async Task<T?> SendRequest<T>(string url, HttpMethod httpMethod, object? body = null)
        {
            using var request = new HttpRequestMessage(httpMethod, apiPath + url);
            if (body != null)
            {
                var jsonContent = JsonConvert.SerializeObject(body);
                request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            }
            var response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode) return default;

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonResponse);
        }

        private static async Task<bool> SendRequest(string url, HttpMethod httpMethod, object? body = null)
        {
            using var request = new HttpRequestMessage(httpMethod, apiPath + url);
            if (body != null)
            {
                var jsonContent = JsonConvert.SerializeObject(body);
                request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            }

            var response = await httpClient.SendAsync(request);

            return response.IsSuccessStatusCode;
        }

        public static class DeviceBrand
        {
            public static async Task<List<Model.DeviceBrand>> Get() => await SendRequest<List<Model.DeviceBrand>>("devicebrand", HttpMethod.Get);
            public static async Task<Model.DeviceBrand> GetByName(string name) => await SendRequest<Model.DeviceBrand>($"devicebrand/{name}", HttpMethod.Get);

            public static async Task<List<Model.DeviceModel>> GetModelsByName(string name) => await SendRequest<List<Model.DeviceModel>>($"devicebrand/{name}/models", HttpMethod.Get);

            public static async Task<bool> Post(string name)
            {
                var dto = new
                {
                    Name = name
                };
                return await SendRequest($"devicebrand", HttpMethod.Post, dto);
            }
        }

        public static class DeviceModel
        {
            public static async Task<List<Model.DeviceModel>> Get() => await SendRequest<List<Model.DeviceModel>>("devicemodel", HttpMethod.Get);

            public static async Task<Model.DeviceModel> GetByName(string name) => await SendRequest<Model.DeviceModel>($"devicemodel/{name}", HttpMethod.Get);

            public static async Task<bool> Post(uint deviceBrandId, string name)
            {
                var dto = new
                {
                    DeviceBrandId = deviceBrandId,
                    Name = name
                };
                return await SendRequest($"devicemodel", HttpMethod.Post, dto);
            }
        }

        public static class Status
        {
            public static async Task<List<Model.Status>> Get() => await SendRequest<List<Model.Status>>("status", HttpMethod.Get);

            public static async Task<Model.Status> GetByName(string name) => await SendRequest<Model.Status>($"status/{name}", HttpMethod.Get);
        }
    }
}
