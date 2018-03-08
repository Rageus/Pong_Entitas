using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class CreateMiddleLineSystem : IInitializeSystem
{
    GameContext _context;

    public CreateMiddleLineSystem(Contexts contexts)
    {
        _context = contexts.game;
    }

    public void Initialize()
    {
        GameEntity nWall = _context.CreateEntity();
        nWall.AddPosition(new Vector2(0, 0));
        nWall.AddSprite("MiddleLine");
    }
}
