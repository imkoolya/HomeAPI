namespace HomeAPI.Contracts.Models.Devices
{
    public class GetDevicesResponse
    {
        public int DeviceAmount { get; set; }
        public DeviceView [] Devices { get; set; }
    }

    public class DeviceView
    {
        public DateTime AddDate { get; set; } 
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public int CurrentVolts { get; set; }
        public bool GasUsage { get; set; }
        public string Location  { get; set; }
    }
}