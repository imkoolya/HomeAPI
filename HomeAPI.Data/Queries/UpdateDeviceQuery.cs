namespace HomeAPI.Data.Queries
{
    public class UpdateDeviceQuery
    {
        public string NewName { get; }
        public string NewSerial{ get; }

        public UpdateDeviceQuery(string newName = null, string newSerial = null)
        {
            NewName = newName;
            NewSerial = newSerial;
        }
    }
}