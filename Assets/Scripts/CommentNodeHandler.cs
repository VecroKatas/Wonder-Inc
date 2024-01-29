using System;
using System.Collections;
using System.Data;
using Classes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommentNodeHandler : MonoBehaviour
{
    public float localPositionY;
    public bool isActive = false, isDeleted = false;
    public Comment Comment;
    public Account Account;

    private RectTransform textFieldRectTransform,
        titleRectTransform,
        textRectTransform,
        SAARectTransform,
        SBNRectTransform,
        commentNodeRectTransform,
        viewsCountRectTransform,
        likesCountRectTransform,
        commentsCountRectTransform,
        sharesCountRectTransform;
    private TextMeshProUGUI titleText, text, viewCount, likesCount, commentsCount, sharesCount;
    private Text SAAText, SBNText;
    private GameObject spaceAfterAuthor, spaceBetweenNods;
    private float _width;

    public void Start()
    {
        
        
        textFieldRectTransform = transform.parent.GetComponent<RectTransform>();
        
        titleText = transform.Find("CommentItself").Find("Title").GetComponent<TextMeshProUGUI>();
        titleRectTransform = titleText.GetComponent<RectTransform>();

        text = transform.Find("CommentItself").Find("Text").GetComponent<TextMeshProUGUI>();
        textRectTransform = text.GetComponent<RectTransform>();
        
        viewCount = transform.Find("CommentItself").Find("ViewCount").GetComponent<TextMeshProUGUI>();
        viewsCountRectTransform = viewCount.GetComponent<RectTransform>();

        likesCount = transform.Find("CommentItself").Find("LikesCount").GetComponent<TextMeshProUGUI>();
        likesCountRectTransform = likesCount.GetComponent<RectTransform>();
        
        commentsCount = transform.Find("CommentItself").Find("CommentsCount").GetComponent<TextMeshProUGUI>();
        commentsCountRectTransform = commentsCount.GetComponent<RectTransform>();

        sharesCount = transform.Find("CommentItself").Find("SharesCount").GetComponent<TextMeshProUGUI>();
        sharesCountRectTransform = sharesCount.GetComponent<RectTransform>();

        spaceAfterAuthor = transform.Find("CommentItself").Find("SpaceAfterAuthor").gameObject;
        SAARectTransform = spaceAfterAuthor.GetComponent<RectTransform>();
        SAAText = spaceAfterAuthor.AddComponent<Text>();
        
        spaceBetweenNods = transform.Find("CommentItself").Find("SpaceBetweenNods").gameObject;
        SBNRectTransform = spaceBetweenNods.GetComponent<RectTransform>();
        SBNText = spaceBetweenNods.AddComponent<Text>();
        
        commentNodeRectTransform = GetComponent<RectTransform>();

        _width = textFieldRectTransform.sizeDelta.x;
        
        StartCoroutine(ResizeTMP());
    }
    
    private IEnumerator ResizeTMP()
    {
        SAAText.font = Resources.GetBuiltinResource(typeof(Font), "LegacyRuntime.ttf") as Font;
        SAAText.fontSize = 4;
        SAAText.text = "\n";

        SBNText.font = Resources.GetBuiltinResource(typeof(Font), "LegacyRuntime.ttf") as Font;
        SBNText.fontSize = 9;
        SBNText.text = "\n";

        yield return new WaitForFixedUpdate();//wait a fixed update because otherwise preferredHeight does shit 
        
        titleRectTransform.sizeDelta = new Vector2(_width, titleText.preferredHeight);
        titleText.transform.localPosition = new Vector3(0, 0, 0);
        
        SAARectTransform.sizeDelta = new Vector2(_width, SAAText.preferredHeight);
        SAAText.transform.localPosition = new Vector3(0, -titleText.preferredHeight, 0);
        
        textRectTransform.sizeDelta = new Vector2(_width, text.preferredHeight);
        text.transform.localPosition = new Vector3(0, -titleText.preferredHeight - SAAText.preferredHeight, 0);

        StartCoroutine(ResizeStats());
        
        SBNRectTransform.sizeDelta = new Vector2(_width, SBNText.preferredHeight);
        SBNText.transform.localPosition = new Vector3(0, -titleText.preferredHeight - SAAText.preferredHeight - text.preferredHeight - likesCount.preferredHeight, 0);
        
        commentNodeRectTransform.sizeDelta = new Vector2(commentNodeRectTransform.sizeDelta.x, titleText.preferredHeight + text.preferredHeight + SBNText.preferredHeight + SAAText.preferredHeight + likesCount.preferredHeight);
        transform.localPosition = new Vector3(0, -localPositionY, 0);
    }

    public void SetActive()
    {
        if (!isDeleted)
        {
            isActive = true;
            ChangeColor();
            transform.parent.GetComponent<CommentsPanel>().SetActiveNode(gameObject);
        }
    }

    public void SetInactive()
    {
        isActive = false;
        ChangeColor();
    }

    public void UpdateStats()
    {
        viewCount.text = "V:" + (int)Comment.Views;
        likesCount.text = "L:" + (int)Comment.Likes;
        commentsCount.text = "C:" + (int)Comment.Comments;
        sharesCount.text = "S:" + (int)Comment.Shares;
        StartCoroutine(ResizeStats());
    }

    IEnumerator ResizeStats()
    {
        yield return new WaitForFixedUpdate();
        
        viewsCountRectTransform.sizeDelta = new Vector2(viewCount.preferredWidth, viewCount.preferredHeight);
        viewCount.transform.localPosition = new Vector3(-_width / 2 + viewCount.preferredWidth / 2,
            -titleText.preferredHeight - SAAText.preferredHeight - text.preferredHeight - viewCount.preferredHeight / 2, 0);
        
        likesCountRectTransform.sizeDelta = new Vector2(likesCount.preferredWidth, likesCount.preferredHeight);
        likesCount.transform.localPosition = new Vector3(-_width / 2 + viewCount.preferredWidth * 2,
            -titleText.preferredHeight - SAAText.preferredHeight - text.preferredHeight - viewCount.preferredHeight / 2, 0);
        
        commentsCountRectTransform.sizeDelta = new Vector2(commentsCount.preferredWidth, commentsCount.preferredHeight);
        commentsCount.transform.localPosition = new Vector3(-_width / 2 + viewCount.preferredWidth * 7 / 2,
            -titleText.preferredHeight - SAAText.preferredHeight - text.preferredHeight - viewCount.preferredHeight / 2, 0);
        
        sharesCountRectTransform.sizeDelta = new Vector2(sharesCount.preferredWidth, sharesCount.preferredHeight);
        sharesCount.transform.localPosition = new Vector3(-_width / 2 + viewCount.preferredWidth * 5, 
            -titleText.preferredHeight - SAAText.preferredHeight - text.preferredHeight - viewCount.preferredHeight / 2, 0);
    }

    private void ChangeColor()
    {
        if (isActive)
            transform.GetComponent<Image>().color = new Color(0.1882353f, 0.1882353f, 0.1882353f, 1f);
        else
            transform.GetComponent<Image>().color = new Color(0.2196079f, 0.2196079f, 0.2196079f, 1f);
    }
}
