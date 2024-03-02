using Model.Core.Data.Unity.Config.Base;
using UnityEngine;

namespace Model.Core.Data.Unity.Config {
    [CreateAssetMenu(menuName = "Configs/PoolsConfig")]
    public class PoolsConfig : ScriptableObject, IConfigData {

        [Header("Pools initial capacity")]
        [Tooltip("Bullets")]
        public int ammo1Capacity = 10;
        [Tooltip("Lasers")]
        public int ammo2Capacity = 5;
        [Space]
        public int ufosCapacity = 10;
        [Space]
        public int asteroidsLargeCapacity = 20;
        public int asteroidsMediumCapacity = 30;
        public int asteroidsSmallCapacity = 50;

    }
}