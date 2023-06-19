using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// UI用スクリプト
/// </summary>
public class HPView : MonoBehaviour
{
    [SerializeField]
    PlayerBase _player;

    [SerializeField]
    Slider _slider;

    public void Awake()
    {
        _slider.maxValue = _player.HP;
        _slider.value = _slider.maxValue;
        _player.hpBar += ChangeHP;
    }

    private void ChangeHP(int hp)
    {
        _slider.value = hp;
    }
}