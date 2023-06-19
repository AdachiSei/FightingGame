using System.Collections;
using System.Collections.Generic;
using Template.Singleton;
using UnityEngine;

public class CommandManager : SingletonMonoBehaviour<CommandManager>
{
	public bool Locked => _locked;

	Queue<IPlayerCommand> _commandBuffer = new();

	bool _locked;

	/// <summary>ICommandをListに登録します。</summary>
	public void AddCommand(IPlayerCommand command)
	{
		_commandBuffer.Enqueue(command);
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
		StartCoroutine(PlayBackCoroutine());
	}


	/// <summary>Listに登録されたコマンドをそのまま1フレームごとにExcuteを実行</summary>
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
