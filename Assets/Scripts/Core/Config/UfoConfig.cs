using Core.Interface.Config;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Config {
    [CreateAssetMenu(menuName = "Data/UfoData")]
    public class UfoConfig : ScriptableObject, IConfigData, IColliderRadiusContainer {
        public float startSpeed = 1;
        public float huntSpeed = 1.2f;

        [Tooltip("in seconds")]
        public float huntDelay = 3;

        [Header("Collision")]
        public float colliderRadius = 0.1f;
        public float ColliderRadius => colliderRadius;

    }
}