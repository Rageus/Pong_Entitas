using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class BoardSystems : Feature
{
    public BoardSystems(Contexts contexts) : base("Board Systems")
    {
        Add(new CreateTextSystem(contexts));
        //Add(new CreateMiddleLineSystem(contexts));
        Add(new CreateSouthWallSystem(contexts));
        Add(new CreateNorthWallSystem(contexts));
        Add(new CreatePlayer1System(contexts));
        Add(new CreatePlayer2System(contexts));
        Add(new CreateBallSystem(contexts));
    }
}
