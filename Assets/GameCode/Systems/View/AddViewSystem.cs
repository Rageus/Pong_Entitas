using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class AddViewSystem : ReactiveSystem<GameEntity>
{
    Transform _viewContainer = new GameObject("Game Views").transform;
    GameContext _context;
    string name;

    public AddViewSystem(Contexts contexts) : base(contexts.game)
    {
        _context = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AnyOf(GameMatcher.Sprite, GameMatcher.Text));
    }

    protected override bool Filter(GameEntity entity)
    {
        return (entity.hasSprite || entity.hasText)&& !entity.hasView;
    }

    protected override void Execute(List<GameEntity> entities)
    {


        foreach (var e in entities)
        {

            if (e.hasSprite)
            {
                name = e.sprite.name;
            }
            else if (e.hasText)
            {
                name = "Text";
            }
            else
            {
                name = "Game View";
            }

            GameObject go = new GameObject(name);
            go.transform.SetParent(_viewContainer, false);
            e.AddView(go);
            go.Link(e, _context);
        }
    }
}
