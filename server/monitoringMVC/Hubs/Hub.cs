using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using monitoringMVC.DB;
using monitoringMVC.Models;
using Newtonsoft.Json;
using Npgsql;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace monitoringMVC.Hubs
{
    public class DataHub : Hub
    {
        public async Task ReceiveData(string harddisk,string rmemory, string prosc)
        {
            var disks = JsonSerializer.Deserialize<List<Disk>>(harddisk);
            
            var rams = JsonSerializer.Deserialize<List<Ram>>(rmemory);
            var process = JsonSerializer.Deserialize<List<Process>>(prosc);
            
            
            foreach (var disk in disks)
            {
                PostgresDataBase.SaveDataDisk(disk);
            }

            foreach (var ram in rams)
            {
                PostgresDataBase.SaveDataRam(ram);
            }

            foreach (var prosec in process)
            {
                PostgresDataBase.SaveDataProcess(prosec);
            }
            
        }
    }
    
}