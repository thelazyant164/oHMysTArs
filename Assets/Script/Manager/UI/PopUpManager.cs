using Com.oHMysTArs.Level;
using Com.oHMysTArs.Assessment;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Com.oHMysTArs.Tutorial;

namespace Com.oHMysTArs.UI
{
    public sealed class PopUpManager : Singleton<PopUpManager>
    {
        [SerializeField]
        private List<PopUp> popUpList;
        public bool Pause => FindPopUp<PausePopUp>().gameObject.activeInHierarchy;

        private T FindPopUp<T>() where T : PopUp => popUpList.Find(o => o is T) as T;

        public void ShowTutorial(TutorialContent content) => FindPopUp<TutorialPopUp>().Show(content);

        public void ShowConfirm(
            string warning,
            Action onProceedCallback
        ) => FindPopUp<ConfirmPopUp>().Show(warning, onProceedCallback);

        public void ShowPause() => FindPopUp<PausePopUp>().Show();

        public void ShowAssessment(LevelAssessment assessment, FeedbackSO[] feedbacks) => FindPopUp<AssessmentPopUp>().Show(assessment, feedbacks);

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }
    }
}
