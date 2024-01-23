using System;
using System.Collections.Generic;
using Interfaces;
using Structs;
using UnityEngine;

namespace Classes
{
    public class Movie : MonoBehaviour, IVideoProduct
    {
        public int expectedBudget;
        public int expectedIncome;
        public int expectedDate;
    
        private List<MovieGenre> _genres = new List<MovieGenre>();

        private void Start()
        {
            _genres.Add(MovieGenre.Crime);
            _genres.Add(MovieGenre.ScienceFiction);
            Debug.Log(GenresToString(_genres));
        }

        private string GenresToString(List<MovieGenre> _genres)
        {
            string s = "";
            foreach (var genre in _genres)
                s += genre + ", ";
            return s.Substring(0, s.Length - 2);
        }

        public void CalculateIncome()
        {
        
        }
    }
}