using fixflow.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fixflow.Utility
{
    class BackupManager
    {
        public readonly static string Path = "backup.json";

        public static async Task<Backup> CreateBackup()
        {
            var backup = new Backup
            {
                DeviceBrands = await ApiClient.DeviceBrand.Get(),
                DeviceModels = await ApiClient.DeviceModel.Get(),
                Repairs = await ApiClient.Repair.Get(),
                Statuses = await ApiClient.Status.Get(),
                Tickets = await ApiClient.Ticket.Get(),
                TicketKits = await ApiClient.TicketKit.Get(),
                TicketMalfunctions = await ApiClient.TicketMalfunction.Get(),
                TicketRepairs = await ApiClient.TicketRepair.Get(),
                TicketStatuses = await ApiClient.TicketStatus.Get()
            };
            var json = JsonConvert.SerializeObject(backup, Formatting.Indented);
            File.WriteAllText(Path, json);
            return backup;
        }

        public static async void SetBackup()
        {
            App.Backup = await CreateBackup();
        }

        public static void SaveBackup(Backup backup)
        {
            File.WriteAllText(Path, JsonConvert.SerializeObject(backup, Formatting.Indented));
        }
    }
}
