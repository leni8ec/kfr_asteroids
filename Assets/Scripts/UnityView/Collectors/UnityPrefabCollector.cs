using UnityEngine;

namespace UnityView.Collectors {
    public class UnityPrefabCollector : MonoBehaviour {
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