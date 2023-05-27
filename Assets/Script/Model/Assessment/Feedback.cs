using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Com.oHMysTArs.Assessment
{
    public sealed class Feedback : MonoBehaviour
    {
        public FeedbackSO Data { get; private set; }
        [SerializeField]
        private Image avatar;
        [SerializeField]
        private TextMeshProUGUI text;
        [SerializeField]
        private TextMeshProUGUI rating;

        public void Init(FeedbackSO feedback)
        {
            Data = feedback;
            avatar.SetTexture(feedback.Avatar);
            text.SetText(feedback.Content);
            rating.SetText($"{(int)feedback.Rating}");
        }
    }
}
