using UnityEngine;
using Entitas;

public class CreatePlayer2System : IInitializeSystem
{
    GameContext _context;
    const float x_Player = 6.0f;

    public CreatePlayer2System(Contexts contexts)
    {
        _context = contexts.game;
    }

    public void Initialize()
    {
        GameEntity P2 = _context.CreateEntity();
        P2.isMover = true;
        P2.AddPosition(new Vector2(x_Player, 0));
        P2.AddVelocity(new Vector2(0, 0));
        P2.AddSprite("Player2");
        P2.isCollider = true;
        P2.isRigidBody = true;
        P2.isKinematic = false;
        P2.isFreezeXZAxis = true;
        P2.isPlayer2 = true;
        P2.AddCount(0);
    }
}
