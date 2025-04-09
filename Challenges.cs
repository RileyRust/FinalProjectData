public class ChallengeNode
{
    public int Difficulty;
    public string Type; // "Combat", "Puzzle", "Trap"
    public ChallengeNode Left, Right;

    public ChallengeNode(int difficulty, string type)
    {
        Difficulty = difficulty;
        Type = type;
    }
}