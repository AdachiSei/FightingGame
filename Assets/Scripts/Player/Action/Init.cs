using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : IPlayerCommand
{
    IActionable _actionable;

    public Init(IActionable _actionable)
    {
        this._actionable = _actionable;
    }

    public void Execute()
    {
        _actionable.Init();
    }
}
