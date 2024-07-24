namespace APILivraria.Models;

public abstract class Entity
{
    public int Id { get; set; }

    protected Entity()
    {
        NumSey numSey = new NumSey();
    }
}

internal class NumSey
{
    public int MyProperty { get; set; }
}
