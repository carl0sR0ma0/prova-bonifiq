﻿namespace ProvaPub.Services
{
    public class RandomService
    {
        int seed;
        public RandomService()
        {
        }
        public int GetRandom()
        {
            GetNewSeed();
            return new Random(seed).Next(100);
        }
        private void GetNewSeed()
        {
            seed = Guid.NewGuid().GetHashCode();
        }
    }
}
