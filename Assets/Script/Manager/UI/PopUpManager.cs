using Com.oHMysTArs.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Com.oHMysTArs.UI
{
    public sealed class PopUpManager : Singleton<PopUpManager>
    {
        [SerializeField]
        private List<PopUp> popUpList;

        private T FindPopUp<T>() where T : PopUp => popUpList.Find(o => o is T) as T;

        public void ShowConfirm(
            ConfirmPopUp.ConfirmPopUpContent content,
            Action onProceedCallback,
            Action onDismissCallback
        ) => FindPopUp<ConfirmPopUp>().Show(content, onProceedCallback, onDismissCallback);

        public void ShowMessage(string message) => FindPopUp<MessagePopUp>().Show(message);

        public void ShowRedirect(
            RedirectPopUp.RedirectPopUpContent content,
            Action onProceedCallback
        ) => FindPopUp<RedirectPopUp>().Show(content, onProceedCallback);

        // TODO: fill out callbacks
        public void ShowAssessment(LevelAssessment assessment) 
            => FindPopUp<AssessmentPopUp>().Show(assessment, () => { }, () => { }, () => { });

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            transform.parent = null;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
