using Entitas;

public class ArrowOfTimeSystems : Feature
{
    public ArrowOfTimeSystems(Contexts contexts) : base("Arrow of time Systems")
    {
        Add(new CreateTimeSystem(contexts));
    }
}
