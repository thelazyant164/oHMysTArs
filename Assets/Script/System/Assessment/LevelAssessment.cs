using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Com.oHMysTArs.Level
{
    public enum Rating
    {
        One = 1,
        Two = 2, 
        Three = 3,
        Four = 4,
        Five = 5
    }

    public readonly struct LevelAssessment
    {
        public readonly string Name;
        private readonly List<Spaceship.Spaceship> spaceships;
        public readonly int Total;
        public readonly int Succeed;

        private readonly Accuracy accuracy;
        private readonly Efficiency efficiency;
        private readonly Punctuality punctuality;
        private readonly Profitability profitability;

        public readonly Rating Accuracy => accuracy.Rating;
        public readonly Rating Punctuality => punctuality.Rating;
        public readonly Rating OverallRating => efficiency.Rating;
        public readonly Rating Profitability => profitability.Rating;

        public LevelAssessment(string name, List<Spaceship.Spaceship> spaceships)
        {
            Name = name;
            this.spaceships = spaceships;
            profitability = new Profitability(spaceships);
            Total = spaceships.Count;
            List<Spaceship.Spaceship> succeed = spaceships.Where(spaceship => spaceship.Succeed).ToList();
            Succeed = succeed.Count();
            accuracy = new Accuracy(succeed);
            punctuality = new Punctuality(succeed);
            efficiency = new Efficiency(accuracy, punctuality, profitability);
        }
    }
}
