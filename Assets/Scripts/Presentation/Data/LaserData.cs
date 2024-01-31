using UnityEngine;

namespace Presentation.Data {
    [CreateAssetMenu(menuName = "Data/LaserData")]
    public class LaserData : ScriptableObject {
        public float duration = 1;
        public float maxDistance = 10;
        [Space]
        public float colliderRadius = 10;
    }
}