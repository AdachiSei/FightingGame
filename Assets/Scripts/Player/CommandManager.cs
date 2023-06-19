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

    /// <summary>ICommand��List�ɓo�^���܂��B</summary>
    public void AddPlayerCommand(IPlayerCommand command)
    {
        _playerCommandBuffer.Enqueue(command);
    }

    public void AddEnemyCommand(IPlayerCommand command)
    {
        _enemyCommandBuffer.Enqueue(command);
    }

    /// <summary>
    /// PlayBack�p�̃R���[�`�������s���܂��B
    /// ���s���͂�����Ȃ��悤�ɂ��邽�߁A����Return
    /// </summary>
    public void PlayBack()
    {
        // ���b�N����Ă���Ƃ��͉������Ȃ�
        if (_locked) return;

        _locked = true;
        StartCoroutine(PlayBackCoroutinePlayer());
        StartCoroutine(PlayBackCoroutineEnemy());
    }


    /// <summary>List�ɓo�^���ꂽ�R�}���h�����̂܂�1�t���[�����Ƃ�Excute�����s</summary>
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
