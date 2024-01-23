using System.Collections.Generic;

namespace Structs
{
    public struct Franchise
    {
        public string Name;
        public int Popularity;

        public void ChangePopularity(int delta)
        {
            Popularity += delta;
        }
    }
}