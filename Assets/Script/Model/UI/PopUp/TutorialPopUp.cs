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
        [SerializeField]
        private Image mask;
        private TutorialContent tutorial;
        private GameObject content;

        private void Awake()
        {
            mask.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        protected override void Start()
        {
            if (GameManager.Instance.InTutorialMode) 
            {
                GameManager.Instance.TutorialManager.OnComplete += (object sender, TutorialContent tutorial) =>
                {
                    if (tutorial != this.tutorial) return;
                    GameObject.Destroy(content);
                    Hide();
                };
            }
        }

        public void Show(TutorialContent tutorialContent)
        {
            mask.gameObject.SetActive(true);
            tutorial = tutorialContent;
            content = GameObject.Instantiate(tutorialContent.Content, transform);
            base.Show();
        }

        public override void Hide()
        {
            mask.gameObject.SetActive(false);
            base.Hide();
        }
    }
}
