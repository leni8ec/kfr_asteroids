using UnityEngine;

namespace Presentation.Data {
    [CreateAssetMenu(menuName = "Data/BulletData")]
    public class BulletData : ScriptableObject {
        public float speed = 5;
        public float maxDistance = 10;

        [Space]
        public float colliderRadius = 10;
    }

}