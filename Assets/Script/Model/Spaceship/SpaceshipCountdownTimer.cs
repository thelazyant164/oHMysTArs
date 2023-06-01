using Com.oHMysTArs.Level;
using Com.oHMysTArs.Queue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.oHMysTArs.Spaceship
{
    public sealed class SpaceshipCountdownTimer : MonoBehaviour
    {
        [SerializeField]
        private Image VIP;
        private Slider countdown;
        private QueueSystem queueSystem;
        private bool vip;

        private void Awake()
        {
            countdown = GetComponentInChildren<Slider>();
            queueSystem = GetComponentInParent<QueueSystem>();
            countdown.maxValue = queueSystem.ServingTime;
        }

        private void Start()
        {
            VIP.gameObject.SetActive(false);
            countdown.gameObject.SetActive(false);
            vip = GetComponentInParent<Spaceship>().VIP;
        }

        private void FixedUpdate()
        {
            if (queueSystem.CurrentQueueState is not Serving) 
            {
                VIP.gameObject.SetActive(false);
                countdown.gameObject.SetActive(false);
                return; 
            }
            VIP.gameObject.SetActive(vip);
            countdown.gameObject.SetActive(true);
            countdown.value = (queueSystem.CurrentQueueState as Serving).RemainingTime;
        }
    }
}
