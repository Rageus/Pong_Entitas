using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class SetFreezeAxisXZSystem : ReactiveSystem<GameEntity>
{
    public SetFreezeAxisXZSystem(Contexts contexts) : base(contexts.game)
    {

    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.FreezeXZAxis);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isRigidBody && entity.isFreezeXZAxis;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity e in entities)
        {
            GameObject go = e.view.gameObject;
            Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        }
    }
}
