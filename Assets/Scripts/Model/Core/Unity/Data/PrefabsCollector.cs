using UnityEngine;

namespace Model.Core.Unity.Data {
    public class PrefabsCollector : MonoBehaviour {
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