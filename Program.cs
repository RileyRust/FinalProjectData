using System;

public class Program
{
    public static void Main()
    {
        Stack<Room> visitedRooms = new Stack<Room>();
        Hero hero = new Hero(strength: 10, agility: 7, intelligence: 5);
        Map map = new Map();
        map.GenerateMap();
        Stack<string> ItemStack = new Stack<string>();
        Random rand = new Random();


        ChallengeTree challengeTree = new ChallengeTree();
        challengeTree.Insert(3, "Puzzle");
        challengeTree.Insert(6, "Trap");
        challengeTree.Insert(9, "Combat");

        Room currentRoom = map.Rooms[1];
        map.Rooms[1].Challenge = null;
        while (currentRoom.Number != map.ExitRoomNumber && hero.Health > 0)
        {
            Console.WriteLine($"\n--- You are in Room {currentRoom.Number} ---");
            GameActions.RunRoomChallenge(hero, currentRoom);
            if (rand.Next(1, 11) == 1) // 1 out of 10
            {
                string[] items = { "Sword", "Smoke Bomb", "Hint Scroll"};
                string foundItems = items[rand.Next(items.Length)];
                ItemStack.Push(foundItems);
                Console.WriteLine($"You found a treasure: {foundItems}!");
            }

            var travelOptions = currentRoom.Connections
                .Where(e => e.CanTravel(hero))
                .ToList();

            if (travelOptions.Count == 0)
            {
                Console.WriteLine("No travelable paths from here. You're stuck!");
                break;
            }

            Console.WriteLine("Where do you want to go?");
            for (int i = 0; i < travelOptions.Count; i++)
            {
                var destination = travelOptions[i].Destination;
                var challenge = destination.Challenge;

                string challengeInfo = challenge != null
                    ? $"Challenge: {challenge.Type} (Difficulty {challenge.Difficulty})"
                    : "No challenge";

                Console.WriteLine($"{i + 1}. Room {destination.Number} - {challengeInfo}");
            }


            Console.Write("Enter your choice: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= travelOptions.Count)
            {
                visitedRooms.Push(currentRoom);
                currentRoom = travelOptions[choice - 1].Destination;
            }
            else
            {
                Console.WriteLine("Invalid choice. Try again.");
            }
        }

        if (currentRoom.Number == map.ExitRoomNumber)
        {
            Console.WriteLine("\n You reached the exit! You win!");
        }
        else if (hero.Health <= 0)
        {
            Console.WriteLine("\n You died before escaping the dungeon.");
        }
    }
}
