using UnityEngine;

namespace Core.Config {
    [CreateAssetMenu(menuName = "Data/AsteroidData")]
    public class AsteroidConfig : ScriptableObject, IConfigData {
        public float speed = 1f;

        [Header("Collision")]
        public float colliderRadius = 0.1f;

    }

}