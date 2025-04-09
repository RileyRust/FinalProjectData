using System.Net.Sockets;

public class Map
{
     public Dictionary<int, Room> Rooms = new Dictionary<int, Room>();
    public int ExitRoomNumber;
     public void GenerateMap()
    {

        for (int i = 1; i <= 15; i++) // makes rooms
        {
            Rooms[i] = new Room(i);
        }
         for (int i = 1; i < 15; i++) // connects rooms
        {
            Rooms[i].Connections.Add(new Edge(Rooms[i + 1]));
        }
        Random rand = new Random();
        for (int i = 1; i <= 15; i++) // adds challenges 
        {
            int other = rand.Next(1, 16);
            if (other != i && !Rooms[i].Connections.Any(e => e.Destination == Rooms[other]))
            {
                string[] types = { "Strength", "Agility", null };
                string[] items = { "Lockpick", null };
                string type = types[rand.Next(types.Length)]; //randomly picks a stat
                string item = items[rand.Next(items.Length)]; // randomly picks a item
                int value = (type != null) ? rand.Next(5, 11) : 0; // picks a number if no stat set to zero

                Rooms[i].Connections.Add(new Edge(Rooms[other], type, value, item)); // addds room to another with requirements
            }
        }

       
        ExitRoomNumber = 15;
    }

      public bool PathExistsBFS(int start, int goal)
    {
        var visited = new HashSet<int>(); //visited nodes
        var queue = new Queue<int>(); //  future rooms
        queue.Enqueue(start); // adds all nodes to queue

        while (queue.Count > 0) // loop continues as long as there are rooms that havent been explored
        {
            int current = queue.Dequeue();
            if (current == goal) return true; // returns true if end
            if (visited.Contains(current)) continue; // skips of we've been here

            visited.Add(current);

            foreach (var edge in Rooms[current].Connections) // explores all reachable rooms
            {
                queue.Enqueue(edge.Destination.Number);
            }
        }

        return false;
    }
    
}