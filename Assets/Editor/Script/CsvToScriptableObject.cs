using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Com.oHMysTArs.Pattern;
using Com.oHMysTArs.Spaceship;
using Com.oHMysTArs.Assessment;

public static class CsvToScriptableObject
{
    public static string csvDirectoryPath = "Assets/Editor/CSV";
    public static string dataDirectoryPath = "Assets/Resources";

    public static List<Pattern> Patterns
    {
        get
        {
            return Directory
                .GetFiles(
                    dataDirectoryPath,
                    "*.asset",
                    SearchOption.TopDirectoryOnly
                )
                .ToList()
                .Select(file => Resources.Load(file, typeof(Pattern)))
                .Cast<Pattern>()
                .ToList();
        }
    }

    public static List<SpaceshipSO> Spaceships
    {
        get
        {
            return Directory
                .GetFiles(
                    dataDirectoryPath,
                    "*.asset",
                    SearchOption.TopDirectoryOnly
                )
                .ToList()
                .Select(file => Resources.Load(file, typeof(SpaceshipSO)))
                .Cast<SpaceshipSO>()
                .ToList();
        }
    }

    public static List<FeedbackSO> Feedbacks
    {
        get
        {
            return Directory
                .GetFiles(
                    dataDirectoryPath,
                    "*.asset",
                    SearchOption.TopDirectoryOnly
                )
                .ToList()
                .Select(file => Resources.Load(file, typeof(FeedbackSO)))
                .Cast<FeedbackSO>()
                .ToList();
        }
    }

    public static int ParseInt(string strValue, int defaultValue = 0)
    {
        if (int.TryParse(strValue, out int temp))
        {
            return temp;
        }
        return defaultValue;
    }
}
