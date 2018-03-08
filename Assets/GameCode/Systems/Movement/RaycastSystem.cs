using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;


public class RaycastSystem : IExecuteSystem
{
    IGroup<GameEntity> _raycasters;
    RaycastHit2D x_hit;
    RaycastHit2D y_hit;
    GameContext _context;

    public RaycastSystem(Contexts contexts)
    {
        _raycasters = contexts.game.GetGroup(GameMatcher.Raycast);
        _context = contexts.game;
    }

    public void Execute()
    {
        foreach (GameEntity e in _raycasters.GetEntities())
        {

            if (e.velocity.direction.x > 0)
            {
                 x_hit = Physics2D.Raycast(e.view.gameObject.transform.position, Vector2.right);
            }
            else if (e.velocity.direction.x < 0)
            {
                x_hit = Physics2D.Raycast(e.view.gameObject.transform.position, Vector2.left);
            }

            if (e.velocity.direction.y >= 0)
            {
                y_hit = Physics2D.Raycast(e.view.gameObject.transform.position, Vector2.up);
            }
            else if (e.velocity.direction.y < 0)
            {
                y_hit = Physics2D.Raycast(e.view.gameObject.transform.position, Vector2.down);
            }

            if ((x_hit.distance <= Mathf.Abs(e.velocity.direction.x / 8f)) && (x_hit.collider != null)) //Mathf.Abs(e.velocity.direction.x / 5f)
            {
                e.velocity.direction = new Vector2(e.velocity.direction.x * (-1.1f), (e.view.gameObject.transform.position.y - x_hit.collider.transform.position.y) * 4);  //change y.velocity //e.view.transform.position.y - x_hit.collider.position.y
            }

            if ((y_hit.distance <= Mathf.Abs(e.velocity.direction.x / 8f)) && (y_hit.collider != null))
            {
                e.velocity.direction = new Vector2(e.velocity.direction.x, e.velocity.direction.y * (-1));
            }




            if (Mathf.Abs(e.view.gameObject.transform.position.x) >= 10.0f)//(y_hit.collider == null)
            {

                //no Wall -> out of board -> score, resetBall
                if (e.view.gameObject.transform.position.x >= +10.0f)//(e.velocity.direction.x >= 0)
                {
                    _context.player1Entity.ReplaceCount(_context.player1Entity.count.count + 1); //score up P1
                    _context.ballEntity.ReplacePosition(new Vector2(0, 0));                      //reset ball position
                    _context.ballEntity.ReplaceVelocity(new Vector2(1, 0));                      //reset ball velocity
                   // _context.ballEntity.AddT_SpawnBall(_context.t_TimeEntity.t_Time.t_time + 3);
                    _context.ballEntity.ReplacePosition(new Vector2(0, 0));
                }
                else if (e.view.gameObject.transform.position.x <= -10.0f) //(e.velocity.direction.x <= 0)
                {
                    _context.player2Entity.ReplaceCount(_context.player2Entity.count.count + 1); //score up P2
                    _context.ballEntity.ReplaceVelocity(new Vector2(-1, 0));                     //reset ball velocity
                   // _context.ballEntity.AddT_SpawnBall(_context.t_TimeEntity.t_Time.t_time + 3);
                    _context.ballEntity.ReplacePosition(new Vector2(0, 0));
                }
            }
        }
    }
}