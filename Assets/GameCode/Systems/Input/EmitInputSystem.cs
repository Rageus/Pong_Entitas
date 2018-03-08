using UnityEngine;
using Entitas;

public class EmitInputSystem : IInitializeSystem, IExecuteSystem
{
    InputContext _context;
    InputEntity _key1Entity;
    InputEntity _key2Entity;

    public EmitInputSystem(Contexts contexts)
    {
        _context = contexts.input;
    }

    public void Initialize()
    {
        _key1Entity = _context.CreateEntity();
        _key1Entity.isKey_P1 = true;
        _key1Entity.AddKeyPress(0.0f);
        _key2Entity = _context.CreateEntity();
        _key2Entity.isKey_P2 = true;
        _key2Entity.AddKeyPress(0.0f);
    }

    public void Execute()
    {
        if ((Input.GetKey(KeyCode.A)) && !(Input.GetKey(KeyCode.Y)))
            _key1Entity.ReplaceKeyPress(2.0f);
        else if (!(Input.GetKey(KeyCode.A)) && (Input.GetKey(KeyCode.Y)))
            _key1Entity.ReplaceKeyPress(-2.0f);
        else if (!(Input.GetKey(KeyCode.A)) && !(Input.GetKey(KeyCode.Y)))
            _key1Entity.ReplaceKeyPress(0.0f);

        if ((Input.GetKey(KeyCode.UpArrow)) && !(Input.GetKey(KeyCode.DownArrow)))
            _key2Entity.ReplaceKeyPress(2.0f);
        else if (!(Input.GetKey(KeyCode.UpArrow)) && (Input.GetKey(KeyCode.DownArrow)))
            _key2Entity.ReplaceKeyPress(-2.0f);
        else if (!(Input.GetKey(KeyCode.UpArrow)) && !(Input.GetKey(KeyCode.DownArrow)))
            _key2Entity.ReplaceKeyPress(0.0f);
    }
}
