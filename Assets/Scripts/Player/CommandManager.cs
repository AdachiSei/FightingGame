using System.Collections;
using System.Collections.Generic;
using Template.Singleton;
using UnityEngine;

public class CommandManager : SingletonMonoBehaviour<CommandManager>
{
    public bool Locked => _locked;

    private Queue<IPlayerCommand> _playerCommandBuffer = new();
    private Queue<IPlayerCommand> _enemyCommandBuffer = new();

    bool _locked;

    public void Init()
    {
        _playerCommandBuffer = new();
        _enemyCommandBuffer = new();
        _locked = false;
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
    public void PlayBack()
    {
        // ロックされているときは何もしない
        if (_locked) return;

        _locked = true;
        StartCoroutine(PlayBackCoroutinePlayer());
        StartCoroutine(PlayBackCoroutineEnemy());
    }


    /// <summary>Listに登録されたコマンドをそのまま1フレームごとにExcuteを実行</summary>
    IEnumerator PlayBackCoroutinePlayer()
    {
        Debug.Log("Playback Start");
        foreach (var command in _playerCommandBuffer)
        {
            command?.Execute();
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("Playback End");
    }

    IEnumerator PlayBackCoroutineEnemy()
    {
        Debug.Log("Playback Start");
        foreach (var command in _enemyCommandBuffer)
        {
            command?.Execute();
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("Playback End");
    }
}
