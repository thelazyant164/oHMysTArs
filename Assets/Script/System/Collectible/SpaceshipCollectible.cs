using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Com.oHMysTArs.Collectible
{
    [Serializable]
    public sealed class SpaceshipCollectible
    {
        private sealed class UniqueSpaceshipUnlocked : SerializableDictionary<string, SpaceshipCollectible>
        {
            public bool TrySave(SpaceshipCollectible newSpaceship)
            {
                if (data.ContainsKey(newSpaceship.Name)) return false;
                data[newSpaceship.Name] = newSpaceship;
                return true;
            }

            public List<SpaceshipCollectible> Results => data.Values.ToList();
        }

        private static string filePath => $"{Application.persistentDataPath}/spaceship_collectible.json";

        public string Name;

        public SpaceshipCollectible(string name) 
        { 
            Name = name;
        }

        public void Save()
        {
            if (!File.Exists(filePath)) File.Create(filePath).Dispose();
            UniqueSpaceshipUnlocked filtered = LoadUnique();
            if (filtered.TrySave(this))
            {
                File.WriteAllText(filePath, JsonUtility.ToJson(filtered));
            }
        }

        public static List<SpaceshipCollectible> Load()
        {
            List<SpaceshipCollectible> results = LoadUnique()?.Results;
            return results;
        }

        private static UniqueSpaceshipUnlocked LoadUnique()
        {
            if (!File.Exists(filePath)) return new UniqueSpaceshipUnlocked();
            UniqueSpaceshipUnlocked parsed = JsonUtility.FromJson<UniqueSpaceshipUnlocked>(File.ReadAllText(filePath));
            return parsed ?? new UniqueSpaceshipUnlocked();
        }
    }
}
