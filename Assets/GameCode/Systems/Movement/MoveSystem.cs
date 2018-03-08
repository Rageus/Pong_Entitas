using UnityEngine;
using Entitas;

public class MoveSystem : IExecuteSystem
{
    IGroup<GameEntity> _moves;
    float _Speed = 200.0f;

    public MoveSystem(Contexts contexts)
    {
        _moves = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Velocity, GameMatcher.RigidBody, GameMatcher.View).NoneOf(GameMatcher.T_SpawnBall));
    }

    public void Execute()
    {
        foreach (GameEntity e in _moves.GetEntities())
        {
            Vector2 newVelocity = e.velocity.direction * Time.deltaTime * _Speed;
            GameObject go = e.view.gameObject;
            Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
            rb.velocity = newVelocity;
        }
    }
}
