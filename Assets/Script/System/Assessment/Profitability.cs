using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Com.oHMysTArs.Assessment
{
    public readonly struct Profitability
    {
        public readonly Rating Rating;

        public Profitability(List<Spaceship.Spaceship> spaceships)
        {
            float total = spaceships.Sum(spaceship => spaceship.VIP ? 3 : 1);
            float score = spaceships.Sum(spaceship => spaceship.Succeed ? spaceship.VIP ? 3 : 1 : 0);
            switch (score / total)
            {
                case >= .8f:
                    Rating = Rating.Five;
                    break;
                case >= .6f:
                    Rating = Rating.Four;
                    break;
                case >= .4f:
                    Rating = Rating.Three;
                    break;
                case >= .2f:
                    Rating = Rating.Two;
                    break;
                default:
                    Rating = Rating.One;
                    break;
            }
        }
    }
}
