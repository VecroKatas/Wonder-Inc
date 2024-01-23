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

    public void OpenOrCLose()
    {
        Transform buttonText = transform.Find("Button").Find("ButtonText");
        Text bText = buttonText.GetComponent<Text>();
        
        if (closed)
        {
            StartCoroutine(Open());
            closed = false;
            bText.text = "<";
        }
        else
        {
            StartCoroutine(Close());
            closed = true;
            bText.text = ">";
        }
    }
    
    public void OpenOrCLoseTMP()
    {
        Transform buttonText = transform.Find("Button").Find("ButtonText");
        TextMeshProUGUI bText = buttonText.GetComponent<TextMeshProUGUI>();
        
        if (closed)
        {
            StartCoroutine(Open());
            closed = false;
            bText.text = "<";
        }
        else
        {
            StartCoroutine(Close());
            closed = true;
            bText.text = ">";
        }
    }

    private IEnumerator Open()
    {
        Vector3 startPosition = transform.localPosition;
        Vector3 targetPosition = new Vector3(-_width / 2 + _width  * _screenPercent / 2, 0, 0);
        float t = 0;
        const float animationDuration = .5f;
        while (t < 1)
        {
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, Ease(t));
            t += Time.deltaTime / animationDuration;
            yield return null;
        }
    }
    
    private IEnumerator Close()
    {
        Vector3 startPosition = transform.localPosition;
        Vector3 targetPosition = new Vector3(-_width / 2 - _width * _screenPercent / 2, 0, 0);
        float t = 0;
        const float animationDuration = .5f;
        while (t < 1)
        {
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, Ease(t));
            t += Time.deltaTime / animationDuration;
            yield return null;
        }
    }

    float Ease(float t)
    {
        return 1 - Mathf.Pow(1 - t, 4);
    }
}
