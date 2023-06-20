using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スクリプト
/// </summary>
public class StartCommand : MonoBehaviour
{
    [SerializeField]
    HPView _playerHP;

    [SerializeField]
    HPView _enemyHP;

    [SerializeField]
    Image _winPanel;

    [SerializeField]
    Image _losePanel;

    [SerializeField]
    Image _panel;

    public async void Replay()
    {
        _playerHP.Init();
        _enemyHP.Init();
        _winPanel.gameObject.SetActive(false);
        _losePanel.gameObject.SetActive(false);
        CommandManager.I.PlayBack();
        await UniTask.WaitUntil(() => CommandManager.I.IsEnd == true);
        _panel.gameObject.SetActive(true);
    }
}
