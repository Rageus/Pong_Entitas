using Entitas;

public class MovementSystems : Feature
{
    public MovementSystems(Contexts contexts) : base("Movement Systems")
    {
        

        Add(new RaycastSystem(contexts));
       // Add(new ChangeCountSystem(contexts));
        Add(new ChangeDeactiveTimeSystem(contexts));
        Add(new MoveSystem(contexts));
    }
}
