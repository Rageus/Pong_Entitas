using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class ChangeDeactiveTimeSystem : ReactiveSystem<GameEntity>
{
    GameContext _context;
    const float maxTime = 102; //99 + 3 for the initial spawn

    public ChangeDeactiveTimeSystem(Contexts contexts) : base(contexts.game)
    {
        _context = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.T_Time);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasT_Time;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach(GameEntity e in entities)
        {
            if (_context.ballEntity.hasT_SpawnBall)
            {
                if (_context.ballEntity.t_SpawnBall.t_time <= e.t_Time.t_time)
                    _context.ballEntity.RemoveT_SpawnBall();
            }
            else
            {
                _context.textEntity.ReplaceText((_context.player1Entity.count.count).ToString() + " : " + (_context.player2Entity.count.count).ToString() + "\n\n\n\n " + (maxTime - _context.t_TimeEntity.t_Time.t_time).ToString("F0"));
            }


            if (e.t_Time.t_time >= maxTime)
            {               
                _context.ballEntity.ReplaceVelocity(new Vector2(0, 0));
                _context.ballEntity.ReplacePosition(new Vector2(0, 0));

                if (_context.player1Entity.count.count > _context.player2Entity.count.count)
                {
                    _context.textEntity.ReplaceText("Left Player wins!");
                }
                else if (_context.player1Entity.count.count < _context.player2Entity.count.count)
                {
                    _context.textEntity.ReplaceText("Right Player wins!");
                }
                else
                {
                    _context.textEntity.ReplaceText("Draw!");
                }
            }
        }
    }
}
