using System;

public static class GameActions
{
    public static void RunRoomChallenge(Hero hero, Room currentRoom)
    {
        var challenge = currentRoom.Challenge;
        if (challenge == null)
        {
            Console.WriteLine("This room has no challenge!");
            return;
        }

        int statValue = 0;
        string requiredItem = "";

        switch (challenge.Type)
        {
            case "Combat":
                statValue = hero.Strength;
                requiredItem = "Sword";
                break;
            case "Trap":
                statValue = hero.Agility;
                requiredItem = "Smoke Bomb";
                break;
            case "Puzzle":
                statValue = hero.Intelligence;
                requiredItem = "Hint Scroll";
                break;
        }


        if (statValue >= challenge.Difficulty)
        {
            Console.WriteLine("You overcame the challenge using your stats!");
        }
        else if (hero.HasItem(requiredItem))
        {
            Console.WriteLine($"You overcame the challenge using your {requiredItem}!");
            hero.RemoveItem(requiredItem);
        }
        else
        {
            int penalty = challenge.Difficulty - statValue;
            hero.Health -= penalty;
            Console.WriteLine($"You failed the challenge and lost {penalty} health. (Remaining health: {hero.Health})");
            hero.CheckRoomForItem(currentRoom);
            hero.PrintInventory();
            if (hero.Health < 20 && hero.HasItem("Potion"))
            {
                Console.WriteLine("Would you like to use a Potion to heal? (yes/no)");
                string input = Console.ReadLine().ToLower();
                if (input == "yes")
                {
                    hero.UsePotion();
                }
            }
        }

        hero.CheckRoomForItem(currentRoom);
        hero.PrintInventory();

        currentRoom.Challenge = null; // Remove the challenge after solving it
    }
}
