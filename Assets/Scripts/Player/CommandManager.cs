using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Template.Singleton;
using UnityEngine;

public class CommandManager : SingletonMonoBehaviour<CommandManager>
{
    public bool Locked => _locked;
    public bool IsEnd { get; private set; } = false;

    private Queue<IPlayerCommand> _playerCommandBuffer = new();
    private Queue<IPlayerCommand> _enemyCommandBuffer = new();

    bool _locked;
    bool _isPlayer;
    bool _isEnemy;

    public void Init()
    {
        _playerCommandBuffer = new();
        _enemyCommandBuffer = new();
        _locked = false;
        IsEnd = false;
        _isPlayer = false;
        _isEnemy = false;
    }

    /// <summary>ICommandをListに登録します。</summary>
    public void AddPlayerCommand(IPlayerCommand command)
    {
        _playerCommandBuffer.Enqueue(command);
    }

    public void AddEnemyCommand(IPlayerCommand command)
    {
        _enemyCommandBuffer.Enqueue(command);
    }

    /// <summary>
    /// PlayBack用のコルーチンを実行します。
    /// 実行中はいじれないようにするため、早期Return
    /// </summary>
    public async void PlayBack()
    {
        // ロックされているときは何もしない
        if (_locked) return;

        _locked = true;
        PlayBackCoroutinePlayer();
        PlayBackCoroutineEnemy();

        await UniTask.WaitUntil(() => _isPlayer);
        await UniTask.WaitUntil(() => _isEnemy);
        IsEnd = true;
    }


    /// <summary>Listに登録されたコマンドをそのまま1フレームごとにExcuteを実行</summary>
    private async void PlayBackCoroutinePlayer()
    {
        Debug.Log("Playback Start");
        foreach (var command in _playerCommandBuffer)
        {
            command?.Execute();
            //await UniTask.Yield(PlayerLoopTiming.Update);
            await UniTask.Delay(TimeSpan.FromSeconds(Time.deltaTime));
        }
        Debug.Log("Playback End");
        _isPlayer = true;
    }

    private async void PlayBackCoroutineEnemy()
    {
        Debug.Log("Playback Start");
        foreach (var command in _enemyCommandBuffer)
        {
            command?.Execute();
            //await UniTask.Yield(PlayerLoopTiming.Update);
            await UniTask.Delay(TimeSpan.FromSeconds(Time.deltaTime));
        }
        Debug.Log("Playback End");
        _isEnemy = true;
    }
}
