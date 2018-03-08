using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class AddTextSystem : ReactiveSystem<GameEntity>
{
    public AddTextSystem(Contexts contexts) : base(contexts.game)
    {

    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Text);
    }

    protected override bool Filter(GameEntity entity)
    {
        return  entity.hasText;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity e in entities)
        {
            GameObject go = e.view.gameObject;
            MeshRenderer mr = go.GetComponent<MeshRenderer>();
            if (mr == null) mr = go.AddComponent<MeshRenderer>();

            TextMesh tm = go.GetComponent<TextMesh>();
            if (tm == null) tm = go.AddComponent<TextMesh>();

            tm.text = e.text.text;
            tm.anchor = TextAnchor.MiddleCenter;
        }
    }
}
