using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class SetKinematicSystem : ReactiveSystem<GameEntity>
{
    public SetKinematicSystem(Contexts contexts) : base(contexts.game)
    {

    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Kinematic);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isRigidBody && entity.isKinematic;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity e in entities)
        {
            GameObject go = e.view.gameObject;
            Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
            rb.isKinematic = true; //deaktivate the physics.... it's
                                   // not the component!!!
        }
    }
}
