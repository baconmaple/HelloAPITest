using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using HelloApi.Models;

namespace HelloApi.Services
{
    public class FileAccessService : IDisposable, IFileAccessService
    {
        private readonly HelloContext _database;

        public FileAccessService(HelloContext database)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        public List<HelloItem> FilePreProcess()
        {
            string jsonString = File.ReadAllText("hello-data.json");
            List<HelloItem> data = JsonSerializer.Deserialize<List<HelloItem>>(jsonString);
            return data;            
        }

        public void InitialData()
        {
            try
            {
                List<HelloItem> data = FilePreProcess();
                _database.InitData(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Have a Problem About Seed Data: {ex.Message}");
            }
        }
    }
}