using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト
/// </summary>
public class ResetCommand : MonoBehaviour
{
    private void Awake()
    {
        CommandManager.I.Init();
    }
}