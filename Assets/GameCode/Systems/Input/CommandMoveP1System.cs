using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class CommandMoveP1System : ReactiveSystem<InputEntity>
{
    GameContext _context;


    public CommandMoveP1System(Contexts contexts) : base(contexts.input)
    {
        _context = contexts.game;
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.KeyPress);
    }

    protected override bool Filter(InputEntity entity)
    {
        return entity.hasKeyPress && (entity.isKey_P1);
    }

    protected override void Execute(List<InputEntity> entities)
    {
        foreach (InputEntity e in entities)
        {
            _context.player1Entity.velocity.direction = new Vector2(0, e.keyPress.value);
        }
    }
}
