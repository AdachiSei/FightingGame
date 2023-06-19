using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Template.Constant;
using UnityEngine;

/// <summary>
/// スクリプト
/// </summary>
public class PlayerBase : MonoBehaviour, IActionable
{
    [Serializable]
    protected class PlayerData
    {
        [Header("スピード")]
        public float Speed = 2;

        [Header("体力")]
        public int HP = 10;

        [Header("ダメージ")]
        public int Damage = 1;
    }

    protected class PlayerActionsHash
    {
        public int IdolHash = 0;
        public int LeftPunchHash = 0;
        public int RightPunchHash = 0;
        public int GuardHash = 0;

        public void Init()
        {
            IdolHash = Animator.StringToHash("Idol");
            LeftPunchHash = Animator.StringToHash("LeftPunch");
            RightPunchHash = Animator.StringToHash("RightPunch");
            GuardHash = Animator.StringToHash("Guard");
        }
    }

    public int HP => _playerData.HP;
    public bool IsPunching { get; private set; } = false;

    [SerializeField]
    protected PlayerData _playerData = new ();

    [SerializeField]
    [Header("敵")]
    Transform _enemyPos;

    protected Rigidbody _rb = null;
    protected Animator _animator = null;
    protected Collider _collider = null;
    protected PlayerActionsHash _playerActionHash = new ();
    private bool IsGod = false;

    public Action<int> hpBar;

    private void Awake()
    {
        _rb ??= GetComponent<Rigidbody>();
        _animator ??= GetComponent<Animator>();
        _collider ??= GetComponent<BoxCollider>();
        _playerActionHash.Init();
        _collider.enabled = false;
    }

    protected virtual void Update()
    {
        var dir = _enemyPos.position - transform.position;
        transform.rotation = Quaternion.LookRotation(dir);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out IActionable actionable))
        {
            if(actionable.IsPunching && !IsGod)
            {
                Damage();
            }
            else if(actionable.IsPunching)
            {
                _playerData.HP--;
                hpBar(_playerData.HP);
            }
        }
    }

    public void Idol()
    {
        _animator.Play(_playerActionHash.IdolHash);
        IsGod = false;
    }

    public async void LeftPunch()
    {
        _animator.Play(_playerActionHash.LeftPunchHash);
        IsPunching = true;
        IsGod = false;
        await UniTask.Delay(TimeSpan.FromSeconds(0.5));
        _collider.enabled = true;
        await UniTask.Delay(TimeSpan.FromSeconds(0.5));
        IsPunching = false;
        _collider.enabled = false;
    }

    public async void RightPunch()
    {
        _animator.Play(_playerActionHash.RightPunchHash);
        IsPunching = true;
        IsGod = false;
        await UniTask.Delay(TimeSpan.FromSeconds(0.5));
        _collider.enabled = true;
        await UniTask.Delay(TimeSpan.FromSeconds(0.5));
        IsPunching = false;
        _collider.enabled = false;
    }

    public void Guard()
    {
        _animator.Play(_playerActionHash.GuardHash);
        IsGod = true;
    }

    public void Damage()
    {
        _playerData.HP = 0;
        hpBar(_playerData.HP);
        var enemy = _enemyPos.position;
        var self = transform.position;
        var dir = enemy - self;
        _rb.AddForce(-dir.normalized * 100);
        Debug.Log(gameObject.name);
    }

    protected void PlayerAction(IPlayerCommand playerCommand)
    {
        IPlayerCommand command = playerCommand;
        command?.Execute();
        CommandManager.I.AddPlayerCommand(command);
    }
}