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
                DeviceTypes = await ApiClient.DeviceType.Get(),
                Repairs = await ApiClient.Repair.Get(),
                Statuses = await ApiClient.Status.Get(),
                Tickets = await ApiClient.Ticket.Get(),
                TicketKits = await ApiClient.TicketKit.Get(),
                TicketMalfunctions = await ApiClient.TicketMalfunction.Get(),
                TicketRepairs = await ApiClient.TicketRepair.Get(),
                TicketStatuses = await ApiClient.TicketStatus.Get(),
                Timestamp = DateTime.Now
            };
            var json = JsonConvert.SerializeObject(backup, Formatting.Indented);
            File.WriteAllText(Path, json);
            return backup;
        }

        public static void SetBackup()
        {
            if (File.Exists(Path))
            {
                App.Backup = JsonConvert.DeserializeObject<Backup>(File.ReadAllText(Path));
            }
        }

        public static void SaveBackup(Backup backup)
        {
            File.WriteAllText(Path, JsonConvert.SerializeObject(backup, Formatting.Indented));
        }
    }
}
