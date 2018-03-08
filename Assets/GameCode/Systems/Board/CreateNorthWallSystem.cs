using UnityEngine;
using Entitas;

public class CreateNorthWallSystem : IInitializeSystem
{
    GameContext _context;
    const float y_Walls = 4.0f;

    public CreateNorthWallSystem(Contexts contexts)
    {
        _context = contexts.game;
    }

    public void Initialize ()
    {
        GameEntity nWall = _context.CreateEntity();
        nWall.isMover = false;
        nWall.AddPosition(new Vector2(0, y_Walls));
        nWall.AddSprite("Wall");
        nWall.isCollider = true;
        nWall.isRigidBody = true;
        nWall.isKinematic = true;
    }
}
