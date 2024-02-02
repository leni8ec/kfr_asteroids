using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Config {
    [CreateAssetMenu(menuName = "Data/LaserData")]
    public class LaserConfig : ScriptableObject {

        [FormerlySerializedAs("shotsCount")]
        [Tooltip("Max shots count")]
        public int maxShotsCount = 3;
        [Tooltip("Delay to restore shot")]
        public float shotCountdown = 3;
        [Space]
        [Tooltip("shots per sec")]
        public float fireRate = 0.3f;
        [Space]
        public float maxDistance = 5;
        [Space]
        [Tooltip("lifetime")]
        public float duration = 0.7f;

        [Header("Collision")]
        public float colliderRadius = 0.05f;
    }
}