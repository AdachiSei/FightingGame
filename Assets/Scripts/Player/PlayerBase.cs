using Cysharp.Threading.Tasks;
using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

/// <summary>
/// スクリプト
/// </summary>
public class PlayerBase : MonoBehaviour, IActionable
{
    [Serializable]
    protected class PlayerData
    {
        [Header("体力")]
        public int HP = 20;

        [Header("ダメージ")]
        public int Damage = 1;
    }

    protected class PlayerActionsHash
    {
        public int IdolHash = 0;
        public int LeftPunchHash = 0;
        public int RightPunchHash = 0;
        public int GuardHash = 0;
        public int DeadHash = 0;

        public void Init()
        {
            IdolHash = Animator.StringToHash("Idol");
            LeftPunchHash = Animator.StringToHash("LeftPunch");
            RightPunchHash = Animator.StringToHash("RightPunch");
            GuardHash = Animator.StringToHash("Guard");
            DeadHash = Animator.StringToHash("Dead");
        }
    }

    public int HP => _playerData.HP;
    public bool IsPunching { get; private set; } = false;

    [SerializeField]
    protected PlayerData _playerData = new();

    [SerializeField]
    protected bool _isPlayer = false;

    [SerializeField]
    [Header("敵")]
    Transform _enemyPos;

    protected Rigidbody _rb = null;
    protected Animator _animator = null;
    protected Collider _collider = null;
    protected PlayerActionsHash _playerActionHash = new();
    private bool IsGod = false;
    private Vector3 _defaultPos;
    private int _defaultHP;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();

    public Action<int> hpBar;

    private void Awake()
    {
        _defaultPos = transform.position;
        _defaultHP = _playerData.HP;
        var dir = _enemyPos.position - transform.position;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    private async void Start()
    {
        _rb ??= GetComponent<Rigidbody>();
        _animator ??= GetComponent<Animator>();
        _collider ??= GetComponent<BoxCollider>();
        _playerActionHash.Init();
        _collider.enabled = false;
        PlayerAction(new Init(this));

        await UniTask.Delay(TimeSpan.FromSeconds(3f));

        this
            .UpdateAsObservable()
            .Subscribe(_ => OnUpdate())
            .AddTo(_compositeDisposable);
    }

    protected virtual void OnUpdate()
    {
        var dir = _enemyPos.position - transform.position;
        transform.rotation = Quaternion.LookRotation(dir);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IActionable actionable))
        {
            if (actionable.IsPunching && IsGod)
            {
                AddForce();
                actionable.AddForce(false);
            }
            else if (actionable.IsPunching && !IsGod)
            {
                if (!CommandManager.I.Locked)
                    _playerData.HP--;

                hpBar(_playerData.HP);
                Dead();
                Debug.Log(_playerData.HP);
            }
            if (actionable.HP <= 0)
                _compositeDisposable.Dispose();
        }
    }

    public void Init()
    {
        transform.position = _defaultPos;
        _playerData.HP = _defaultHP;
        _animator.Play(_playerActionHash.IdolHash);
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

    public async void AddForce(bool _isDamege = true)
    {
        var force = _isPlayer ? new Vector3(0, 0, -20) : new Vector3(0, 0, 20);

        if (!_isDamege)
            force *= -1;

        _rb.AddForce(force);

        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));

        _rb.velocity = Vector3.zero;
        Dead();
    }

    public void SetHP(int hp)
    {
        _playerData.HP = hp;
        hpBar(_playerData.HP);
    }
    public void Dead()
    {
        if (_playerData.HP > 0)
            return;

        _animator.Play(_playerActionHash.DeadHash);
        _compositeDisposable.Dispose();
    }

    protected void PlayerAction(IPlayerCommand playerCommand)
    {
        IPlayerCommand command = playerCommand;
        command?.Execute();
        CommandManager.I.AddPlayerCommand(command);
    }

}
