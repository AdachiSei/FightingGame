using System.Collections;
using System.Collections.Generic;
using Template.Constant;
using UnityEngine;

/// <summary>
/// スクリプト
/// </summary>
public class PlayerController : PlayerBase
{
    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (CommandManager.I.Locked)
            return;

        var leftButton = Input.GetButtonDown(InputName.FIRE1);
        var rightButton = Input.GetButtonDown(InputName.FIRE2);
        var spaceButton = Input.GetButton(InputName.JUMP);

        if (spaceButton && !IsPunching)
        {
            PlayerAction(new Guard(this));
            return;
        }
        if (leftButton && !IsPunching)
        {
            PlayerAction(new LeftPunch(this));
            return;
        }
        if (rightButton && !IsPunching)
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
}