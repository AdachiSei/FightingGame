using System.Collections;
using System.Collections.Generic;
using Template.Singleton;
using UnityEngine;

public class CommandManager : SingletonMonoBehaviour<CommandManager>
{
	public bool Locked => _locked;

	Queue<IPlayerCommand> _commandBuffer = new();

	bool _locked;

	/// <summary>ICommand��List�ɓo�^���܂��B</summary>
	public void AddCommand(IPlayerCommand command)
	{
		_commandBuffer.Enqueue(command);
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
		StartCoroutine(PlayBackCoroutine());
	}


	/// <summary>List�ɓo�^���ꂽ�R�}���h�����̂܂�1�t���[�����Ƃ�Excute�����s</summary>
	IEnumerator PlayBackCoroutine()
	{
		Debug.Log("Playback Start");
		foreach (var command in _commandBuffer)
		{
			command?.Execute();
			yield return new WaitForEndOfFrame();
		}
		Debug.Log("Playback End");
		_locked = false;
	}
}
