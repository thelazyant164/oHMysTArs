using Com.oHMysTArs.Pattern;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Linq;
using System.IO;

namespace Com.oHMysTArs.Spaceship
{
    public sealed class SpaceshipLoader : BaseLoader<SpaceshipSO>
    {
        private readonly List<Pattern.Pattern> patterns = Resources.LoadAll<Pattern.Pattern>("Pattern").ToList();
        private readonly List<Texture2D> solidTextures = Resources.LoadAll<Texture2D>("Spaceship/Texture/Solid").ToList();
        private readonly List<Texture2D> scanTextures = Resources.LoadAll<Texture2D>("Spaceship/Texture/Scan").ToList();
        private readonly List<Texture2D> unlockTextures = Resources.LoadAll<Texture2D>("Spaceship/Texture/Unlock").ToList();

        public SpaceshipLoader() : base("Spaceship/Model", "Spaceship_data.csv") { }

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
                Texture2D texture = solidTextures.Find(texture => texture.name == fields[0]);
                Texture2D scanTexture = scanTextures.Find(texture => texture.name == $"{fields[0]}_scan_v2");
                Texture2D unlockTexture = unlockTextures.Find(texture => texture.name == $"{fields[0]}_unlock");
                SpaceshipSO newSpaceship = SpaceshipSO.Init(fields[0], pattern, 
                    DataParser.ParseInt(fields[2]) == 1, 
                    texture, scanTexture, unlockTexture);
                items.Add(newSpaceship);
            }
            return items;
        }
    }
}
