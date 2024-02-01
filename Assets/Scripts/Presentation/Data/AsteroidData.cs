using UnityEngine;

namespace Presentation.Data {
    [CreateAssetMenu(menuName = "Data/AsteroidData")]
    public class AsteroidData : ScriptableObject {
        public float speed = 1f;

        [Header("Collision")]
        public float colliderRadius = 0.1f;

    }

}