using UnityEngine;

namespace Presentation.Data {
    [CreateAssetMenu(menuName = "Data/LaserData")]
    public class LaserData : ScriptableObject {

        [Tooltip("shots per sec")]
        public float fireRate = 0.3f;
        [Space]
        public float duration = 1;
        public float maxDistance = 10;

        [Header("Collision")]
        public float colliderRadius = 0.1f;
    }
}