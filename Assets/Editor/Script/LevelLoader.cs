using Com.oHMysTArs.Pattern;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using Com.oHMysTArs.Grid;
using System.Linq;
using Com.oHMysTArs.Spaceship;
using static Com.oHMysTArs.Grid.GridSystem;

namespace Com.oHMysTArs.Level
{
    public sealed class LevelLoader : BaseGenerator<Level>
    {
        private readonly List<Spaceship.SpaceshipSO> spaceships = CsvToScriptableObject.Spaceships;

        public LevelLoader() : base("Level", "Level_data.csv") { }

        [MenuItem("Utility/Parse/Level")]
        public static void Invoke() => new PatternLoader().Generate();

        public override List<Level> ParseItems(string[] lines)
        {
            List<Level> items = new List<Level>();
            foreach (string line in lines)
            {
                string[] fields = line.Split(",");
                if (fields.Length == 0) break;
                Spaceship.SpaceshipSO[] predefinedOrder = new Spaceship.SpaceshipSO[fields.Length - 1];
                for (int i = 1; i < fields.Length; i++) 
                {
                    predefinedOrder[i - 1] = spaceships.Find(spaceship => spaceship.name == fields[i]);
                }

                Level newLevel = Level.Init(fields[0], predefinedOrder);
                items.Add(newLevel);
            }
            return items;
        }
    }
}
