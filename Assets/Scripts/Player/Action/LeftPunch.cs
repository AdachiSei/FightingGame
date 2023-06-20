using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPunch : IPlayerCommand
{
    IActionable _actionable;
    int _hp;

    public LeftPunch(IActionable _actionable)
    {
        this._actionable = _actionable;
        _hp = _actionable.HP;
    }

    public void Execute()
    {
        _actionable.LeftPunch();
        _actionable.SetHP(_hp);
        _actionable.Dead();
    }
}