using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Client.Model;
using System.Text.Json;
using Microsoft.AspNetCore.SignalR.Client;

namespace Client
{
    public static class Data
    {
        // Static methods for getting system information. Like ram, cpu and disk space
        public static (List<DiskDto>,List<RamDto>,List<ProcessesDto>) GetData()
        {
            // Get ram information
            var ramFree = new PerformanceCounter("Memory", "Available MBytes");
            var gcMemoryInfo = GC.GetGCMemoryInfo();
            var installedMemory = gcMemoryInfo.TotalAvailableMemoryBytes;
            var physicalMemory = (double) installedMemory / 1048576.0;
            List<RamDto> ramDtos = new List<RamDto>()
            {
                new RamDto($"{physicalMemory}", $"{ramFree.NextValue()}")
            };

            // Get processes utilization
            List<ProcessesDto> processesDtos = new List<ProcessesDto>();
             foreach (Process proc in Process.GetProcesses()) {
                 using (PerformanceCounter pcProcess = new PerformanceCounter("Process", "% Processor Time", proc.ProcessName)) {
                     pcProcess.NextValue();
                     processesDtos.Add(new ProcessesDto($"{proc.ProcessName}",$"{pcProcess.NextValue()}"));
                 }
             }
            
            // Get disk info
            List<DiskDto> diskDtos = new();
            foreach (var drive in DriveInfo.GetDrives())
            { 
                try
                {
                    diskDtos.Add(new DiskDto($"{drive.Name}", $"{drive.TotalFreeSpace}", $"{drive.TotalSize}"));
                }
                catch
                {
                }
            }
            return (diskDtos,ramDtos,processesDtos);
        }
    }

    class Program
    {

        static async Task Main(string[] args)
        {
            const string url = "https://localhost:5001/hub";
            var connection = new HubConnectionBuilder()
                .WithUrl(url)
                .WithAutomaticReconnect()
                .Build();
            
            // Start the websocket connection
            await connection.StartAsync();
            
            var (diskDtos, ramDtos, processesDtos) = Data.GetData();
            var jsonDisk = JsonSerializer.Serialize(diskDtos);
            var jsonRam=JsonSerializer.Serialize(ramDtos);
            var jsonProcess = JsonSerializer.Serialize(processesDtos);

            await connection.InvokeAsync("ReceiveData", jsonDisk, jsonRam, jsonProcess);
        }
    }
}