using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : IPlayerCommand
{
    IActionable _actionable;

    public Guard(IActionable _actionable)
    {
        this._actionable = _actionable;
    }

    public void Execute()
    {
        _actionable.Guard();
    }
}