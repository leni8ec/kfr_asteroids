using UnityEngine;
using UnityEngine.Serialization;

namespace Presentation.Data {
    [CreateAssetMenu(menuName = "Data/UfoData")]
    public class UfoData : ScriptableObject {
        [FormerlySerializedAs("speed")]
        public float startSpeed = 1;
        public float huntSpeed = 1.2f;

        [Tooltip("in seconds")]
        public float huntDelay = 3;

        [Header("Collision")]
        public float colliderRadius = 10;
    }
}