public class Hero
{
    public int Strength { get; set; }

    public int Agility { get; set; }

    public int Intelligence { get; set; }

    public int Health { get; set; } = 20;

    private Queue<string> inventory = new Queue<string>();

    public Hero(int strength, int agility, int intelligence)
    {
        Strength = strength;
        Agility = agility;
        Intelligence = intelligence;
        Additem("Sword");
        Additem("Potion");

    }
    public void RemoveItem(string item)
    {
        inventory = new Queue<string>(inventory.Where(i => i != item));
    }
    public void Additem(string item)
    {
        if (inventory.Count >= 5)
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


    public void CheckRoomForItem(Room currentRoom)
    {
        if (currentRoom.Item != null)
        {
            Additem(currentRoom.Item);  // Pick up the item
            Console.WriteLine($"You found and picked up a {currentRoom.Item}!");

            currentRoom.Item = null; // Remove item from the room after picking it up
        }
    }
    public void UsePotion()
    {
        if (HasItem("Potion"))
        {
            Health += 10; // Heals 10 HP (you can change the amount)
            if (Health > 20) Health = 20; // Cap health at max 20
            RemoveItem("Potion");
            Console.WriteLine("You used a Potion! Health restored. (Current Health: " + Health + ")");
        }
        else
        {
            Console.WriteLine("You don't have any Potions to use!");
        }
    }




}