namespace ParksApi.Models;

public class Park 
{
  public int ParkId { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public int StateId { get; set; }
  public string StateName { get; set; }
  public State State { get; set; }
}