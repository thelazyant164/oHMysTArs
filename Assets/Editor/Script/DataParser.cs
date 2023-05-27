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
using Com.oHMysTArs.Level;

public static class DataParser
{
    public static string csvDirectoryPath = "Assets/Editor/CSV";
    public static string dataDirectoryPath = "Assets/Resources";

    public static int ParseInt(string strValue, int defaultValue = 0)
    {
        if (int.TryParse(strValue, out int temp))
        {
            return temp;
        }
        return defaultValue;
    }

    [MenuItem("Utility/Parse/All")]
    public static void InvokeAll()
    {
        FeedbackLoader.Invoke();
        PatternLoader.Invoke();
        SpaceshipLoader.Invoke();
        LevelLoader.Invoke();
    }
}
