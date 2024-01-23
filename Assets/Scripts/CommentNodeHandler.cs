using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommentNodeHandler : MonoBehaviour
{
    public float localPositionY;
    public bool isActive = false;

    private RectTransform textFieldRectTransform,
        titleRectTransform,
        textRectTransform,
        SAARectTransform,
        SBNRectTransform,
        commentNodeRectTransform;
    private TextMeshProUGUI titleText, text;
    private Text SAAText, SBNText;
    private GameObject spaceAfterAuthor, spaceBetweenNods;

    public void Start()
    {
        textFieldRectTransform = transform.parent.GetComponent<RectTransform>();
        
        titleText = transform.Find("Title").GetComponent<TextMeshProUGUI>();
        titleRectTransform = titleText.GetComponent<RectTransform>();
        
        textRectTransform = transform.Find("Text").GetComponent<RectTransform>();
        text = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        
        spaceAfterAuthor = transform.Find("SpaceAfterAuthor").gameObject;
        SAARectTransform = spaceAfterAuthor.GetComponent<RectTransform>();
        SAAText = spaceAfterAuthor.AddComponent<Text>();
        
        spaceBetweenNods = transform.Find("SpaceBetweenNods").gameObject;
        SBNRectTransform = spaceBetweenNods.GetComponent<RectTransform>();
        SBNText = spaceBetweenNods.AddComponent<Text>();
        
        commentNodeRectTransform = GetComponent<RectTransform>();
        
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
        
        titleRectTransform.sizeDelta = new Vector2(textFieldRectTransform.sizeDelta.x, titleText.preferredHeight);
        titleText.transform.localPosition = new Vector3(0, 0, 0);
        
        SAARectTransform.sizeDelta = new Vector2(textFieldRectTransform.sizeDelta.x, SAAText.preferredHeight);
        SAAText.transform.localPosition = new Vector3(0, -titleText.preferredHeight, 0);
        
        textRectTransform.sizeDelta = new Vector2(textRectTransform.sizeDelta.x, text.preferredHeight);
        text.transform.localPosition = new Vector3(0, -titleText.preferredHeight - SAAText.preferredHeight, 0);
        
        SBNRectTransform.sizeDelta = new Vector2(textFieldRectTransform.sizeDelta.x, SBNText.preferredHeight);
        SBNText.transform.localPosition = new Vector3(0, -titleText.preferredHeight - SAAText.preferredHeight - text.preferredHeight, 0);
        
        commentNodeRectTransform.sizeDelta = new Vector2(commentNodeRectTransform.sizeDelta.x, titleText.preferredHeight + text.preferredHeight + SBNText.preferredHeight + SAAText.preferredHeight);
        transform.localPosition = new Vector3(0, -localPositionY, 0);
    }

    public void SetActive()
    {
        isActive = true;
        ChangeColor();
        transform.parent.GetComponent<CommentsPanel>().SetActiveNode(gameObject);
    }

    public void SetInactive()
    {
        isActive = false;
        ChangeColor();
    }

    private void ChangeColor()
    {
        if (isActive)
            transform.GetComponent<Image>().color = new Color(0.1882353f, 0.1882353f, 0.1882353f, 1f);
        else
            transform.GetComponent<Image>().color = new Color(0.2196079f, 0.2196079f, 0.2196079f, 1f);
    }
}
