using UnityEngine;

namespace Presentation.Data {
    [CreateAssetMenu(menuName = "Data/BulletData")]
    public class BulletData : ScriptableObject {
        [Tooltip("shots per sec")]
        public float fireRate = 5;
        [Space]
        public float speed = 5;
        [Tooltip("in sec")]
        public float lifetime = 2;

        [Header("Collision")]
        public float colliderRadius = 10;
    }

}