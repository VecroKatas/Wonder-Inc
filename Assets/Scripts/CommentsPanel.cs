using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommentsPanel : MonoBehaviour
{
    public GameObject activeCommentNode;
    public GameObject CommentNodePrefab;

    private RectTransform rectTransform;
    private Transform _transform;
    private List<GameObject> commentNodes = new List<GameObject>();
    private int _commentNodeIndex = -1;
    private float _scrollValue, _height = Screen.height - 70, _firstCommentNode = 0;
    private bool _textUIEmpty = true;
    private const string lorem = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam tempus nibh egestas orci mollis pharetra. Proin convallis eros a elementum commodo.";

    private void Awake()
    {
        _transform = transform;
        rectTransform = transform.GetComponent<RectTransform>();
        _scrollValue = transform.parent.Find("Scrollbar").GetComponent<Scrollbar>().value;
    }

    private void Start()
    {
        ChangePositionViaScroll();
        NewNode();
    }
    
    public void NewNode()
    {
        string title = "Title", text = lorem;
        CreateCommentNode(title, text);
    }

    public void NewNode(string title, string text)
    {
        CreateCommentNode(title, text);
    }
    
    public void CreateCommentNode(string title, string text)
    {
        GameObject commentNode = Instantiate(CommentNodePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        _commentNodeIndex++;
        commentNode.transform.SetParent(_transform, false);
        commentNode.name = "CommentNode" + _commentNodeIndex;
        commentNodes.Add(commentNode);

        commentNode.transform.Find("Title").GetComponent<TextMeshProUGUI>().text = title + " " + _commentNodeIndex;
        commentNode.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = text;
        
        if (_firstCommentNode == 0) _firstCommentNode = commentNode.GetComponent<RectTransform>().sizeDelta.y;
        
        if (_firstCommentNode == 0)
            commentNode.GetComponent<CommentNodeHandler>().localPositionY = 0;
        else
            commentNode.GetComponent<CommentNodeHandler>().localPositionY = -CalculateHeight() + _firstCommentNode;
        
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, CalculateHeight());
        transform.localPosition = new Vector3(0, - 10 - CalculateHeight() + _firstCommentNode, 0);
    }

    private float CalculateHeight()
    {
        float sum = 0;
        for (int i = 0; i <= _commentNodeIndex; i++)
            sum += commentNodes[i].transform.GetComponent<RectTransform>().sizeDelta.y;
        
        return sum;
    }

    private void ChangePositionViaScroll()
    {
        if (CalculateHeight() >= _height) 
            transform.localPosition = new Vector3(0, -(_scrollValue) * (_height - CalculateHeight()) - CalculateHeight() + _firstCommentNode - 10, 0);
    }

    public void Scroll()
    {
        _scrollValue = transform.parent.Find("Scrollbar").GetComponent<Scrollbar>().value;
        ChangePositionViaScroll();
    }

    public void SetActiveNode(GameObject node)
    {
        if (activeCommentNode != null)
            activeCommentNode.GetComponent<CommentNodeHandler>().SetInactive();

        activeCommentNode = node;
    }
}
