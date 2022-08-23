using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using monitoringMVC.Models;
using Newtonsoft.Json;
using Npgsql;

namespace monitoringMVC.DB
{
    public static class PostgresDataBase
    {
        private static string Host = "localhost";
        private static string User = "postgres";
        private static string DBname = "Test";
        private static string Password = "9009";
        private static string Port = "5432";

        static string connString =
            String.Format(
                "Server={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Prefer",
                Host,
                User,
                DBname,
                Port,
                Password);

        public static void SaveDataDisk(Disk disk)
        {
            // Build connection string using parameters from portal

            using (var conn = new NpgsqlConnection(connString))

            {
                conn.Open();
                
                using (var command = new NpgsqlCommand("INSERT INTO disks (name, freesizedisk, sizedisk)  VALUES (@name, @free, @size)", conn))
                {
                    command.Parameters.AddWithValue("name", disk.Name);
                    command.Parameters.AddWithValue("free", disk.FreeSizeDisk);
                    command.Parameters.AddWithValue("size", disk.SizeDisk);

                    command.ExecuteNonQuery();
                }
            }

        }

        public static void SaveDataRam(Ram ram)
        {

            using (var conn = new NpgsqlConnection(connString))

            {
                conn.Open();

                using (var command =
                       new NpgsqlCommand("INSERT INTO rams (totalram, freeram)  VALUES (@total, @free)", conn))
                {
                    command.Parameters.AddWithValue("total", ram.TotalRam);
                    command.Parameters.AddWithValue("free", ram.FreeRam);

                    command.ExecuteNonQuery();
                }
            }
        }
        
        public static void SaveDataProcess(Process process)
        {

            using (var conn = new NpgsqlConnection(connString))

            {
                conn.Open();

                using (var command =
                       new NpgsqlCommand("INSERT INTO process (processes, cpu)  VALUES (@proc, @cpu)", conn))
                {
                    command.Parameters.AddWithValue("proc", process.Processes);
                    command.Parameters.AddWithValue("cpu", process.Cpu);

                    command.ExecuteNonQuery();
                }
            }
        }
        
        public static List<Disk> GetDataDisk()
        {
            List<Disk> resultDisks = new();
            using (var conn = new NpgsqlConnection(connString))
            {

                conn.Open();

                string sql = "SELECT * FROM disks";
                using var cmd = new NpgsqlCommand(sql, conn);

                using NpgsqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    resultDisks.Add(new Disk() { Id = rdr.GetInt32(0), Name = rdr.GetString(1), FreeSizeDisk = rdr.GetString(2),SizeDisk = rdr.GetString(3) });
                }
            }

            return resultDisks;
        }
        
        public static List<Ram> GetDataRam()
        {
            List<Ram> resultRams = new();

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                string sql = "SELECT * FROM rams";
                using var cmd = new NpgsqlCommand(sql, conn);

                using NpgsqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    resultRams.Add(new Ram() { Id = rdr.GetInt32(0), TotalRam = rdr.GetString(1), FreeRam = rdr.GetString(2) });
                }
            }
            return resultRams;
        }
        
        public static List<Process> GetDataProcess()
        {
            List<Process> resultProcess = new();

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                string sql = "SELECT * FROM process";
                using var cmd = new NpgsqlCommand(sql, conn);

                using NpgsqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    resultProcess.Add(new Process() { Id = rdr.GetInt32(0), Processes = rdr.GetString(1), Cpu = rdr.GetString(2) });
                }
            }
            return resultProcess;
        }
    }
}