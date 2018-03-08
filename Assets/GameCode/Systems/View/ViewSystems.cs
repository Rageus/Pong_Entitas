using Entitas;

public class ViewSystems : Feature
{
    public ViewSystems(Contexts contexts) : base("View Systems")
    {
        Add(new AddViewSystem(contexts));
        Add(new RenderSpriteSystem(contexts));
        Add(new AddTextSystem(contexts));
        Add(new AddColliderSystem(contexts));
        Add(new AddRigidBodySystem(contexts));
        Add(new SetKinematicSystem(contexts));
        Add(new SetFreezeAxisXZSystem(contexts));
        Add(new RenderPositionSystem(contexts));
    }
}
