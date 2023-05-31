using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Com.oHMysTArs.Assessment
{
    public sealed class FeedbackManager : MonoBehaviour
    {
        [SerializeField]
        private Feedback feedbackPrefab;
        private readonly List<FeedbackSO> feedbacks = new();

        private void Awake()
        {
            feedbacks.AddRange(Resources.LoadAll<FeedbackSO>($"Feedback/Accuracy"));
            feedbacks.AddRange(Resources.LoadAll<FeedbackSO>($"Feedback/Efficiency"));
            feedbacks.AddRange(Resources.LoadAll<FeedbackSO>($"Feedback/Punctuality"));
            feedbacks.AddRange(Resources.LoadAll<FeedbackSO>($"Feedback/Profitability"));
        }

        private List<FeedbackSO> Filter(Type type, Rating rating) => feedbacks.Where(feedback => feedback.Type == type && feedback.Rating == rating).ToList();

        private FeedbackSO GetRandomFeedback(Type type, Rating rating) => Filter(type, rating).GetRandom();

        public GameObject CreateFeedback(FeedbackSO data, Transform forum)
        {
            Feedback newFeedback = GameObject.Instantiate(feedbackPrefab.gameObject, forum).GetComponent<Feedback>();
            newFeedback.Init(data);
            return newFeedback.gameObject;
        }

        public FeedbackSO[] CreateFeedbacks(LevelAssessment assessment)
        {
            FeedbackSO[] feedbacks = new FeedbackSO[3];
            feedbacks[0] = GetRandomFeedback(Type.Accuracy, assessment.Accuracy);
            feedbacks[1] = GetRandomFeedback(Type.Punctuality, assessment.Punctuality);
            feedbacks[2] = GetRandomFeedback(Type.Profitability, assessment.Profitability);
            feedbacks[Random.Range(0, 3)] = GetRandomFeedback(Type.Efficiency, assessment.OverallRating);
            return feedbacks;
        }
    }
}
