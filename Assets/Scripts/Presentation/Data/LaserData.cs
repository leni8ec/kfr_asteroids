using UnityEngine;
using UnityEngine.Serialization;

namespace Presentation.Data {
    [CreateAssetMenu(menuName = "Data/LaserData")]
    public class LaserData : ScriptableObject {

        [FormerlySerializedAs("shotsCount")]
        [Tooltip("Max shots count")]
        public int maxShotsCount = 3;
        [Tooltip("shots per sec")]
        public float fireRate = 0.3f;
        [Space]
        public float maxDistance = 5;
        [Space]
        public float duration = 0.7f;

        [Header("Collision")]
        public float colliderRadius = 0.05f;
    }
}