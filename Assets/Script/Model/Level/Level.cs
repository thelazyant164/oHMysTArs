using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.oHMysTArs.Level
{
    [CreateAssetMenu(fileName = "New level", menuName = "Level")]
    public sealed class Level : ScriptableObject
    {
        public Spaceship.SpaceshipSO[] Queue;

        public static Level Init(string name, Spaceship.SpaceshipSO[] spaceships)
        {
            Level level = ScriptableObject.CreateInstance<Level>();
            level.name = name;
            level.Queue = spaceships;
            return level;
        }
    }
}
