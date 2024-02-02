using UnityEngine;

namespace Core.Config {
    [CreateAssetMenu(menuName = "Data/BulletData")]
    public class BulletConfig : ScriptableObject, IConfigData {
        [Tooltip("shots per sec")]
        public float fireRate = 5;
        [Space]
        public float speed = 5;
        [Tooltip("in sec")]
        public float lifetime = 2;

        [Header("Collision")]
        public float colliderRadius = 0.1f;
    }

}