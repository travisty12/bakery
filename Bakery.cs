using System;
using System.Collections.Generic;
using Bakery.Food;

namespace Bakery
{
  public class Order
  {
    public static Bread wholeGrain = new Bread("3 Wheat Whole Grain", 3, 4);
    public static Bread white = new Bread("Standard Bread", 5, 20);
    public static Bread vegan = new Bread("Vegan Bread", 4.75, 16);
    public static Pastry bananaBread = new Pastry("Banana Bread", 2, 35);
    public static Pastry muffin = new Pastry("Muffin", 6.50, 10);
    public static Pastry cake = new Pastry("Slice of Chocolate Cake", 3.25, 0);

    public static Dictionary<string, int?> order = new Dictionary<string, int?>();
    public static Dictionary<string, int[]> deals = new Dictionary<string, int[]>()
    {
      {"Standard Bread", new int[] {2, 5}},
      {"Vegan Bread", new int[] {4, 15}},
      {"Muffin", new int[] {3, 15}},
      {"Banana Bread", new int[] {3, 5}}
    };
    public static List<Bread> Breads = new List<Bread>() {wholeGrain, white, vegan};
    public static List<Pastry> Pastries = new List<Pastry>() {bananaBread, muffin, cake};
    public static void Main()
    {
      Console.WriteLine("--------------------");
      Console.WriteLine("Welcome to Pierre's Bakery!");
      Console.WriteLine("--Options--");
      Console.WriteLine("Type 'menu' to view the menu");
      Console.WriteLine("Type 'purchase' to add to your order");
      Console.WriteLine("Type 'view' to view your order");
      Console.WriteLine("Type 'exit' or 'quit' to leave Pierre's");
      string answer = Console.ReadLine();
      if (answer == "menu" || answer == "Menu")
      {
        ShowMenu();
      }
      else if (answer == "purchase" || answer == "Purchase")
      {
        AddToOrder();
      }
      else if (answer == "view" || answer == "View")
      {
        ViewOrder();
      }
      else if (answer == "exit" || answer == "Exit" || answer == "quit" || answer == "Quit")
      {
        Console.WriteLine("Thank you for visiting Pierre's!");
        Console.WriteLine("Goodbye");
      }
      else
      {
        Console.WriteLine("Unknown response. Please try again.");
        Main();
      }
    }
    public static void ShowMenu()
    {
      Console.WriteLine("Here's what we have on menu for today:");
      Console.WriteLine("--------------------");

      foreach(Bread bread in Breads)
      {
        Console.WriteLine("--------------------");
        Console.WriteLine(bread.Type);
        Console.WriteLine("$" + bread.Price);
        if (bread.Stock > 0)
        {
        Console.WriteLine(bread.Stock + " remaining");
        }
        else
        {
          Console.WriteLine("Fresh out of " + bread.Type);
        }
      }
      foreach(Pastry pastry in Pastries)
      {
        Console.WriteLine("--------------------");
        Console.WriteLine(pastry.Type);
        Console.WriteLine("$" + pastry.Price);
        if (pastry.Stock > 0)
        {
        Console.WriteLine(pastry.Stock + " remaining");
        }
        else
        {
          Console.WriteLine("Fresh out of " + pastry.Type);
        }
      }
      Console.WriteLine("--------------------");
      Console.WriteLine("Today's Deals");
      foreach(KeyValuePair<string, int[]> entry in deals)
      {
        Console.WriteLine(entry.Key + ": " + entry.Value[0] + " for $" + entry.Value[1]);
      }
      Console.WriteLine("--------------------");
      Console.WriteLine("Would you like to add something to your order? (Y/n)");
      string addOrder = Console.ReadLine();
      if (addOrder == "y" || addOrder == "Y")
      {
        AddToOrder();
      }
      else 
      {
        Main();
      }
    }
    public static void AddToOrder()
    {
      Console.WriteLine("--------------------");
      Console.WriteLine("Type the name of the item you would like to purchase:");
      string itemToPurchase = Console.ReadLine();
      bool found = false;
      for (int i = 0; i < Breads.Count; i++)
      {
        if (Breads[i].Type == itemToPurchase)
        {
          Console.WriteLine("How many would you like?");
          int number = int.Parse(Console.ReadLine());
          if (Breads[i].Stock >= number)
          {
            if (order.ContainsKey(Breads[i].Type))
            {
              order[Breads[i].Type] = order[Breads[i].Type] + number;
            }
            else 
            {
              order[Breads[i].Type] = number;
            }
            Breads[i].Stock -= number;
            Console.WriteLine("Added " + number + " " + Breads[i].Type + " to your order");
          }
          else
          {
            Console.WriteLine("Not enough in stock!");
          }
          found = true;
          break;
        }
      }
      for (int i = 0; i < Pastries.Count; i++)
      {
        if (Pastries[i].Type == itemToPurchase)
        {
          Console.WriteLine("How many would you like?");
          int number = int.Parse(Console.ReadLine());
          if (Pastries[i].Stock >= number)
          {
            if (order.ContainsKey(Pastries[i].Type))
            {
              order[Pastries[i].Type] = order[Pastries[i].Type] + number;
            }
            else 
            {
              order[Pastries[i].Type] = number;
            }
            Pastries[i].Stock -= number;
            Console.WriteLine("Added " + number + " " + Pastries[i].Type + " to your order");
          }
          else
          {
            Console.WriteLine("Not enough in stock!");
          }
          found = true;
          break;
        }
      }
      if (!found)
      {
        Console.WriteLine("Item not found.");
      }
      Console.WriteLine("Would you like to continue adding to your order? (Y/n)");
      string addOrder = Console.ReadLine();
      if (addOrder == "y" || addOrder == "Y")
      {
        AddToOrder();
      }
      else 
      {
        Main();
      }

    }
    public static void ViewOrder()
    {
      Console.WriteLine("--------------------");
      Console.WriteLine("Current Order");
      foreach(KeyValuePair<string, int?> entry in order)
      {
        Console.WriteLine(entry.Key + ": " + entry.Value);
      }
      Console.WriteLine("--------------------");
      Console.WriteLine("Total Cost:");
      double total = 0;
      var keys = new List<string>(order.Keys);
      foreach(string entry in keys)
      {
        foreach(Bread bread in Breads)
        {
          if (bread.Type == entry)
          {
            if (deals.ContainsKey(entry)) {
              total += (double)deals[entry][1] * Math.Floor((double)order[entry] / (double)deals[entry][0]) + ((double)order[entry] % (double)deals[entry][0]) * bread.Price;
            }
            else 
            {
            total += (double)order[entry] * bread.Price;
            }
          }
        }
        foreach(Pastry pastry in Pastries)
        {
          if (pastry.Type == entry)
          {
            if (deals.ContainsKey(entry)) {
              total += (double)deals[entry][1] * Math.Floor((double)order[entry] / (double)deals[entry][0]) + ((double)order[entry] % (double)deals[entry][0]) * pastry.Price;
            }
            else 
            {
            total += (double)order[entry] * pastry.Price;
            }
          }
        }
        
      }
      if (total != 0)
      {
        Console.WriteLine("Total: $" + total);
      }
      Console.WriteLine("Would you like to continue with Pierre's? (Y/n)");
      string answer = Console.ReadLine();
      if (answer == "Y" || answer == "y")
      {
        Main();
      }
      else
      {
        Console.WriteLine("Thank you for visiting Pierre's!");
      }
    }
  }
}