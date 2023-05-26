using Com.oHMysTArs.Pattern;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using Com.oHMysTArs.Grid;
using System.Linq;
using static Com.oHMysTArs.Grid.GridSystem;
using System.IO;

namespace Com.oHMysTArs.Spaceship
{
    public sealed class SpaceshipLoader : BaseGenerator<SpaceshipSO>
    {
        private readonly List<Pattern.Pattern> patterns = CsvToScriptableObject.Patterns;
        private readonly List<Texture2D> textures = Resources.LoadAll<Texture2D>("Spaceship/Texture").ToList();

        public SpaceshipLoader() : base("Spaceship", "Spaceship_data.csv") { }

        [MenuItem("Utility/Parse/Spaceship")]
        public static void Invoke() => new SpaceshipLoader().Generate();

        public override List<SpaceshipSO> ParseItems(string[] lines)
        {
            List<SpaceshipSO> items = new List<SpaceshipSO>();
            foreach (string line in lines)
            {
                string[] fields = line.Split(",");
                if (fields.Length == 0) break;

                Pattern.Pattern pattern = patterns.Find(pattern => pattern.name == fields[1]);
                Texture2D texture = textures.Find(texture => texture.name == fields[1]);
                SpaceshipSO newSpaceship = SpaceshipSO.Init(fields[0], pattern, CsvToScriptableObject.ParseInt(fields[2]) == 1, texture);
                items.Add(newSpaceship);
            }
            return items;
        }
    }
}
