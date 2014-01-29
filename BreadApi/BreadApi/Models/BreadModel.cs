using System.Collections.Generic;

namespace Hylasoft.BreadApi.Models
{
  public class BreadModel
  {
    public BreadLoader Bread { get; set; }
  }

  public class BreadLoader
  {
    public BreadLoader(BreadEngine.BreadLoader b)
    {
      Name = b.Name;
      Breads = new List<Bread>();
      foreach (var bread in b.Breads)
      {
        var br = new Bread
        {
          Name = bread.Name,
          Methods = bread.Methods,
          Entity = bread.EntityType == null ? "" : bread.EntityType.Name
        };
        Breads.Add(br);
      }
    }

    public string Name { get; set; }
    public IList<Bread> Breads { get; set; }
  }

  public class Bread
  {
    public string Name { get; set; }
    public IList<string> Methods { get; set; }
    public string Entity { get; set; }
  }
}