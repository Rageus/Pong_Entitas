using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class CreateBallSystem : IInitializeSystem
{
    GameContext _context;
    const float spawnAfter = -3;

    public CreateBallSystem(Contexts contexts)
    {
        _context = contexts.game;
    }


    public void Initialize()
    {      
        GameEntity Ball = _context.CreateEntity();
        Ball.isBall = true;
        Ball.isMover = true;
        Ball.AddVelocity(new Vector2(1.0f, 0.0f));
        Ball.AddPosition(new Vector2(0, 0));
        Ball.AddSprite("Ball");
        Ball.isCollider = false;
        Ball.isRigidBody = true;
        Ball.isKinematic = false;
        Ball.isFreezeXZAxis = false;
        Ball.isRaycast = true;
        Ball.AddT_SpawnBall(3);
    }
}
