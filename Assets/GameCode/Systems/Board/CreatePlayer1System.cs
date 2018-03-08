using UnityEngine;
using Entitas;

public class CreatePlayer1System : IInitializeSystem
{
    GameContext _context;
    const float x_Player = 6.0f;

    public CreatePlayer1System(Contexts contexts)
    {
        _context = contexts.game;
    }

    public void Initialize()
    {
        GameEntity P1 = _context.CreateEntity();
        P1.isMover = true;
        P1.AddPosition(new Vector2(-(x_Player), 0));
        P1.AddVelocity(new Vector2(0, 0));
        P1.AddSprite("Player1");
        P1.isCollider = true;
        P1.isRigidBody = true;
        P1.isKinematic = false;
        P1.isFreezeXZAxis = true;
        P1.isPlayer1 = true;
        P1.AddCount(0);
    }
}
