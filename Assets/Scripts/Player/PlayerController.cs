using System;
using System.Collections;
using System.Collections.Generic;
using Template.Constant;
using UnityEngine;

/// <summary>
/// スクリプト
/// </summary>
public class PlayerController : MonoBehaviour, IActionable
{
    [Serializable]
    private class PlayerData
    {
        [Header("スピード")]
        public float Speed = 2;

        [Header("体力")]
        public int HP = 20;

        [Header("ダメージ")]
        public int Damage = 2;
    }

    private class PlayerActionsHash
    {
        public int LeftPunchHash = 0;
        public int RightPunchHash = 0;
        public int GuardHash = 0;

        public void Init()
        {
            LeftPunchHash = Animator.StringToHash("LeftPunch");
            RightPunchHash = Animator.StringToHash("RightPunch");
            GuardHash = Animator.StringToHash("Guard");
        }
    }

    [SerializeField]
    PlayerData _playerData = new ();

    Rigidbody _rb = null;
    Animator _animator = null;
    PlayerActionsHash _playerActionHash = new ();

    private void Awake()
    {
        _rb ??= GetComponent<Rigidbody>();
        _animator ??= GetComponent<Animator>();
        _playerActionHash.Init();
    }

    private void Update()
    {
        if (CommandManager.I.Locked)
            return;

        var leftButton = Input.GetButtonDown(InputName.FIRE1);
        var rightButton = Input.GetButtonDown(InputName.FIRE2);
        var spaceButton = Input.GetButton(InputName.JUMP);

        if(spaceButton)
        {
            PlayerAction(new Guard(this));
            return;
        }
        if (leftButton)
        {
            //PlayerAction(new LeftPunch(this));
            return;
        }
        if (rightButton)
        {
            PlayerAction(new RightPunch(this));
            return;
        }
        PlayerAction(null);
    }

    public void LeftPunch()
    {
        _animator.Play(_playerActionHash.LeftPunchHash);
    }

    public void RightPunch()
    {
        _animator.Play(_playerActionHash.RightPunchHash);
    }

    public void Guard()
    {
        _animator.Play(_playerActionHash.GuardHash);
    }

    public void Damage()
    {

    }

    private void PlayerAction(IPlayerCommand playerCommand)
    {
        IPlayerCommand command = playerCommand;
        command?.Execute();
        CommandManager.I.AddCommand(command);
    }
}