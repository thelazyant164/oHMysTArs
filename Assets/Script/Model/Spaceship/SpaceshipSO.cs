using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.oHMysTArs.Pattern;

namespace Com.oHMysTArs.Spaceship
{
    [CreateAssetMenu(fileName = "New Spaceship", menuName = "SpaceshipSO")]
    public sealed class SpaceshipSO : ScriptableObject
    {
        public Pattern.Pattern Pattern;
        public bool VIP;
        public Texture2D Texture;
        public Texture2D ScanTexture;

        public static SpaceshipSO Init(string name, Pattern.Pattern pattern, bool vip, Texture2D texture, Texture2D scanTexture)
        {
            SpaceshipSO spaceship = ScriptableObject.CreateInstance<SpaceshipSO>();
            spaceship.name = name;
            spaceship.Pattern = pattern;
            spaceship.VIP = vip;
            spaceship.Texture = texture;
            spaceship.ScanTexture = scanTexture;
            return spaceship;
        }
    }
}
