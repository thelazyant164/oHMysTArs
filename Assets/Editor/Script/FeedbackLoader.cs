using Com.oHMysTArs.Pattern;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using Com.oHMysTArs.Assessment;
using System.Linq;
using static Com.oHMysTArs.Grid.GridSystem;
using System.IO;

namespace Com.oHMysTArs.Spaceship
{
    public sealed class FeedbackLoader : BaseGenerator<FeedbackSO>
    {
        private static List<Texture2D> textures = Resources.LoadAll<Texture2D>("Feedback/Avatar").ToList();
        private readonly Type type;
        private int generatedCount = 0;

        public FeedbackLoader(Type type, string outDir, string inDir) : base(outDir, inDir) 
        {
            this.type = type;
        }

        [MenuItem("Utility/Parse/Feedback")]
        public static void Invoke() 
        { 
            new FeedbackLoader(Type.Accuracy, "Feedback/Accuracy", "Feedback_data_accuracy.csv").Generate();
            new FeedbackLoader(Type.Punctuality, "Feedback/Punctuality", "Feedback_data_punctuality.csv").Generate();
            new FeedbackLoader(Type.Profitability, "Feedback/Profitability", "Feedback_data_profitability.csv").Generate();
            new FeedbackLoader(Type.Efficiency, "Feedback/Efficiency", "Feedback_data_efficiency.csv").Generate();
        }

        public override List<FeedbackSO> ParseItems(string[] lines)
        {
            List<FeedbackSO> items = new();
            foreach (string line in lines)
            {
                string[] fields = line.Split(",");
                if (fields.Length == 0) break;
                FeedbackSO newFeedback = FeedbackSO.Init(
                    $"Feedback_{nameof(type)}_{generatedCount}", type, 
                    (Rating)CsvToScriptableObject.ParseInt(fields[1]), 
                    fields[0], textures.GetRandom());
                items.Add(newFeedback);
                generatedCount++;
            }
            return items;
        }
    }
}
