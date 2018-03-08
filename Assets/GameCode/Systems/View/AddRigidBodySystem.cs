using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class AddRigidBodySystem : ReactiveSystem<GameEntity>
{
    public AddRigidBodySystem(Contexts contexts) : base(contexts.game)
    {

    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.RigidBody);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasView && entity.isRigidBody;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach(GameEntity e in entities)
        {
            GameObject go = e.view.gameObject;
            Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
            if (rb == null) rb = go.AddComponent<Rigidbody2D>();

            rb.gravityScale = 0; //deactivates gravity
        }
    }
}
