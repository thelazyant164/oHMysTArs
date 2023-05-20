using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Com.oHMysTArs.Level
{
    public readonly struct Efficiency
    {
        public readonly Rating Rating;

        public Efficiency(Accuracy accuracy, Punctuality punctuality, Profitability profitability)
        {
            int score = (int)accuracy.Rating + (int)punctuality.Rating * 3 + (int)profitability.Rating * 6;
            switch (score / 10f)
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
