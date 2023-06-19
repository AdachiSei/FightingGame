using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スクリプト
/// </summary>
public class Explanation : MonoBehaviour
{
    [SerializeField]
    Image _pamel;

    bool _isExplanation = false;

    public void OnExplanation()
    {
        _isExplanation = !_isExplanation;

        if (_isExplanation)
            _pamel.gameObject.SetActive(false);

        else
            _pamel.gameObject.SetActive(true);
    }
}