using Com.oHMysTArs.Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.oHMysTArs.Assessment
{
    public enum Type
    {
        Accuracy = 0,
        Punctuality = 1,
        Profitability = 2,
        Efficiency = 3
    }

    public sealed class FeedbackSO : ScriptableObject
    {
        public Type Type;
        public Rating Rating;
        public string Content;
        public Texture2D Avatar;

        public static FeedbackSO Init(string name, Type type, Rating rating, string content, Texture2D avatar) 
        {
            FeedbackSO feedback = ScriptableObject.CreateInstance<FeedbackSO>();
            feedback.name = name;
            feedback.Type = type;
            feedback.Rating = rating;
            feedback.Content = content;
            feedback.Avatar = avatar;
            return feedback;
        }
    }
}
