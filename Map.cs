using System.Net.Sockets;

public class Map
{
    public Dictionary<int, Room> Rooms = new Dictionary<int, Room>();
    public int ExitRoomNumber;
    public void GenerateMap()
    {
        int totalRooms = 30;
        Random rand = new Random();

        for (int i = 1; i <= totalRooms; i++)
        {
            Rooms[i] = new Room(i);
        }


        List<int> shuffledRooms = Enumerable.Range(1, totalRooms)
                                            .OrderBy(_ => rand.Next())
                                            .ToList(); //look up

        for (int i = 0; i < shuffledRooms.Count - 1; i++)
        {
            int current = shuffledRooms[i];
            int next = shuffledRooms[i + 1];
            if (!Rooms[current].Connections.Any(e => e.Destination == Rooms[next]))
            {
                Rooms[current].Connections.Add(new Edge(Rooms[next]));
            }
        }

        ExitRoomNumber = shuffledRooms[^1];
        string[] possibleItems = { "Lockpick", "Smoke Bomb", "Hint Scroll", "Potion" };
        for (int i = 1; i <= totalRooms; i++)
        {
            int chance = rand.Next(100); 

            if (chance < 20) 
            {
                string item = possibleItems[rand.Next(possibleItems.Length)];
                Rooms[i].Item = item;
            }
            else
            {
                Rooms[i].Item = null; 
            }
        }


        for (int i = 1; i <= totalRooms; i++)
        {
            int extraConnections = rand.Next(1, 3);
            for (int j = 0; j < extraConnections; j++)
            {
                int other = rand.Next(1, totalRooms + 1);
                if (other != i && !Rooms[i].Connections.Any(e => e.Destination == Rooms[other]))
                {
                    string[] types = { "Strength", "Agility", null };
                    string[] items = { "Lockpick", null };
                    string type = types[rand.Next(types.Length)];
                    string item = items[rand.Next(items.Length)];
                    int value = (type != null) ? rand.Next(5, 11) : 0; // look up

                    Rooms[i].Connections.Add(new Edge(Rooms[other], type, value, item));
                }
            }
        }

        string[] challengeTypes = { "Combat", "Puzzle", "Trap" };
        for (int i = 1; i <= totalRooms; i++)
        {
            string challengeType = challengeTypes[rand.Next(challengeTypes.Length)];
            int difficulty = rand.Next(1, 11); // 1–10
            Rooms[i].Challenge = new ChallengeNode(difficulty, challengeType);
        }
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