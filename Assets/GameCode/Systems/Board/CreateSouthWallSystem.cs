using UnityEngine;
using Entitas;

public class CreateSouthWallSystem : IInitializeSystem
{
    GameContext _context;
    const float y_Walls = 4.0f;

    public CreateSouthWallSystem(Contexts contexts)
    {
        _context = contexts.game; ;
    }

    public void Initialize()
    {
        GameEntity sWall = _context.CreateEntity();
        sWall.isMover = false;
        sWall.AddPosition(new Vector2(0, -(y_Walls)));
        sWall.AddSprite("Wall");
        sWall.isCollider = true;
        sWall.isRigidBody = true;
        sWall.isKinematic = true;
    }
}
