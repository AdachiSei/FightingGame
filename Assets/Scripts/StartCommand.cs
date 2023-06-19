using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト
/// </summary>
public class StartCommand : MonoBehaviour
{
    private void Awake()
    {
        CommandManager.I.PlayBack();
    }
}