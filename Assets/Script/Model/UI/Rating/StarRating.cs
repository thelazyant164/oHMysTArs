using Com.oHMysTArs.Assessment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.oHMysTArs.UI
{
    public sealed class StarRating : MonoBehaviour
    {
        [SerializeField]
        private GameObject unfilledStarPrefab;
        [SerializeField]
        private GameObject starPrefab;
        private List<GameObject> stars = new();

        public void Init(Rating rating)
        {
            for (int i = 0; i < 5 - (int)rating; i++)
            {
                stars.Add(GameObject.Instantiate(unfilledStarPrefab, transform));
            }
            for (int i = 0; i < (int)rating; i++)
            {
                stars.Add(GameObject.Instantiate(starPrefab, transform));
            }
        }

        public void Reset() 
        {
            foreach(GameObject star in stars)
            {
                GameObject.Destroy(star);
            }
            stars.Clear();
        }
    }
}
