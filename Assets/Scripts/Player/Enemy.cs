using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト
/// </summary>
public class Enemy : PlayerBase
{
    int _random = 0;
    bool _isGuard = false;

    protected override void Update()
    {
        base.Update();

        if (CommandManager.I.Locked)
            return;

        _random = UnityEngine.Random.Range(0, 10);

        if (_isGuard || (_random == 0 && !IsPunching))
        {
            PlayerAction(new Guard(this));

            if(_isGuard == false)
                GuardCancel();

            _isGuard = true;
            return;
        }
        if (_random == 1 && !IsPunching)
        {
            PlayerAction(new LeftPunch(this));
            return;
        }
        if (_random == 2 && !IsPunching)
        {
            PlayerAction(new RightPunch(this));
            return;
        }
        if (!IsPunching)
        {
            PlayerAction(new Idol(this));
            return;
        }

        PlayerAction(null);
    }

    private async void GuardCancel()
    {
        var random = UnityEngine.Random.Range(1, 3);
        await UniTask.Delay(TimeSpan.FromSeconds(random));
        _isGuard = false;
    }
}