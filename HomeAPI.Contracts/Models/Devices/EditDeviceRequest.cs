namespace HomeAPI.Contracts.Models.Devices
{
    public class EditDeviceRequest
    {
        public string NewRoom { get; set; }
        public string NewName { get; set; }
        public string NewSerial { get; set; }
    }
}