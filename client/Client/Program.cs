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
        public static (List<DiskDto>,List<RamDto>,List<ProcessesDto>) GetData()
        {
            PerformanceCounter ramFree = new PerformanceCounter("Memory", "Available MBytes");
            var gcMemoryInfo = GC.GetGCMemoryInfo();
            var installedMemory = gcMemoryInfo.TotalAvailableMemoryBytes;
            var physicalMemory = (double) installedMemory / 1048576.0;
            List<RamDto> ramDtos = new List<RamDto>()
            {
                new RamDto($"{physicalMemory}", $"{ramFree.NextValue()}")
            };

            
            List<ProcessesDto> processesDtos = new List<ProcessesDto>();
             foreach (Process proc in Process.GetProcesses()) {
                 using (PerformanceCounter pcProcess = new PerformanceCounter("Process", "% Processor Time", proc.ProcessName)) {
                     pcProcess.NextValue();
                     processesDtos.Add(new ProcessesDto($"{proc.ProcessName}",$"{pcProcess.NextValue()}"));
                 }
             }
            
             
            List<DiskDto> diskDtos = new List<DiskDto>();
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

        static void Main(string[] args)
        {
            
            HubConnection connection;
            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/hub")
                .WithAutomaticReconnect()
                .Build();
            connection.StartAsync();
            
            var result = Data.GetData();
            var jsonDisk = JsonSerializer.Serialize(result.Item1);
            var jsonRam=JsonSerializer.Serialize(result.Item2);
            var jsonProcess = JsonSerializer.Serialize(result.Item3);
            
            connection.InvokeAsync("ReceiveData", jsonDisk,jsonRam,jsonProcess).GetAwaiter().GetResult();
        }
    }
}