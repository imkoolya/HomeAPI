using System.ComponentModel.DataAnnotations.Schema;

namespace HomeAPI.Data.Models
{
    [Table("Devices")]
    public class Device
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime AddDate { get; set; } = DateTime.Now;
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public int CurrentVolts { get; set; }
        public bool GasUsage { get; set; }
        public string Location { get; set; }
        public Guid RoomId { get; set; }
        public Room Room { get; set; }
    }
}