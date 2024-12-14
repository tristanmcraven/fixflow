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

            public static async Task<Model.DeviceBrand> GetById(Guid id) => await SendRequest<Model.DeviceBrand>($"devicebrand/{id}", HttpMethod.Get);
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

            public static async Task<Model.DeviceModel> GetById(Guid id) => await SendRequest<Model.DeviceModel>($"devicemodel/{id}", HttpMethod.Get);
            public static async Task<Model.DeviceModel> GetByName(string name) => await SendRequest<Model.DeviceModel>($"devicemodel/{name}", HttpMethod.Get);

            public static async Task<bool> Post(Guid deviceBrandId, string name)
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

            public static async Task<Model.Status> GetById(Guid id) => await SendRequest<Model.Status>($"status/{id}", HttpMethod.Get);
            public static async Task<Model.Status> GetByName(string name) => await SendRequest<Model.Status>($"status/{name}", HttpMethod.Get);
        }

        public static class Ticket
        {
            public static async Task<List<Model.Ticket>> Get() => await SendRequest<List<Model.Ticket>>("ticket", HttpMethod.Get);

            public static async Task<Model.Ticket> GetById(Guid id) => await SendRequest<Model.Ticket>($"ticket/{id}", HttpMethod.Get);
            public static async Task<List<Model.TicketKit>> GetKits(Guid id) => await SendRequest<List<Model.TicketKit>>($"ticket/{id}/kits", HttpMethod.Get);
            public static async Task<List<Model.TicketStatus>> GetStatuses(Guid id) => await SendRequest<List<Model.TicketStatus>>($"ticket/{id}/statuses", HttpMethod.Get);
            public static async Task<List<Model.TicketMalfunction>> GetMalfunctions(Guid id) => await SendRequest<List<Model.TicketMalfunction>>($"ticket/{id}/malfunctions", HttpMethod.Get);
            public static async Task<List<Model.TicketRepair>> GetRepairs(Guid id) => await SendRequest<List<Model.TicketRepair>>($"ticket/{id}/repairs", HttpMethod.Get);

            public static async Task<Model.Ticket> Post(Guid deviceBrandId, Guid deviceModelId, Guid deviceTypeId, string? clientName, string? clientPhone, string? note, string? description)
            {
                var dto = new
                {
                    DeviceBrandId = deviceBrandId,
                    DeviceModelId = deviceModelId,
                    deviceTypeId = deviceTypeId,
                    ClientFullname = clientName,
                    ClientPhoneNumber = clientPhone,
                    Timestamp = DateTime.Now,
                    Note = note,
                    Description = description
                };
                return await SendRequest<Model.Ticket>($"ticket", HttpMethod.Post, dto);
            }

            public static async Task<Model.Ticket> Put(Guid ticketId, string note)
            {
                return await SendRequest<Model.Ticket>($"ticket?id={ticketId}&note={note}", HttpMethod.Put);
            }

            public static async Task<bool> ChangeClientName(Guid ticketId, string name)
            {
                var dto = new
                {
                    TicketId = ticketId,
                    Name = name
                };
                return await SendRequest($"ticket/{ticketId}/changeclientname", HttpMethod.Put, dto);
            }
            public static async Task<bool> ChangeClientPhone(Guid ticketId, string phone)
            {
                var dto = new
                {
                    TicketId = ticketId,
                    Phone = phone
                };
                return await SendRequest($"ticket/{ticketId}/changeclientphone", HttpMethod.Put, dto);
            }
            public static async Task<bool> ChangeDeviceBrand(Guid ticketId, Guid deviceBrandId)
            {
                var dto = new
                {
                    TicketId = ticketId,
                    DeviceBrandId = deviceBrandId
                };
                return await SendRequest($"ticket/{ticketId}/changedevicebrand", HttpMethod.Put, dto);
            }
            public static async Task<bool> ChangeDeviceModel(Guid ticketId, Guid deviceModelId)
            {
                var dto = new
                {
                    TicketId = ticketId,
                    DeviceModelId = deviceModelId
                };
                return await SendRequest($"ticket/{ticketId}/changedevicemodel", HttpMethod.Put, dto);
            }
        }

        public static class TicketKit
        {
            public static async Task<List<Model.TicketKit>> Get() => await SendRequest<List<Model.TicketKit>>($"ticketkit", HttpMethod.Get);
            public static async Task<bool> Post(Guid ticketId, string name)
            {
                var dto = new
                {
                    TicketId = ticketId,
                    Name = name
                };
                return await SendRequest($"ticketkit", HttpMethod.Post, dto);
            }

            public static async Task<bool> Put(Guid ticketKitid, string name)
            {
                var dto = new
                {
                    TicketKitId = ticketKitid,
                    Name = name
                };
                return await SendRequest($"ticketkit", HttpMethod.Put, dto);
            }

            public static async Task<bool> Delete(Guid ticketKitId) => await SendRequest($"ticketkit/{ticketKitId}", HttpMethod.Delete);
        }

        public static class TicketMalfunction
        {

            public static async Task<List<Model.TicketMalfunction>> Get() => await SendRequest<List<Model.TicketMalfunction>>("ticketmalfunction", HttpMethod.Get);
            public static async Task<bool> Post(Guid ticketId, string name)
            {
                var dto = new
                {
                    TicketId = ticketId,
                    Name = name
                };
                return await SendRequest($"ticketmalfunction", HttpMethod.Post, dto);
            }

            public static async Task<bool> Delete(Guid ticketMalfId) => await SendRequest($"ticketmalfunction/{ticketMalfId}", HttpMethod.Delete);

            public static async Task<bool> Put(Guid ticketMalfId, string name)
            {
                var dto = new
                {
                    Id = ticketMalfId,
                    Name = name
                };
                return await SendRequest($"ticketmalfunction", HttpMethod.Put, dto);
            }
        }

        public static class TicketStatus
        {
            public static async Task<List<Model.TicketStatus>> Get() => await SendRequest<List<Model.TicketStatus>>("ticketstatus", HttpMethod.Get);
            public static async Task<bool> Post(Guid ticketId, Guid statusId)
            {
                var dto = new
                {
                    TicketId = ticketId,
                    StatusId = statusId
                };
                return await SendRequest($"ticketstatus", HttpMethod.Post, dto);
            }
        }

        public static class TicketRepair
        {
            public static async Task<List<Model.TicketRepair>> Get() => await SendRequest<List<Model.TicketRepair>>("ticketrepair", HttpMethod.Get);
            public static async Task<bool> Post(Guid ticketId, Guid repairId, int price)
            {
                var dto = new
                {
                    TicketId = ticketId,
                    RepairId = repairId,
                    Price = price
                };
                return await SendRequest($"ticketrepair", HttpMethod.Post, dto);
            }
        }

        public static class Repair
        {
            public static async Task<List<Model.Repair>> Get() => await SendRequest<List<Model.Repair>>("repair", HttpMethod.Get);

            public static async Task<Model.Repair?> GetByName(string name)
            {
                var escapedName = Uri.EscapeDataString(name);
                return await SendRequest<Model.Repair>($"repair/name?name={name}", HttpMethod.Get);
            }

            public static async Task<Model.Repair?> GetById(Guid id) => await SendRequest<Model.Repair>($"repair/{id}", HttpMethod.Get);

            public static async Task<Model.Repair> Post(string name)
            {
                return await SendRequest<Model.Repair>($"repair/name?name={name}", HttpMethod.Post);
            }
                    
        }
    }
}
