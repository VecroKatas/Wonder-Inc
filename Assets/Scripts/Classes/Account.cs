using System.Collections.Generic;
using UnityEngine;

namespace Classes
{
    public class Account
    {
        public string AccountName;
        public int HaterLevel;
        public bool IsBanned = false;
        public List<Comment> CommentsList;

        public Account()
        {
            AccountName = "accountName";
            HaterLevel = 0;
            CommentsList = new List<Comment>();
        }
        
        public Account(string accountName, int haterLevel) : this()
        {
            AccountName = accountName;
            HaterLevel = haterLevel;
        }
    }
}