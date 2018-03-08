//using Entitas;
//using UnityEngine;
//using System.Collections.Generic;

//public class ChangeCountSystem : ReactiveSystem<GameEntity>
//{
//    GameContext _context;

//    public ChangeCountSystem(Contexts contexts) : base(contexts.game)
//    {
//        _context = contexts.game;
//    }

//    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
//    {
//        return context.CreateCollector(GameMatcher.Count);
//    }

//    protected override bool Filter(GameEntity entity)
//    {
//        return entity.hasCount;
//    }

//    protected override void Execute(List<GameEntity> entities)
//    {
//        foreach (GameEntity e in entities)
//        {
//            //_context.textEntity.ReplaceText((_context.player1Entity.count.count).ToString() + " : " + (_context.player2Entity.count.count).ToString() + "\n");
//        }
//    }
//}
