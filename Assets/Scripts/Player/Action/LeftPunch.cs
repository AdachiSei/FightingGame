using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPunch : IPlayerCommand
{
    IActionable _actionable;

    public LeftPunch(IActionable _actionable)
    {
        this._actionable = _actionable;
    }

    public void Execute()
    {
        _actionable.LeftPunch();
    }
}