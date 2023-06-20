using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : IPlayerCommand
{
    IActionable _actionable;
    int _hp;

    public Guard(IActionable _actionable)
    {
        this._actionable = _actionable;
        _hp = _actionable.HP;
    }

    public void Execute()
    {
        _actionable.Guard();
        _actionable.SetHP(_hp);
        _actionable.Dead();
    }
}