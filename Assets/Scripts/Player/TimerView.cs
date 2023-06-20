using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;
using System;

/// <summary>
/// UI用スクリプト
/// </summary>
public class TimerView : MonoBehaviour
{
    [SerializeField]
    Text _timerText;

    private void Awake()
    {
        _timerText.text = "3";
        StartCount();
    }

    private async void StartCount()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        _timerText.text = "2";
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        _timerText.text = "1";
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        _timerText.gameObject.SetActive(false);
    }
}