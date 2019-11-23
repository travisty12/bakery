using System;

namespace Bakery.Food 
{
  public class Bread
  {
    public string Type { get; set; }
    public double Price { get; set; }
    public int? Stock { get; set; }

    public Bread(string type, double price, int? stock)
    {
      Type = type;
      Price = price;
      Stock = stock;
    }
  }
  public class Pastry
  {
    public string Type { get; set; }
    public double Price { get; set; }
    public int? Stock { get; set; }

    public Pastry(string type, double price, int? stock)
    {
      Type = type;
      Price = price;
      Stock = stock;
    }
  }
}
