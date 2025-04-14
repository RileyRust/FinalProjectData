public static class GameActions
{
    public static void RunRoomChallenge(Hero hero, Room currentRoom, ChallengeTree challengeTree)
    {
        var challenge = challengeTree.FindClosest(currentRoom.Number);
        if (challenge == null)
        {
            Console.WriteLine("There are no more challenges!");
            return;
        }

        Console.WriteLine($"\nRoom {currentRoom.Number} has a challenge! Type: {challenge.Type}, Difficulty: {challenge.Difficulty}");

        bool passed = false;
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

        if (statValue >= challenge.Difficulty || hero.HasItem(requiredItem))
        {
            Console.WriteLine("You overcame the challenge!");
            passed = true;
        }
        else
        {
            int penalty = challenge.Difficulty - statValue;
            hero.Health -= penalty;
            Console.WriteLine($"You failed the challenge and lost {penalty} health. (Remaining health: {hero.Health})");
        }

        challengeTree.Remove(challenge.Difficulty);
    }
}
