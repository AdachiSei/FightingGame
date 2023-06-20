using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト
/// </summary>
public class StartCommand : MonoBehaviour
{
　　　[SerializeField]
    HPView _playerHP;

    [SerializeField]
    HPView _enemyHP;
    
    private void Awake()
    {
    　　　_playerHP.Init();
       _enemyHP.Init();
        CommandManager.I.PlayBack();
    }
}
