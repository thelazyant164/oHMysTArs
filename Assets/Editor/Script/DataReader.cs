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

public static class DataReader
{
    public static string csvDirectoryPath = "Assets/Editor/CSV";
    public static string dataDirectoryPath = "Assets/Resources";

    public static List<FeedbackSO> Feedbacks {
        get
        {
            List<FeedbackSO> feedbacks = new();
            feedbacks.AddRange(Resources.LoadAll<FeedbackSO>($"{dataDirectoryPath}/Feedback/Accuracy"));
            feedbacks.AddRange(Resources.LoadAll<FeedbackSO>($"{dataDirectoryPath}/Feedback/Efficiency"));
            feedbacks.AddRange(Resources.LoadAll<FeedbackSO>($"{dataDirectoryPath}/Feedback/Punctuality"));
            feedbacks.AddRange(Resources.LoadAll<FeedbackSO>($"{dataDirectoryPath}/Feedback/Profitability"));
            return feedbacks;
        }
    }

    public static List<Level> Levels => Resources.LoadAll<Level>($"{dataDirectoryPath}/Level").ToList();

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
