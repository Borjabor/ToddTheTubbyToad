using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuLevelSelect : MenuBase
{
    [Tooltip("Add how many scenes are in front of the first level in the build scenes")]
    [SerializeField]
    private int _indexFixer;

    [SerializeField]
    private Button[] _button;

    private void Awake()
    {
        int i = 0 + _indexFixer;
        foreach (var button in _button)
        {
            var i1 = i;
            button.onClick.AddListener(() => _levelLoader.LoadSpecificLevel(i1));
            i++;
            Debug.Log($"{i}");
        }
    }
}