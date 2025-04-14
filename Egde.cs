public class Edge
{
    public Room Destination { get; }
    public string RequirementType { get; }
    public int RequirementValue { get; }
    public string RequiredItem { get; }

    public Edge(Room destination, string? requirementType = null, int requirementValue = 0, string? requiredItem = null)
    {
        Destination = destination;
        RequirementType = requirementType;
        RequirementValue = requirementValue;
        RequiredItem = requiredItem;
    }

    public bool CanTravel(Hero hero)
    {
        if (RequirementType == "Strength" && hero.Strength < RequirementValue)
            return false;
        if (RequirementType == "Agility" && hero.Agility < RequirementValue)
            return false;
        if (RequiredItem != null && !hero.HasItem(RequiredItem))
            return false;

        return true;
    }
}
