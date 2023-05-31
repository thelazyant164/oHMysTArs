using Com.oHMysTArs.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Com.oHMysTArs.Tutorial
{
    public abstract class TutorialContent : MonoBehaviour
    {
        [Header("Content")]
        [SerializeField]
        private GameObject content;
        public GameObject Content => content;
        [Space]

        [Header("Layer sorting override")]
        [SerializeField]
        protected List<GameObject> ignore;
        private List<Transform> ignoreOriginalLayer;
        [SerializeField]
        protected List<GameObject> focus;
        private List<Transform> focusOriginalLayer;
        private TutorialManager tutorialManager;
        [Space]

        [Header("Input override")]
        [SerializeField]
        private bool disableCheatSheet;
        public bool DisableCheatSheet => disableCheatSheet;
        [SerializeField]
        private bool disableDrawing;
        public bool DisableDrawing => disableDrawing;

        public abstract bool Complete { get; }

        private void Awake()
        {
            tutorialManager = GetComponentInParent<TutorialManager>();
            ignoreOriginalLayer = ignore.Select(gameObject => gameObject.transform.parent).ToList();
            focusOriginalLayer = focus.Select(gameObject => gameObject.transform.parent).ToList();
        }

        public virtual void Play() 
        { 
            foreach (GameObject gameObject in ignore)
            {
                gameObject.transform.SetParent(tutorialManager.Ignore, true);
            }
            foreach (GameObject gameObject in focus)
            {
                gameObject.transform.SetParent(tutorialManager.Focus, true);
            }
        }

        public virtual void Close() 
        {
            for (int i = 0; i < ignore.Count; i++)
            {
                ignore[i].transform.SetParent(ignoreOriginalLayer[i]);
            }
            for (int i = 0; i < focus.Count; i++)
            {
                focus[i].transform.SetParent(focusOriginalLayer[i]);
            }
        }
    }
}
