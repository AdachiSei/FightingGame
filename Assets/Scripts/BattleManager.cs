using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スクリプト
/// </summary>
public class BattleManager : MonoBehaviour
{
    [SerializeField]
    PlayerBase _player;

    [SerializeField]
    PlayerBase _enemy;

    [SerializeField]
    Image _playerPanel;

    [SerializeField]
    Image _enemyPanel;

    bool _isEnd;

    private void Awake()
    {
        Player();
        Enemy();
    }

    private async void Player()
    {
        await UniTask.WaitUntil(() => _player.HP <= 0);

        if (_isEnd)
            return;
        _isEnd = true;

        PlayerResult();
    }

    private async void Enemy()
    {
        await UniTask.WaitUntil(() => _enemy.HP <= 0);

        if (_isEnd)
            return;
        _isEnd = true;

        EnemyResult();
    }

    private void PlayerResult()
    {
        _playerPanel.gameObject.SetActive(true);
    }

    private void EnemyResult()
    {
        _enemyPanel.gameObject.SetActive(true);
    }
}