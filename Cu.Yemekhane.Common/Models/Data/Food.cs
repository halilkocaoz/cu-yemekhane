namespace Cu.Yemekhane.Common.Models.Data;

public class Food
{
    public Food(string name, int calories)
    {
        Name = name;
        Calories = calories;
    }

    public string Name { get; private set; }
    public int Calories { get; private set; }

    public override string ToString()
    {
        return $"{Name} - {Calories} kalori.";
    }
}