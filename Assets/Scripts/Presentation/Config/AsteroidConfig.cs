using UnityEngine;

namespace Presentation.Config {
    [CreateAssetMenu(menuName = "Data/AsteroidData")]
    public class AsteroidConfig : ScriptableObject {
        public float speed = 1f;

        [Header("Collision")]
        public float colliderRadius = 0.1f;

    }

}