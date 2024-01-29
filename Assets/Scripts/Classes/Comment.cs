using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;

namespace Classes
{
    public class Comment
    {
        public string CommentText;
        public float Views, Likes, Comments, Shares, Interval = 1;
        public bool IsTrending = false, isDeleted = false;
        public Account Account;
        
        private GameObject CommentsPanel;
        private float growthRate, likesRatio, commentsRatio, sharesRatio;
        private Random random = new Random();
        public Comment()
        {
            CommentText= "Default comment text";
            Account = new Account();
            Account.CommentsList.Add(this);
            Views = 0;
            Likes = 0;
            Comments = 0;
            Shares = 0;
            growthRate = random.Next(1, 10);
            growthRate = growthRate - 2 <= 0 ? 0 : growthRate - 2;
            likesRatio = random.Next((int)growthRate + 1, 10);
            commentsRatio = random.Next((int)growthRate + 1, 20);
            sharesRatio = random.Next((int)growthRate + 1, 30);
        }

        public Comment(Account account, string commentText, GameObject commentsPanel) : this()
        {
            CommentText = commentText;
            Account = account;
            Account.CommentsList.Add(this);
            CommentsPanel = commentsPanel;
        }

        private void Start()
        {
            Interval = GameObject.Find("CommentsManager").GetComponent<CommentManager>().CommentsGenerationInterval;
        }

        public void GrowComment()
        {
            while (!isDeleted)
            {
                Views += growthRate;
                Likes += growthRate / likesRatio;
                Comments += growthRate / commentsRatio;
                Shares += growthRate / sharesRatio;
                CommentsPanel.GetComponent<CommentNodeHandler>().UpdateStats();
            }
        }
    }
}