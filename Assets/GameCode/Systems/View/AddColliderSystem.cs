using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class AddColliderSystem : ReactiveSystem<GameEntity>
{
    public AddColliderSystem(Contexts contexts) : base(contexts.game)
    {

    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Collider);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasView && entity.isCollider;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach(GameEntity e in entities)
        {
            GameObject go = e.view.gameObject;
            BoxCollider2D co = go.GetComponent<BoxCollider2D>();
            if (co == null) co = go.AddComponent<BoxCollider2D>();
        }
    }
}
