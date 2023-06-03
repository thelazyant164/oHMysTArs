using Com.oHMysTArs.Input;
using Com.oHMysTArs.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.oHMysTArs.Tutorial
{
    public sealed class VIPTutorial : DrawTutorial
    {
        [SerializeField]
        private float delay = 2f;
        [SerializeField]
        private GameObject tutorialPopUp;
        [SerializeField]
        private GameObject mask;
        [SerializeField]
        private ParticleSystem emphasis;

        public override void Play()
        {
            StartCoroutine(DelayPause(delay));
            base.Play();
        }

        public override void Close()
        {
            base.Close();
            emphasis.Stop();
        }

        private IEnumerator DelayPause(float duration)
        {
            mask.gameObject.SetActive(false);
            tutorialPopUp.gameObject.SetActive(false);
            GameManager.Instance.TogglePause(false);
            yield return new WaitForSeconds(duration);
            GameManager.Instance.TogglePause(true);
            tutorialPopUp.gameObject.SetActive(true);
            mask.gameObject.SetActive(true);
            emphasis.Play();
        }
    }
}
