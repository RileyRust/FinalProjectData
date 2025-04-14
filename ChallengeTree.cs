public class ChallengeNode
{
    public int Difficulty;
    public string Type; 
    public ChallengeNode Left, Right;

    public ChallengeNode(int difficulty, string type)
    {
        Difficulty = difficulty;
        Type = type;
    }
}

public class ChallengeTree
{
    public ChallengeNode Root;

    public void Insert(int difficulty, string type)
    {
        Root = Insert(Root, difficulty, type);
    }

    private ChallengeNode Insert(ChallengeNode node, int difficulty, string type)
    {
        if (node == null)
            return new ChallengeNode(difficulty, type);

        if (difficulty < node.Difficulty)
            node.Left = Insert(node.Left, difficulty, type);
        else
            node.Right = Insert(node.Right, difficulty, type);

        return node;
    }

    public ChallengeNode FindClosest(int targetDifficulty)
    {
        return FindClosest(Root, targetDifficulty, Root);
    }

    private ChallengeNode FindClosest(ChallengeNode node, int target, ChallengeNode closest)
    {
        if (node == null)
            return closest;

        if (Math.Abs(node.Difficulty - target) < Math.Abs(closest.Difficulty - target))
            closest = node;

        if (target < node.Difficulty)
            return FindClosest(node.Left, target, closest);
        else
            return FindClosest(node.Right, target, closest);
    }

    public void Remove(int difficulty)
    {
        Root = Remove(Root, difficulty);
    }

    private ChallengeNode Remove(ChallengeNode node, int difficulty)
    {
        if (node == null) return null;

        if (difficulty < node.Difficulty)
            node.Left = Remove(node.Left, difficulty);
        else if (difficulty > node.Difficulty)
            node.Right = Remove(node.Right, difficulty);
        else
        {
            if (node.Left == null) return node.Right;
            if (node.Right == null) return node.Left;

            ChallengeNode min = FindMin(node.Right);
            node.Difficulty = min.Difficulty;
            node.Type = min.Type;
            node.Right = Remove(node.Right, min.Difficulty);
        }

        return node;
    }

    private ChallengeNode FindMin(ChallengeNode node)
    {
        while (node.Left != null)
            node = node.Left;
        return node;
    }

    public void InOrder()
    {
        InOrder(Root);
    }

    private void InOrder(ChallengeNode node)
    {
        if (node == null) return;
        InOrder(node.Left);
        Console.WriteLine($"Difficulty: {node.Difficulty}, Type: {node.Type}");
        InOrder(node.Right);
    }
}
