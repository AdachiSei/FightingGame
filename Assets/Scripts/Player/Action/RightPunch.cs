using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightPunch : IPlayerCommand
{
    IActionable _actionable;

    public RightPunch(IActionable _actionable)
    {
        this._actionable = _actionable;
    }

    public void Execute()
    {
        _actionable.RightPunch();
    }
}