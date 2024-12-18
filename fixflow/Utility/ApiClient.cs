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
using System.Xml.Linq;

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
            public static async Task<List<Model.DeviceBrand>> Get()
            {
                if (App.OfflineMode)
                {
                    return App.Backup.DeviceBrands;
                }
                return await SendRequest<List<Model.DeviceBrand>>("devicebrand", HttpMethod.Get);
            }

            public static async Task<Model.DeviceBrand> GetById(Guid id)
            {
                if (App.OfflineMode)
                {
                    return App.Backup.DeviceBrands.Where(db => db.Guid.Equals(id)).FirstOrDefault();
                }
                return await SendRequest<Model.DeviceBrand>($"devicebrand/{id}", HttpMethod.Get);
            }

            public static async Task<Model.DeviceBrand> GetByName(string name)
            {
                if (App.OfflineMode)
                {
                    return App.Backup.DeviceBrands.Where(db => db.Name.Equals(name)).FirstOrDefault();
                }
                return await SendRequest<Model.DeviceBrand>($"devicebrand/{name}", HttpMethod.Get);
            }

            public static async Task<List<Model.DeviceModel>> GetModelsByName(string name)
            {
                if (App.OfflineMode)
                {
                    var brand = await GetByName(name);
                    return App.Backup.DeviceModels.Where(dm => dm.DeviceBrandGuid.Equals(brand.Guid)).ToList();
                }
                return await SendRequest<List<Model.DeviceModel>>($"devicebrand/{name}/models", HttpMethod.Get);
            }

            public static async Task<bool> Post(string name)
            {
                if (App.OfflineMode)
                {
                    App.Backup.DeviceBrands.Add(new Model.DeviceBrand(name));
                    return true;
                }
                var dto = new
                {
                    Name = name
                };
                return await SendRequest($"devicebrand", HttpMethod.Post, dto);
            }
        }

        public static class DeviceModel
        {
            public static async Task<List<Model.DeviceModel>> Get()
            {
                if (App.OfflineMode)
                {
                    return App.Backup.DeviceModels;
                }
                return await SendRequest<List<Model.DeviceModel>>("devicemodel", HttpMethod.Get);
            }

            public static async Task<Model.DeviceModel> GetById(Guid id)
            {
                if (App.OfflineMode)
                {
                    return App.Backup.DeviceModels.Where(dm => dm.Guid.Equals(id)).FirstOrDefault();
                }
                return await SendRequest<Model.DeviceModel>($"devicemodel/{id}", HttpMethod.Get);
            }

            public static async Task<Model.DeviceModel> GetByName(string name)
            {
                if (App.OfflineMode)
                {
                    return App.Backup.DeviceModels.Where(dm => dm.Name.Equals(name)).FirstOrDefault();
                }
                return await SendRequest<Model.DeviceModel>($"devicemodel/{name}", HttpMethod.Get);
            }

            public static async Task<bool> Post(Guid deviceBrandId, string name)
            {
                if (App.OfflineMode)
                {
                    App.Backup.DeviceModels.Add(new Model.DeviceModel(deviceBrandId, name));
                    return true;
                }
                var dto = new
                {
                    DeviceBrandId = deviceBrandId,
                    Name = name
                };
                return await SendRequest($"devicemodel", HttpMethod.Post, dto);
            }
        }

        public static class DeviceType
        {
            public static async Task<List<Model.DeviceType>> Get()
            {
                if (App.OfflineMode)
                {
                    return App.Backup.DeviceTypes;
                }
                return await SendRequest<List<Model.DeviceType>>("devicetype", HttpMethod.Get);
            }

            public static async Task<bool> Post(string name)
            {
                if (App.OfflineMode)
                {
                    App.Backup.DeviceTypes.Add(new Model.DeviceType(name));
                    return true;
                }
                return await SendRequest($"devicetype/{name}", HttpMethod.Post);
            }

            public static async Task<Model.DeviceType> GetByName(string name)
            {
                if (App.OfflineMode)
                {
                    return App.Backup.DeviceTypes.FirstOrDefault(dt => dt.Name.Equals(name));
                }
                return await SendRequest<Model.DeviceType>($"devicetype/{name}", HttpMethod.Get);
            }

            public static async Task<Model.DeviceType> GetById(Guid id)
            {
                if (App.OfflineMode)
                {
                    return App.Backup.DeviceTypes.FirstOrDefault(x => x.Guid.Equals(id));
                }
                return await SendRequest<Model.DeviceType>($"devicetype/{id}", HttpMethod.Get);
            }
        }

        public static class Status
        {
            public static async Task<List<Model.Status>> Get()
            {
                if (App.OfflineMode)
                {
                    return App.Backup.Statuses;
                }
                return await SendRequest<List<Model.Status>>("status", HttpMethod.Get);
            }

            public static async Task<Model.Status> GetById(Guid id)
            {
                if (App.OfflineMode)
                {
                    return App.Backup.Statuses.FirstOrDefault(s => s.Guid.Equals(id));
                }
                return await SendRequest<Model.Status>($"status/{id}", HttpMethod.Get);
            }

            public static async Task<Model.Status> GetByName(string name)
            {
                if (App.OfflineMode)
                {
                    return App.Backup.Statuses.FirstOrDefault(s => s.Name.Equals(name));
                }
                return await SendRequest<Model.Status>($"status/{name}", HttpMethod.Get);
            }
        }

        public static class Ticket
        {
            public static async Task<List<Model.Ticket>> Get()
            {
                if (App.OfflineMode)
                {
                    return App.Backup.Tickets;
                }
                return await SendRequest<List<Model.Ticket>>("ticket", HttpMethod.Get);
            }

            public static async Task<Model.Ticket> GetById(Guid id)
            {
                if (App.OfflineMode)
                {
                    return App.Backup.Tickets.FirstOrDefault(t => t.Guid.Equals(id));
                }
                return await SendRequest<Model.Ticket>($"ticket/{id}", HttpMethod.Get);
            }

            public static async Task<List<Model.TicketKit>> GetKits(Guid id)
            {
                if (App.OfflineMode)
                {
                    return App.Backup.TicketKits.Where(tk => tk.TicketGuid.Equals(id)).ToList();
                }
                return await SendRequest<List<Model.TicketKit>>($"ticket/{id}/kits", HttpMethod.Get);
            }

            public static async Task<List<Model.TicketStatus>> GetStatuses(Guid id)
            {
                if (App.OfflineMode)
                {
                    return App.Backup.TicketStatuses.Where(tk => tk.TicketGuid.Equals(id)).ToList();
                }
                return await SendRequest<List<Model.TicketStatus>>($"ticket/{id}/statuses", HttpMethod.Get);
            }

            public static async Task<List<Model.TicketMalfunction>> GetMalfunctions(Guid id)
            {
                if (App.OfflineMode)
                {
                    return App.Backup.TicketMalfunctions.Where(tk => tk.TicketGuid.Equals(id)).ToList();
                }
                return await SendRequest<List<Model.TicketMalfunction>>($"ticket/{id}/malfunctions", HttpMethod.Get);
            }

            public static async Task<List<Model.TicketRepair>> GetRepairs(Guid id)
            {
                if (App.OfflineMode)
                {
                    return App.Backup.TicketRepairs.Where(tk => tk.TicketGuid.Equals(id)).ToList();
                }
                return await SendRequest<List<Model.TicketRepair>>($"ticket/{id}/repairs", HttpMethod.Get);
            }

            public static async Task<Model.Ticket> Post(Guid deviceBrandId, Guid deviceModelId, Guid deviceTypeId, string? clientName, string? clientPhone, string? note, string? description)
            {
                if (App.OfflineMode)
                {
                    var ticket = new Model.Ticket(deviceBrandId,
                                                  deviceModelId,
                                                  deviceTypeId,
                                                  clientName,
                                                  clientPhone,
                                                  DateTime.Now,
                                                  note,
                                                  description);
                    App.Backup.Tickets.Add(ticket);
                    return ticket;
                }
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
                if (App.OfflineMode)
                {
                    var ticket = App.Backup.Tickets.FirstOrDefault(t => t.Guid.Equals(ticketId));
                    ticket.Note = note;
                    var index = App.Backup.Tickets.FindIndex(t=> t.Guid.Equals(ticketId));
                    App.Backup.Tickets[index] = ticket;
                    return ticket;
                }
                return await SendRequest<Model.Ticket>($"ticket?id={ticketId}&note={note}", HttpMethod.Put);
            }

            public static async Task<bool> ChangeClientName(Guid ticketId, string name)
            {
                if (App.OfflineMode)
                {
                    var ticket = App.Backup.Tickets.FirstOrDefault(t => t.Guid.Equals(ticketId));
                    ticket.ClientFullname = name;
                    var index = App.Backup.Tickets.FindIndex(t => t.Guid.Equals(ticketId));
                    App.Backup.Tickets[index] = ticket;
                    return true;
                }
                var dto = new
                {
                    TicketId = ticketId,
                    Name = name
                };
                return await SendRequest($"ticket/{ticketId}/changeclientname", HttpMethod.Put, dto);
            }
            public static async Task<bool> ChangeClientPhone(Guid ticketId, string phone)
            {
                if (App.OfflineMode)
                {
                    var ticket = App.Backup.Tickets.FirstOrDefault(t => t.Guid.Equals(ticketId));
                    ticket.ClientPhoneNumber = phone;
                    var index = App.Backup.Tickets.FindIndex(t => t.Guid.Equals(ticketId));
                    App.Backup.Tickets[index] = ticket;
                    return true;
                }
                var dto = new
                {
                    TicketId = ticketId,
                    Phone = phone
                };
                return await SendRequest($"ticket/{ticketId}/changeclientphone", HttpMethod.Put, dto);
            }
            public static async Task<bool> ChangeDeviceBrand(Guid ticketId, Guid deviceBrandId)
            {
                if (App.OfflineMode)
                {
                    var ticket = App.Backup.Tickets.FirstOrDefault(t => t.Guid.Equals(ticketId));
                    ticket.DeviceBrandGuid = deviceBrandId;
                    var index = App.Backup.Tickets.FindIndex(t => t.Guid.Equals(ticketId));
                    App.Backup.Tickets[index] = ticket;
                    return true;
                }
                var dto = new
                {
                    TicketId = ticketId,
                    DeviceBrandId = deviceBrandId
                };
                return await SendRequest($"ticket/{ticketId}/changedevicebrand", HttpMethod.Put, dto);
            }
            public static async Task<bool> ChangeDeviceModel(Guid ticketId, Guid deviceModelId)
            {
                if (App.OfflineMode)
                {
                    var ticket = App.Backup.Tickets.FirstOrDefault(t => t.Guid.Equals(ticketId));
                    ticket.DeviceModelGuid = deviceModelId;
                    var index = App.Backup.Tickets.FindIndex(t => t.Guid.Equals(ticketId));
                    App.Backup.Tickets[index] = ticket;
                    return true;
                }
                var dto = new
                {
                    TicketId = ticketId,
                    DeviceModelId = deviceModelId
                };
                return await SendRequest($"ticket/{ticketId}/changedevicemodel", HttpMethod.Put, dto);
            }

            public static async Task<bool> ChangeDeviceType(Guid ticketId, Guid deviceTypeId)
            {
                if (App.OfflineMode)
                {
                    var ticket = App.Backup.Tickets.FirstOrDefault(t => t.Guid.Equals(ticketId));
                    ticket.DeviceTypeGuid = deviceTypeId;
                    var index = App.Backup.Tickets.FindIndex(t => t.Guid.Equals(ticketId));
                    App.Backup.Tickets[index] = ticket;
                    return true;
                }
                var dto = new
                {
                    TicketId = ticketId,
                    DeviceTypeId = deviceTypeId
                };
                return await SendRequest($"ticket/{ticketId}/changedevicetype", HttpMethod.Put, dto);
            }

            public static async Task<bool> Delete(Guid id)
            {
                if (App.OfflineMode)
                {
                    var ticket = App.Backup.Tickets.FirstOrDefault(t => t.Guid.Equals(id));
                    App.Backup.Tickets.Remove(ticket);
                    return true;
                }
                return await SendRequest($"ticket/{id}", HttpMethod.Delete);
            }
        }

        public static class TicketKit
        {
            public static async Task<List<Model.TicketKit>> Get()
            {
                if (App.OfflineMode)
                {
                    return App.Backup.TicketKits;
                }
                return await SendRequest<List<Model.TicketKit>>($"ticketkit", HttpMethod.Get);
            }

            public static async Task<Model.TicketKit?> GetById(Guid id)
            {
                if (App.OfflineMode)
                {
                    return App.Backup.TicketKits.FirstOrDefault(tk => tk.Guid.Equals(id));
                }
                return await SendRequest<Model.TicketKit>($"ticketkit/{id}", HttpMethod.Get);
            }

            public static async Task<bool> Post(Guid ticketId, string name)
            {
                if (App.OfflineMode)
                {
                    App.Backup.TicketKits.Add(new Model.TicketKit(ticketId, name));
                    return true;
                }
                var dto = new
                {
                    TicketId = ticketId,
                    Name = name
                };
                return await SendRequest($"ticketkit", HttpMethod.Post, dto);
            }

            public static async Task<bool> Put(Guid ticketKitid, string name)
            {
                if (App.OfflineMode)
                {
                    var ticketkit = App.Backup.TicketKits.FirstOrDefault(tk => tk.Guid.Equals(ticketKitid));
                    ticketkit.Name = name;
                    var index = App.Backup.TicketKits.FindIndex(tk => tk.Guid.Equals(ticketKitid));
                    App.Backup.TicketKits[index] = ticketkit;
                    return true;
                }
                var dto = new
                {
                    TicketKitId = ticketKitid,
                    Name = name
                };
                return await SendRequest($"ticketkit", HttpMethod.Put, dto);
            }

            public static async Task<bool> Delete(Guid ticketKitId)
            {
                if (App.OfflineMode)
                {
                    var ticketKit = App.Backup.TicketKits.FirstOrDefault(tk => tk.Guid.Equals(ticketKitId));
                    App.Backup.TicketKits.Remove(ticketKit);
                    return true;
                }
                return await SendRequest($"ticketkit/{ticketKitId}", HttpMethod.Delete);
            }
        }

        public static class TicketMalfunction
        {

            public static async Task<List<Model.TicketMalfunction>> Get()
            {
                if (App.OfflineMode)
                {
                    return App.Backup.TicketMalfunctions;
                }
                return await SendRequest<List<Model.TicketMalfunction>>("ticketmalfunction", HttpMethod.Get);
            }

            public static async Task<bool> Post(Guid ticketId, string name)
            {
                if (App.OfflineMode)
                {
                    App.Backup.TicketMalfunctions.Add(new Model.TicketMalfunction(ticketId, name));
                    return true;
                }
                var dto = new
                {
                    TicketId = ticketId,
                    Name = name
                };
                return await SendRequest($"ticketmalfunction", HttpMethod.Post, dto);
            }

            public static async Task<bool> Delete(Guid ticketMalfId)
            {
                if (App.OfflineMode)
                {
                    var ticketMalf = App.Backup.TicketMalfunctions.FirstOrDefault(tm => tm.Guid.Equals(ticketMalfId));
                    App.Backup.TicketMalfunctions.Remove(ticketMalf);
                    return true;
                }
                return await SendRequest($"ticketmalfunction/{ticketMalfId}", HttpMethod.Delete);
            }

            public static async Task<bool> Put(Guid ticketMalfId, string name)
            {
                if (App.OfflineMode)
                {
                    var ticketMalf = App.Backup.TicketMalfunctions.FirstOrDefault(tm => tm.Guid.Equals(ticketMalfId));
                    ticketMalf.Name = name;
                    var index = App.Backup.TicketMalfunctions.FindIndex(tm => tm.Guid.Equals(ticketMalfId));
                    App.Backup.TicketMalfunctions[index] = ticketMalf;
                    return true;
                }
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
            public static async Task<List<Model.TicketStatus>> Get()
            {
                if (App.OfflineMode)
                {
                    return App.Backup.TicketStatuses;
                }
                return await SendRequest<List<Model.TicketStatus>>("ticketstatus", HttpMethod.Get);
            }

            public static async Task<bool> Post(Guid ticketId, Guid statusId)
            {
                if (App.OfflineMode)
                {
                    App.Backup.TicketStatuses.Add(new Model.TicketStatus(ticketId, statusId));
                    return true;
                }
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
            public static async Task<List<Model.TicketRepair>> Get()
            {
                if (App.OfflineMode)
                {
                    return App.Backup.TicketRepairs;
                }
                return await SendRequest<List<Model.TicketRepair>>("ticketrepair", HttpMethod.Get);
            }

            public static async Task<bool> Post(Guid ticketId, Guid repairId, int price)
            {
                if (App.OfflineMode)
                {
                    App.Backup.TicketRepairs.Add(new Model.TicketRepair(ticketId, repairId, (uint)price));
                    return true;
                }
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
            public static async Task<List<Model.Repair>> Get()
            {
                if (App.OfflineMode)
                {
                    return App.Backup.Repairs;
                }
                return await SendRequest<List<Model.Repair>>("repair", HttpMethod.Get);
            }

            public static async Task<Model.Repair?> GetByName(string name)
            {
                if (App.OfflineMode)
                {
                    return App.Backup.Repairs.FirstOrDefault(x => x.Name.Equals(name));
                }
                var escapedName = Uri.EscapeDataString(name);
                return await SendRequest<Model.Repair>($"repair/{name}", HttpMethod.Get);
            }

            public static async Task<Model.Repair?> GetById(Guid id)
            {
                if (App.OfflineMode)
                {
                    return App.Backup.Repairs.FirstOrDefault(r => r.Guid.Equals(id));
                }
                return await SendRequest<Model.Repair>($"repair/{id}", HttpMethod.Get);
            }

            public static async Task<Model.Repair> Post(string name)
            {
                if (App.OfflineMode)
                {
                    var repair = new Model.Repair(name);
                    App.Backup.Repairs.Add(repair);
                    return repair;
                }
                return await SendRequest<Model.Repair>($"repair/{name}", HttpMethod.Post);
            }
                    
        }

        public static class Sync
        {
            public static async Task SyncData(Backup backup)
            {

            }

            private static async Task Delete(Backup backup)
            {
                var dbTickets = await Ticket.Get();
                var backupTicketIds = App.Backup.Tickets.Select(t => t.Guid).ToHashSet();

                foreach (var dbTicket in  dbTickets)
                {
                    if (!backupTicketIds.Contains(dbTicket.Guid))
                    {
                        await Ticket.Delete(dbTicket.Guid);
                    }
                }
            }

            private static async Task Post(Backup backup)
            {
                var brands = App.Backup.DeviceBrands;
                foreach (var brand in brands)
                {
                    var existingBrand = await DeviceBrand.GetByName(brand.Name);
                    if (existingBrand == null)
                    {
                        await DeviceBrand.Post(brand.Name);
                    }
                }

                var models = App.Backup.DeviceModels;
                foreach (var model in models)
                {
                    var existingModel = await DeviceModel.GetByName(model.Name);
                    if (existingModel == null)
                    {
                        await DeviceModel.Post(model.Guid, model.Name);
                    }
                }

                var types = App.Backup.DeviceTypes;
                foreach (var type in types)
                {
                    var existingBrand = await DeviceType.GetByName(type.Name);
                    if (existingBrand == null)
                    {
                        await DeviceType.Post(type.Name);
                    }
                }

                var repairs = App.Backup.Repairs;
                foreach (var repair in repairs)
                {
                    var existingRepair = await Repair.GetByName(repair.Name);
                    if (existingRepair == null)
                    {
                        await Repair.Post(repair.Name);
                    }
                }

                var tickets = App.Backup.Tickets;
                foreach (var ticket in tickets)
                {
                    var existingTicket = await Ticket.GetById(ticket.Guid);
                    if (existingTicket == null)
                    {
                        await Ticket.Post();
                    }
                }

                var ticketKits = App.Backup.TicketKits;
                foreach (var ticketKit in ticketKits)
                {
                    var existingTicketKit = await TicketKit.GetById
                }
            }
        }
    }
}
