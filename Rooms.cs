using System.Security.Cryptography;

public class Room
{
    public int Number {get; set;}
    public List<Edge> Connections {get;} = new List<Edge>(); 
    public Room(int number)
    {
        Number = number; 
    }
}