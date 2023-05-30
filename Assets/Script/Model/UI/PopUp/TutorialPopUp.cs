using Com.oHMysTArs.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Com.oHMysTArs.Tutorial
{
    public sealed class TutorialPopUp : PopUp
    {
        private TutorialContent tutorial;
        private GameObject content;

        protected override void Start()
        {
            TutorialManager tutorialManager = GameManager.Instance.TutorialManager;
            if (tutorialManager != null)
            {
                tutorialManager.OnComplete += (object sender, TutorialContent tutorial) =>
                {
                    if (tutorial != this.tutorial) return;
                    GameObject.Destroy(content);
                    base.Hide();
                };
            }
            base.Start();
        }

        public void Show(TutorialContent tutorialContent)
        {
            tutorial = tutorialContent;
            content = GameObject.Instantiate(tutorialContent.Content, transform);
            base.Show();
        }
    }
}
