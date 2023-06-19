using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idol : IPlayerCommand
{
    IActionable _actionable;

    public Idol(IActionable _actionable)
    {
        this._actionable = _actionable;
    }

    public void Execute()
    {
        _actionable.Idol();
    }
}