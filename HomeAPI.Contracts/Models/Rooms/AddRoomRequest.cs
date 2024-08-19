namespace HomeAPI.Contracts.Models.Rooms
{
    public class AddRoomRequest
    {
        public string Name { get; set; }
        public int Area { get; set; }
        public bool GasConnected { get; set; }
        public int Voltage { get; set; }
    }
}