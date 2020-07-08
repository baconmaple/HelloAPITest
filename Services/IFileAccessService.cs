using System;
using System.Collections.Generic;
using HelloApi.Models;

namespace HelloApi.Services
{
    public interface IFileAccessService : IDisposable
    {
        void InitialData();
        List<HelloItem> FilePreProcess();
    }
}