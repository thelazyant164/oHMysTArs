using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.oHMysTArs.UI
{
    public sealed class PauseButton : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => 
            { 
                GameManager.Instance.TogglePause(true); 
                PopUpManager.Instance.ShowPause();
            });;
        }
    }
}
