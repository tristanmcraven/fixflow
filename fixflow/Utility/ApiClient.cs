﻿using fixflow.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using Windows.Perception.Spatial.Preview;
using System.Net;
using System.Windows;
using fixflow.Windows;
using System.Media;
using System.IO;

namespace fixflow.Utility
{
    public static class ApiClient
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static readonly string apiPath = "http://localhost:5108/api/";

        private static async Task<T?> SendRequest<T>(string url, HttpMethod httpMethod, object? body = null)
        {
            try
            {
                using var request = new HttpRequestMessage(httpMethod, apiPath + url);
                if (body != null)
                {
                    var jsonContent = JsonConvert.SerializeObject(body);
                    request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                }
                var response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    OnConnectionLost();
                    return default;
                }

                if (!response.IsSuccessStatusCode) return default;

                var jsonResponse = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(jsonResponse);
            }
            catch (Exception)
            {
                OnConnectionLost();
                return default;
            }
        }

        private static async Task<bool> SendRequest(string url, HttpMethod httpMethod, object? body = null)
        {
            try
            {
                using var request = new HttpRequestMessage(httpMethod, apiPath + url);
                if (body != null)
                {
                    var jsonContent = JsonConvert.SerializeObject(body);
                    request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                }

                var response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    OnConnectionLost();
                    return default;
                }

                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                OnConnectionLost();
                return default;
            }

        }

        private static async void OnConnectionLost()
        {
            string soundFilePath = @"C:\Windows\Media\Windows Notify System Generic.wav";
            if (File.Exists(soundFilePath))
            {
                using (SoundPlayer player = new SoundPlayer(soundFilePath))
                {
                    player.Play();
                }
            }
            App.OfflineMode = true;
            App.Backup = BackupManager.GetExistingBackup();
            foreach (var win in Application.Current.Windows)
            {
                var window = (Window)win;
                if (window is not MainWindow)
                    window.Close();
            }
            await WindowManager.Get<MainWindow>().UpdateAll();
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

            // !!! POTENTIAL PIZDA
            // If there are somehow multiple models with same name, expect the unexpected =)
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

            public static async Task<Model.Ticket> Post(Guid deviceBrandId,
                                                        Guid deviceModelId,
                                                        Guid deviceTypeId,
                                                        string? clientName,
                                                        string? clientPhone,
                                                        string? note,
                                                        string? description,
                                                        Guid? guid = null)
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
                    Description = description,
                    TicketId = guid
                };
                return await SendRequest<Model.Ticket>($"ticket", HttpMethod.Post, dto);
            }

            public static async Task<Model.Ticket> Put(Guid ticketId, string note)
            {
                if (App.OfflineMode)
                {
                    var ticket = App.Backup.Tickets.FirstOrDefault(t => t.Guid.Equals(ticketId));
                    ticket.Note = note;
                    var index = App.Backup.Tickets.FindIndex(t => t.Guid.Equals(ticketId));
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

            public static async Task<List<Model.Ticket>> Search(string q)
            {
                if (App.OfflineMode)
                {
                    // finish this (fetching devicebrands and models)
                    return App.Backup.Tickets.Where(x => 
                    x.ClientFullname.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                    x.ClientPhoneNumber.Contains(q))
                        .ToList();
                }
                return await SendRequest<List<Model.Ticket>>($"ticket/search?q={q}", HttpMethod.Get);
            }

            public static async Task<List<Model.Ticket>> Filter(
                Guid? deviceBrandGuid,
                Guid? deviceModelGuid,
                Guid? deviceTypeGuid,
                Guid? statusGuid,
                string? clientName,
                string? clientPhone,
                DateTime? startDate,
                DateTime? endDate)
            {
                var baseUrl = $"ticket/filter?";
                var queryParams = new List<string>();

                if (deviceBrandGuid.HasValue)
                    queryParams.Add($"deviceBrandGuid={deviceBrandGuid.Value}");

                if (deviceModelGuid.HasValue)
                    queryParams.Add($"deviceModelGuid={deviceModelGuid.Value}");

                if (deviceTypeGuid.HasValue)
                    queryParams.Add($"deviceTypeGuid={deviceTypeGuid.Value}");

                if (statusGuid.HasValue)
                    queryParams.Add($"statusGuid={statusGuid.Value}");

                if (!String.IsNullOrWhiteSpace(clientName))
                    queryParams.Add($"clientName={clientName}");

                if (!String.IsNullOrEmpty(clientPhone))
                    queryParams.Add($"clientPhone={clientPhone}");

                if (startDate.HasValue)
                    queryParams.Add($"startDate={startDate.Value}");

                if (endDate.HasValue)
                    queryParams.Add($"endDate={endDate.Value}");

                var finalUrl = baseUrl + String.Join('&', queryParams);

                return await SendRequest<List<Model.Ticket>>(finalUrl, HttpMethod.Get);
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

            public static async Task<Model.TicketMalfunction> GetById(Guid id)
            {
                if (App.OfflineMode)
                {
                    return App.Backup.TicketMalfunctions.FirstOrDefault(tm => tm.Guid.Equals(id));
                }
                return await SendRequest<Model.TicketMalfunction>($"ticketmalfunction/{id}", HttpMethod.Get);
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

            public static async Task<Model.TicketStatus> GetById(Guid id)
            {
                return await SendRequest<Model.TicketStatus>($"ticketstatus/{id}", HttpMethod.Get);
            }

            public static async Task<bool> Post(Guid ticketId, Guid statusId, Guid? id = null)
            {
                if (App.OfflineMode)
                {
                    App.Backup.TicketStatuses.Add(new Model.TicketStatus(ticketId, statusId));
                    return true;
                }
                var dto = new
                {
                    TicketId = ticketId,
                    StatusId = statusId,
                    Guid = id
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

            public static async Task<Model.TicketRepair> GetById(Guid id)
            {
                return await SendRequest<Model.TicketRepair>($"ticketrepair/{id}", HttpMethod.Get);
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
                await Delete(backup);
                await Post(backup);
                await Put(backup);
            }

            private static async Task Delete(Backup backup)
            {
                var dbTickets = await Ticket.Get();
                var backupTicketIds = backup.Tickets.Select(t => t.Guid).ToHashSet();

                foreach (var dbTicket in dbTickets)
                {
                    if (!backupTicketIds.Contains(dbTicket.Guid))
                    {
                        await Ticket.Delete(dbTicket.Guid);
                    }
                }

                var dbTicketKits = await TicketKit.Get();
                var backupTicketKitIds = backup.TicketKits.Select(t => t.Guid).ToHashSet();
                foreach (var dbTicketKit in dbTicketKits)
                {
                    if (!backupTicketKitIds.Contains(dbTicketKit.Guid))
                    {
                        await TicketKit.Delete(dbTicketKit.Guid);
                    }
                }

                var dbTicketMalfs = await TicketMalfunction.Get();
                var backupTicketMalfIds = backup.TicketMalfunctions.Select(t => t.Guid).ToHashSet();
                foreach (var dbTicketMalf in dbTicketMalfs)
                {
                    if (!backupTicketMalfIds.Contains(dbTicketMalf.Guid))
                    {
                        await TicketMalfunction.Delete(dbTicketMalf.Guid);
                    }
                }

                //var dbTicketRepairs = await TicketRepair.Get();
                //var backupTicketRepairIds = backup.TicketMalfunctions.Select(t => t.Guid).ToHashSet();
                //foreach (var dbTicketRepair in dbTicketRepairs)
                //{
                //    if (!backupTicketRepairIds.Contains(dbTicketRepair.Guid))
                //    {
                //        await TicketRepair.Delete
                //    }
                //}


            }

            private static async Task Post(Backup backup)
            {
                var brands = backup.DeviceBrands;
                foreach (var brand in brands)
                {
                    var existingBrand = await DeviceBrand.GetByName(brand.Name);
                    if (existingBrand == null)
                    {
                        await DeviceBrand.Post(brand.Name);
                    }
                }

                var models = backup.DeviceModels;
                foreach (var model in models)
                {
                    var existingModel = await DeviceModel.GetByName(model.Name);
                    if (existingModel == null)
                    {
                        await DeviceModel.Post(model.Guid, model.Name);
                    }
                }

                var types = backup.DeviceTypes;
                foreach (var type in types)
                {
                    var existingBrand = await DeviceType.GetByName(type.Name);
                    if (existingBrand == null)
                    {
                        await DeviceType.Post(type.Name);
                    }
                }

                var repairs = backup.Repairs;
                foreach (var repair in repairs)
                {
                    var existingRepair = await Repair.GetByName(repair.Name);
                    if (existingRepair == null)
                    {
                        await Repair.Post(repair.Name);
                    }
                }

                var tickets = backup.Tickets;
                foreach (var ticket in tickets)
                {
                    var existingTicket = await Ticket.GetById(ticket.Guid);
                    if (existingTicket == null)
                    {
                        await Ticket.Post(ticket.DeviceBrandGuid,
                                          ticket.DeviceModelGuid,
                                          ticket.DeviceTypeGuid,
                                          ticket.ClientFullname,
                                          ticket.ClientPhoneNumber,
                                          ticket.Note,
                                          null,
                                          ticket.Guid);
                    }
                }

                var ticketKits = backup.TicketKits;
                foreach (var ticketKit in ticketKits)
                {
                    var existingTicketKit = await TicketKit.GetById(ticketKit.Guid);
                    if (existingTicketKit == null)
                    {
                        await TicketKit.Post(ticketKit.TicketGuid, ticketKit.Name);
                    }
                }

                var ticketMalfs = backup.TicketMalfunctions;
                foreach (var ticketMalf in ticketMalfs)
                {
                    var existingTicketMalf = await TicketMalfunction.GetById(ticketMalf.Guid);
                    if (existingTicketMalf == null)
                    {
                        await TicketMalfunction.Post(ticketMalf.TicketGuid, ticketMalf.Name);
                    }
                }

                var ticketRepairs = backup.TicketRepairs;
                foreach (var ticketRepair in ticketRepairs)
                {
                    var existingTicketRepair = await TicketRepair.GetById(ticketRepair.Guid);
                    if (existingTicketRepair == null)
                    {
                        await TicketRepair.Post(ticketRepair.TicketGuid, ticketRepair.RepairGuid, (int)ticketRepair.Price);
                    }
                }

                var ticketStatuses = backup.TicketStatuses;
                foreach (var ticketStatus in ticketStatuses)
                {
                    var existingTicketStatus = await TicketStatus.GetById(ticketStatus.Guid);
                    if (existingTicketStatus == null)
                    {
                        await TicketStatus.Post(ticketStatus.TicketGuid, ticketStatus.StatusGuid);
                    }
                }
            }

            private static async Task Put(Backup backup)
            {
                var dbTickets = await Ticket.Get();
                var backupTickets = backup.Tickets;
                var ticketsToUpdate = backupTickets
                    .Where(bt => dbTickets.Any(db => db.Guid == bt.Guid && !db.Equals(bt)))
                    .ToList();

                foreach (var ticket in ticketsToUpdate)
                {
                    await Ticket.Put(ticket.Guid, ticket.Note);
                    await Ticket.ChangeDeviceBrand(ticket.Guid, ticket.DeviceBrandGuid);
                    await Ticket.ChangeDeviceModel(ticket.Guid, ticket.DeviceModelGuid);
                    await Ticket.ChangeDeviceType(ticket.Guid, ticket.DeviceTypeGuid);
                    await Ticket.ChangeClientName(ticket.Guid, ticket.ClientFullname);
                    await Ticket.ChangeClientPhone(ticket.Guid, ticket.ClientPhoneNumber);
                }

                var dbTicketKits = await TicketKit.Get();
                var backupTicketKits = backup.TicketKits;
                var ticketsKitsToUpdate = backupTicketKits
                    .Where(bt => dbTicketKits.Any(db => db.Guid == bt.Guid && !db.Equals(bt)))
                    .ToList();

                foreach (var ticketKit in ticketsKitsToUpdate)
                {
                    await TicketKit.Put(ticketKit.TicketGuid, ticketKit.Name);
                }

                var dbTicketMalfs = await TicketMalfunction.Get();
                var backupTicketMalfs = backup.TicketMalfunctions;
                var ticketMalfsToUpdate = backupTicketMalfs
                    .Where(bt => dbTicketMalfs.Any(db => db.Guid == bt.Guid && !db.Equals(bt)))
                    .ToList();

                foreach (var ticketMalf in ticketMalfsToUpdate)
                {
                    await TicketMalfunction.Put(ticketMalf.TicketGuid, ticketMalf.Name);
                }

            }
        }
    }
}
