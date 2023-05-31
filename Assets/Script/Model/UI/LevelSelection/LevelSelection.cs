using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Com.oHMysTArs.Level
{
    public sealed class LevelSelection : MonoBehaviour
    {
        [SerializeField]
        private LevelButton levelButtonPrefab;
        [SerializeField]
        private GridLayoutGroup levelLayoutGrid;
        [SerializeField]
        private AudioSource UIAudio;

        private void Awake()
        {
            List<Level> staticLvlData = Resources.LoadAll<Level>("Level").ToList();
            List<LevelResult> playedLvlData = LevelResult.Load();
            List<Level> lockedLvls = staticLvlData.Where(lvl => !playedLvlData.Any(playedLvl => playedLvl.Name == lvl.name)).ToList();
            foreach (LevelResult result in playedLvlData)
            {
                LevelButton lvl = InstantiateLevelButton();
                lvl.Init(result);
            }
            Level nextUnlockedLevel = lockedLvls.FirstOrDefault();
            if (nextUnlockedLevel != null)
            {
                LevelButton lvl = InstantiateLevelButton();
                lvl.Init(nextUnlockedLevel.name);
            }
            lockedLvls.Remove(nextUnlockedLevel);
            foreach (Level level in lockedLvls)
            {
                LevelButton lvl = InstantiateLevelButton();
                lvl.Init(level);
            }
        }

        private LevelButton InstantiateLevelButton() 
        {
            GameObject button = GameObject.Instantiate(levelButtonPrefab.gameObject, levelLayoutGrid.transform);
            button.GetComponentInChildren<Button>().onClick.AddListener(() => UIAudio.Play());
            return button.GetComponent<LevelButton>();
        }
    }
}
