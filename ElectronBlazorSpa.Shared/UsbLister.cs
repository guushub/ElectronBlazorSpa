using System;
using System.Collections.Generic;
using System.Text;
using System.Management;

namespace ElectronBlazorSpa.Shared
{
    public class UsbLister
    {
        public static List<UsbDeviceInfo> GetUSBDevices()
        {
            List<UsbDeviceInfo> devices = new List<UsbDeviceInfo>();

            ManagementObjectCollection collection;
            using (var searcher = new ManagementObjectSearcher(@"Select * From Win32_USBHub"))
                collection = searcher.Get();

            foreach (var device in collection)
            {
                devices.Add(new UsbDeviceInfo()
                {
                    DeviceID = (string)device.GetPropertyValue("DeviceID"),
                    PnpDeviceID = (string)device.GetPropertyValue("PNPDeviceID"),
                    Description = (string)device.GetPropertyValue("Description")
                });
            }

            collection.Dispose();
            return devices;
        }
    }

    
}

