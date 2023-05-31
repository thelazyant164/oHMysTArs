using Com.oHMysTArs.Assessment;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Com.oHMysTArs.Level
{
    [Serializable]
    public sealed class LevelResult
    {
        private sealed class UniqueLevelResults : SerializableDictionary<string, int>
        {
            public bool TrySave(LevelResult levelResult)
            {
                if (data.TryGetValue(levelResult.Name, out int rating))
                {
                    if (levelResult.Rating > rating)
                    {
                        data[levelResult.Name] = levelResult.Rating;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    data[levelResult.Name] = levelResult.Rating;
                    return true;
                }
            }

            public List<LevelResult> Results => data.Select(pair => new LevelResult(pair.Key, (Rating)pair.Value)).ToList();
        }

        private static string filePath => $"{Application.persistentDataPath}/level_result.json";

        public string Name;
        public int Rating;

        public LevelResult(string name, Rating overallRating)
        {
            Name = name;
            Rating = (int)overallRating;
        }

        public void Save()
        {
            if (!File.Exists(filePath)) File.Create(filePath).Dispose();
            UniqueLevelResults filtered = LoadUnique();
            if (filtered.TrySave(this))
            {
                File.WriteAllText(filePath, JsonUtility.ToJson(filtered));
            }
        }

        public static List<LevelResult> Load()
        {
            List<LevelResult> results = LoadUnique()?.Results;
            results.Sort(new LevelResultComparer());
            return results;
        }

        private static UniqueLevelResults LoadUnique()
        {
            if (!File.Exists(filePath)) return new UniqueLevelResults();
            UniqueLevelResults parsed = JsonUtility.FromJson<UniqueLevelResults>(File.ReadAllText(filePath));
            return parsed ?? new UniqueLevelResults();
        }
    }

    public sealed class LevelResultComparer : IComparer<LevelResult>
    {
        public int Compare(LevelResult a, LevelResult b)
        {
            if (a.Name == b.Name) throw new ArgumentException($"Duplicate level name: {a.Name}");
            return String.Compare(a.Name, b.Name);
        }
    }
}
