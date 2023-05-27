using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Com.oHMysTArs.Assessment
{
    public readonly struct Efficiency
    {
        public readonly Rating Rating;

        public Efficiency(Accuracy accuracy, Punctuality punctuality, Profitability profitability)
        {
            int score = (int)accuracy.Rating + (int)punctuality.Rating * 3 + (int)profitability.Rating * 6;
            Rating = (Rating)(score / 10);
        }
    }
}
