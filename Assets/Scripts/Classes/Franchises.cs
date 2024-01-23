using System.Collections.Generic;
using Structs;

namespace Classes
{
    public class FranchisesList
    {
        public List<Franchise> Franchises = new List<Franchise>() { };

        public void Add(string name, int popularity)
        {
            Franchises.Add(new Franchise(){Name = name, Popularity = popularity});
        }
    }
}