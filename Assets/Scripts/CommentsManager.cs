using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Classes;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class CommentManager : MonoBehaviour
{
    public bool commentsGenerating = false;
    public int CommentsGenerationInterval = 2;
    
    private static CommentManager instance;
    
    [SerializeField] private GameObject CommentsPanel;
    [SerializeField] private TextAsset accountsTextAsset;
    [SerializeField] private TextAsset commentsTextAsset;

    private List<string> accountsTextList;
    private List<string> commentsTextList;

    private List<Account> accountsList;
    private List<Comment> commentsList;
    
    private CommentsPanel CommentsPanelObject;

    private Random random = new Random();

    private void Awake()
    {
        if (instance != null) Debug.LogWarning("Found more than 1 Dialogue Manager");
        instance = this;
        CommentsPanelObject = CommentsPanel.GetComponent<CommentsPanel>();

        commentsTextList = commentsTextAsset.text.Split('\n').ToList();
        accountsTextList = accountsTextAsset.text.Split('\n').ToList();

        accountsList = new List<Account>();
        commentsList = new List<Comment>();
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
            StartCoroutine(StartCommentFlow(CommentsGenerationInterval));
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
            Account account = NewAccount(accountsTextList[random.Next(accountsTextList.Count)]);
            CommentsPanel.GetComponent<CommentsPanel>().NewNode(account, NewComment(account, commentsTextList[random.Next(commentsTextList.Count)]));
            yield return new WaitForSeconds(interval);
        }
    }

    private Account NewAccount(string accountName)
    {
        Account newAccount = accountsList.Find(c => c.AccountName == accountName);
        
        if (newAccount == null)
        {
            newAccount = new Account(accountName, random.Next(-2, 2));
            accountsList.Add(newAccount);
        }

        return newAccount;
    }
    
    private Comment NewComment(Account account, string commentText)
    {
        List<Comment> identicalCommentsList = commentsList.FindAll(c => c.CommentText == commentText);
        
        if (identicalCommentsList.Count == 0)
        {
            foreach (var c in identicalCommentsList)
                if (c.Account.AccountName == account.AccountName)
                    return c;
        }

        return new Comment(account, commentText, CommentsPanel);
    }
}
