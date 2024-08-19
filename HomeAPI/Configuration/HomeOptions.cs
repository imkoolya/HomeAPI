namespace HomeAPI.Configuration
{
    public class HomeOptions
    {
       public int FloorAmount { get; set; }
       public string Telephone { get; set; }
       public Heating Heating { get; set; }
       public int CurrentVolts { get; set; }
       public bool GasConnected { get; set; }
       public int Area { get; set; }
       public Material Material { get; set; }
       public Address Address { get; set; }
    }
}