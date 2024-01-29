using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TextUI : MonoBehaviour
{
    private bool closed = true;
    private static float _width;
    private static float _height;
    private static float _screenPercent = .25f;
    private RectTransform _rectTransform = new RectTransform();

    private void Start()
    {
        _width = Screen.width;
        _height = Screen.height;
        _rectTransform = GetComponent<RectTransform>();
        Resize();
    }

    private void Resize()
    {
        _rectTransform.sizeDelta = new Vector2(_width * _screenPercent, 0);
    }
}
