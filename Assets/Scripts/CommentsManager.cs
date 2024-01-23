using System;
using System.Collections;
using System.Collections.Generic;
//using Ink.Runtime;
using TMPro;
using UnityEngine;

public class CommentManager : MonoBehaviour
{
    private static CommentManager instance;
    
    [SerializeField] private GameObject CommentsPanel;

    private CommentsPanel CommentsPanelObject;
    public bool commentsGenerating = false;

    private void Awake()
    {
        if (instance != null) Debug.LogWarning("Found more than 1 Dialogue Manager");
        instance = this;
        CommentsPanelObject = CommentsPanel.GetComponent<CommentsPanel>();
    }

    public static CommentManager GetInstance()
    {
        return instance;
    }

    public void StartPauseComments()
    {
        if (!commentsGenerating)
        {
            commentsGenerating = true;
            StartCoroutine(StartCommentFlow(1));
        }
        else
        {
            commentsGenerating = false;
        }
    }

    IEnumerator StartCommentFlow(int interval)
    {
        while (commentsGenerating)
        {
            CommentsPanel.GetComponent<CommentsPanel>().NewNode();
            yield return new WaitForSeconds(interval);
        }
    }
}
