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

    if (statValue >= challenge.Difficulty || hero.HasItem(requiredItem))
    {
        Console.WriteLine("You overcame the challenge!");
    }
    else
    {
        int penalty = challenge.Difficulty - statValue;
        hero.Health -= penalty;
        Console.WriteLine($"You failed the challenge and lost {penalty} health. (Remaining health: {hero.Health})");
    }

    currentRoom.Challenge = null;
}

}
