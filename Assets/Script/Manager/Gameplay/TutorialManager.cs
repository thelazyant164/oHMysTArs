using Com.oHMysTArs.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.oHMysTArs.Tutorial
{
    public sealed class TutorialManager : MonoBehaviour
    {
        [Header("Layer setup")]
        [SerializeField]
        private Canvas ignoreLayer;
        public Transform Ignore => ignoreLayer.transform;
        [SerializeField]
        private Canvas focusLayer;
        public Transform Focus => focusLayer.transform;
        [Space]

        [Header("Tutorial content")]
        [SerializeField]
        private List<TutorialContent> pendingTutorial;
        [SerializeField]
        private List<TutorialContent> completeTutorial;
        private TutorialContent Current => pendingTutorial.FirstOrDefault();
        public event EventHandler<TutorialContent> OnStart;
        public event EventHandler<TutorialContent> OnComplete;

        private void Awake()
        {
            OnStart += StartTutorial;
            OnComplete += EndTutorial;
        }

        private void Start()
        {
            if (GameManager.Instance.InTutorialMode)
            {
                OnStart?.Invoke(this, Current);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        private void StartTutorial(object sender, TutorialContent tutorial)
        {
            PopUpManager.Instance.ShowTutorial(Current);
            GameManager.Instance.TogglePause(true);
            tutorial.Play();
        }

        private void EndTutorial(object sender, TutorialContent tutorial)
        {
            pendingTutorial.Remove(tutorial);
            completeTutorial.Add(tutorial);
            GameManager.Instance.TogglePause(false);
            tutorial.Close();
        }

        private void Update()
        {
            if (Current == null || !Current.Complete) return;
            OnComplete?.Invoke(this, Current);
            if (Current != null)
            {
                OnStart?.Invoke(this, Current);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}