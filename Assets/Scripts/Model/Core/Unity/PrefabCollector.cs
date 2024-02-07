using Model.Core.Unity.Helpers;
using UnityEngine;

namespace Model.Core.Unity {
    public class PrefabCollector : MonoBehaviourHandler<PrefabCollector> {
        public GameObject player;

        [Header("Weapon")]
        public GameObject bullet;
        public GameObject laser;

        [Header("Enemies")]
        public GameObject asteroidLarge;
        public GameObject asteroidMedium;
        public GameObject asteroidSmall;
        [Space]
        public GameObject ufo;

    }
}