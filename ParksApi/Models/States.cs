namespace ParksApi.Models;

public class State 
{
  public int StateId { get; set; }
  public string Name { get; set; }  
  public List<Park> Parks { get; set; }
}