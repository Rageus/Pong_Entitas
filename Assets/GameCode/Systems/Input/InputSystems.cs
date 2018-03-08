using Entitas;

public class InputSystems : Feature
{
    public InputSystems(Contexts contexts) : base("Input Systems")
    {
        Add(new EmitInputSystem(contexts));
        Add(new CommandMoveP1System(contexts));
        Add(new CommandMoveP2System(contexts));
    }
}
