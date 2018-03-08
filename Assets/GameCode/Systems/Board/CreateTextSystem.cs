using UnityEngine;
using Entitas;

public class CreateTextSystem : IInitializeSystem
{
    GameContext _context;

    public CreateTextSystem(Contexts contexts)
    {
        _context = contexts.game;
    }

    public void Initialize()
    {
        GameEntity count = _context.CreateEntity();
        count.AddPosition(new Vector2(0, 0));
        count.AddText("0 : 0\n\n\n\n ??");
    }
}
