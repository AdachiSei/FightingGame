using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idol : IPlayerCommand
{
    IActionable _actionable;
    int _hp;

    public Idol(IActionable _actionable)
    {
        this._actionable = _actionable;
        _hp = _actionable.HP;
    }

    public void Execute()
    {
        _actionable.Idol();
        _actionable.SetHP(_hp);
        _actionable.Dead();
    }
}