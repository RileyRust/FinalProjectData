public class Hero
{
    public int Strength {get; set;} = 3; 

    public int Agility {get; set;} = 3;

    public int Intelligence {get; set;} = 3;

    public int Health {get; set;} = 20; 

    private Queue<string> inventory = new Queue<string>();

    public Hero(int strength, int agility, int intelligence)
    {
        Strength = strength; 
        Agility =  agility; 
        Intelligence = intelligence; 
        Additem("Sword"); 
        Additem("Potion"); 
    
    }
    public void Additem(string item)
    {
        if(inventory.Count>=5)
        {
            inventory.Dequeue(); 
        }
        inventory.Enqueue(item); 
    }
   
    public bool HasItem(string item)
     {
     return inventory.Contains(item);
     } 
      public void PrintInventory()
    {
        Console.WriteLine("Inventory: " + string.Join(", ", inventory));
    }
        


}