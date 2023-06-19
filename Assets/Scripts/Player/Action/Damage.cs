using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : IPlayerCommand
{
    IActionable _actionable;

    public Damage(IActionable _actionable)
    {
        this._actionable = _actionable;
    }

    public void Execute()
    {
        _actionable.Damage();
    }
}