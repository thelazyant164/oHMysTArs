using Com.oHMysTArs.Spaceship;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Com.oHMysTArs.Collectible
{
    public sealed class CollectibleDisplay : MonoBehaviour
    {
        [SerializeField]
        private CollectibleItem collectibleItem;
        [SerializeField]
        private GridLayoutGroup grid;
        [SerializeField]
        private TextMeshProUGUI count;
        [SerializeField]
        private TextMeshProUGUI total;
        [SerializeField]
        private Button backButton;

        private void Awake()
        {
            backButton.onClick.AddListener(() => SceneManager.LoadSceneAsync("MenuScene"));
            List<SpaceshipCollectible> unlocked = SpaceshipCollectible.Load();
            List<SpaceshipSO> spaceships = Resources.LoadAll<SpaceshipSO>("Spaceship/Model").ToList();
            List<SpaceshipSO> unlockedSpaceships = spaceships.Where(spaceship => unlocked.Any(unlocked => unlocked.Name == spaceship.name)).ToList();
            List<SpaceshipSO> lockedSpaceships = spaceships.Except(unlockedSpaceships).ToList();
            total.SetText(spaceships.Count.ToString());
            count.SetText(unlockedSpaceships.Count.ToString());
            foreach (SpaceshipSO spaceship in unlockedSpaceships)
            {
                CollectibleItem spaceshipCollectible = SpawnCollectibleItem();
                spaceshipCollectible.InitUnlock(spaceship.Texture);
            }
            foreach (SpaceshipSO spaceship in lockedSpaceships)
            {
                CollectibleItem spaceshipCollectible = SpawnCollectibleItem();
                spaceshipCollectible.InitLock(spaceship.UnlockTexture);
            }
        }

        private CollectibleItem SpawnCollectibleItem() => GameObject.Instantiate(collectibleItem, grid.transform).GetComponent<CollectibleItem>();
    }
}
