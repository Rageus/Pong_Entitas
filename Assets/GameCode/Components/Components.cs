using UnityEngine;
using System.Collections;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine.UI;

//Input Components
[Input, Unique]
public class Key_P1Component : IComponent
{

}

[Input, Unique]
public class Key_P2Component : IComponent
{

}

[Input]
public class KeyPressComponent : IComponent
{
    public float value;
}


//Game Components
[Game, Unique]
public class Player1Component : IComponent
{

}

[Game, Unique]
public class Player2Component : IComponent
{

}

[Game, Unique]
public class BallComponent : IComponent
{

}

[Game]
public class PositionComponent : IComponent
{
    public Vector2 value;
}

[Game]
public class ViewComponent : IComponent
{
    public GameObject gameObject;
}

[Game]
public class SpriteComponent : IComponent
{
    public string name;
}

[Game]
public class ColliderComponent : IComponent
{
    //flag: true false
}

[Game]
public class RigidBodyComponent : IComponent
{
    //flag: true false
}

[Game]
public class KinematicComponent : IComponent
{
    //flag: true false
}

[Game]
public class FreezeXZAxisComponent : IComponent
{
    //flag: true false
}

[Game]
public class VelocityComponent : IComponent
{
    public Vector2 direction;
}

[Game]
public class MoverComponent : IComponent
{
    //flag: true false
}

[Game]
public class BallInGameComponent : IComponent
{
    //flag: true false
}

[Game]
public class T_SpawnBallComponent : IComponent
{
    public float t_time;
}

[Game, Unique]
public class T_TimeComponent : IComponent
{
    public float t_time;
}

[Game]
public class RaycastComponent : IComponent
{
    //flag: true false
}

[Game, Unique]
public class TextComponent : IComponent
{
    public string text;
}

[Game]
public class CountComponent : IComponent
{
    public int count;
}