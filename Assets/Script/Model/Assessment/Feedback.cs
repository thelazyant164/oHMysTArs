using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Com.oHMysTArs.Assessment
{
    public sealed class Feedback : MonoBehaviour
    {
        [SerializeField]
        private Image avatar;
        [SerializeField]
        private TextMeshProUGUI text;
        [SerializeField]
        private TextMeshProUGUI rating;

        public void Init(FeedbackSO feedback)
        {
            avatar.SetTexture(feedback.Avatar);
            text.SetText(feedback.Content);
            rating.SetText($"{(int)feedback.Rating}");
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
