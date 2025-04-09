
using System.ComponentModel.Design;

public class Edge(Room destination, string? requirementType = null, int requirementValue = 0, string? requiredItem = null)
{
    public Room Destination { get; } = destination;
    public string RequirementType { get; } = requirementType;
    public int RequirementValue { get; } = requirementValue;
    public string RequiredItem { get; } = requiredItem;

    public bool canTravel(Hero hero)
    {
        if(RequirementType == "Strength" && hero.Strength<RequirementValue)
        return false; 
        if(RequirementType == "Agility" && hero.Agility<RequirementValue)
        return false; 
         if (RequiredItem != null && !hero.HasItem(RequiredItem))
            return false;
        return true;

    }
}