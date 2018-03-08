using UnityEngine;
using Entitas;
using System.Collections;

public class CreateTimeSystem : IInitializeSystem, IExecuteSystem
{
    GameContext _context;
    GameEntity T_Time;
    //GameEntity T_SpawnBall;

    public CreateTimeSystem(Contexts contexts)
    {
        _context = contexts.game;
        T_Time = _context.CreateEntity();
        //T_SpawnBall = _context.CreateEntity();

    }

    public void Initialize()
    {
        T_Time.AddT_Time(0.0f);
        //T_SpawnBall.AddT_SpawnBall(-3.0f);
    }

    public void Execute()
    {
        T_Time.ReplaceT_Time(T_Time.t_Time.t_time + Time.deltaTime);
        //T_SpawnBall.ReplaceT_SpawnBall(T_SpawnBall.t_SpawnBall.t_time + Time.deltaTime);
    }
}
