using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using monitoringMVC.Models;
using static System.Text.Json.JsonSerializer;
using static monitoringMVC.DB.PostgresDataBase;

namespace monitoringMVC.Hubs
{
    public class DataHub : Hub
    {
        // Receive data from the console application.
        public void ReceiveData(string harddisk,string rmemory, string prosc)
        {
            var disks = Deserialize<List<Disk>>(harddisk);
            
            var rams = Deserialize<List<Ram>>(rmemory);
            var process = Deserialize<List<Process>>(prosc);

            // Disk
            foreach (var disk in disks)
                SaveDataDisk(disk);

            // Ram
            foreach (var ram in rams)
                SaveDataRam(ram);

            // Cpu
            foreach (var prosec in process)
                SaveDataProcess(prosec);
        }
    }
}