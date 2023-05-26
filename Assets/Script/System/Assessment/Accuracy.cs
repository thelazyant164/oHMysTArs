using Com.oHMysTArs.Spaceship;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.oHMysTArs.Assessment
{
    public readonly struct Accuracy
    {
        public readonly Rating Rating;

        public Accuracy(List<Spaceship.Spaceship> spaceships)
        {
            float sum = 0;
            foreach (Spaceship.Spaceship spaceship in spaceships)
            {
                sum += spaceship.Attempts;
            }
            float avg = sum / spaceships.Count;
            switch (avg)
            {
                case > 4:
                    Rating = Rating.One;
                    break;
                case > 3:
                    Rating = Rating.Two;
                    break;
                case > 2:
                    Rating = Rating.Three;
                    break;
                case > 1:
                    Rating = Rating.Four;
                    break;
                default:
                    Rating = Rating.One;
                    break;
            }
        }
    }
}
