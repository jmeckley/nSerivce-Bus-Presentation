using System;
using System.IO;
using NServiceBus;

namespace Backend
{
    public class EnsureEmailDirectoryExists
        : IWantToRunWhenBusStartsAndStops 
    {
        public static readonly string PickupDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Emails");

        public void Start()
        {
            EnsureDirectoryExists();
        }

        private static void EnsureDirectoryExists()
        {
            if (Directory.Exists(PickupDirectory)) return;
            Directory.CreateDirectory(PickupDirectory);
        }

        public void Stop()
        {
        }
    }
}
