using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Com.oHMysTArs.Pattern;

public static class CsvToScriptableObject
{
    public static string csvDirectoryPath = "Assets/Editor/CSV";
    public static string dataDirectoryPath = "Assets/Resource";

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
                .Select(file => AssetDatabase.LoadAssetAtPath(file, typeof(Pattern)))
                .Cast<Pattern>()
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
