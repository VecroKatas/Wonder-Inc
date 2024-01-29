using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollHandle : MonoBehaviour
{
    [SerializeField] private GameObject CommentsPanel; 
    
    private RectTransform _panelRectTransform;
    private Scrollbar scrollbar;
    private float _height = Screen.height - 70;
    private void Awake()
    {
        scrollbar = transform.GetComponent<Scrollbar>();
        _panelRectTransform = CommentsPanel.GetComponent<RectTransform>();
    }

    private void Update()
    {
        ChangeSize();
        
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            if (scrollbar.value + Input.GetAxis("Mouse ScrollWheel") > 1f)
                scrollbar.value = 1f;
            else if (scrollbar.value + Input.GetAxis("Mouse ScrollWheel") < 0f)
                scrollbar.value = 0f;
            else
                scrollbar.value += Input.GetAxis("Mouse ScrollWheel");
        }
    }

    private void ChangeSize()
    {
        if (_panelRectTransform.sizeDelta.y < _height)
            scrollbar.size = 1f;
        else
            scrollbar.size = _height / _panelRectTransform.sizeDelta.y;
    }
}
