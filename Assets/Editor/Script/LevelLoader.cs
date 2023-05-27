using System.Collections.Generic;
using UnityEditor;
using Com.oHMysTArs.Spaceship;
using UnityEngine;
using System.Linq;

namespace Com.oHMysTArs.Level
{
    public sealed class LevelLoader : BaseLoader<Level>
    {
        private readonly List<SpaceshipSO> spaceships = Resources.LoadAll<SpaceshipSO>("Spaceship/Model").ToList();

        public LevelLoader() : base("Level", "Level_data.csv") { }

        [MenuItem("Utility/Parse/Level")]
        public static void Invoke() => new LevelLoader().Generate();

        public override List<Level> ParseItems(string[] lines)
        {
            List<Level> items = new List<Level>();
            foreach (string line in lines)
            {
                string[] fields = line.Split(",");
                if (fields.Length == 0) break;
                SpaceshipSO[] predefinedOrder = new SpaceshipSO[fields.Length - 1];
                for (int i = 0; i < predefinedOrder.Length; i++) 
                {
                    if (fields[i + 1] == "") break;
                    predefinedOrder[i] = spaceships.Find(spaceship => spaceship.name == fields[i + 1]);
                }

                Level newLevel = Level.Init(fields[0], predefinedOrder);
                items.Add(newLevel);
            }
            return items;
        }
    }
}
