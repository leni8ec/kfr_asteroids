using UnityEngine;

namespace Presentation.Data {
    [CreateAssetMenu(menuName = "Data/AsteroidData")]
    public class AsteroidData : ScriptableObject {
        public float speed = 0.5f;

        [Header("Collision")]
        public float colliderRadius = 10;

    }

}