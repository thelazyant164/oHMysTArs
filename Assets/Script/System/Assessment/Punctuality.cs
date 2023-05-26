using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.oHMysTArs.Assessment
{
    public readonly struct Punctuality
    {
        public readonly Rating Rating;

        public Punctuality(List<Spaceship.Spaceship> spaceships)
        {
            float sum = 0;
            foreach (Spaceship.Spaceship spaceship in spaceships)
            {
                sum += spaceship.ServeTime;
            }
            float avg = sum / spaceships.Count;
            switch (avg)
            {
                case >= 5:
                    Rating = Rating.Five;
                    break;
                case >= 4:
                    Rating = Rating.Four;
                    break;
                case >= 3:
                    Rating = Rating.Three;
                    break;
                case >= 2:
                    Rating = Rating.Two;
                    break;
                default:
                    Rating = Rating.One;
                    break;
            }
        }
    }
}
