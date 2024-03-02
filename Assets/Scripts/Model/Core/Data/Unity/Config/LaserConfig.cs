using Model.Core.Data.Unity.Config.Base;
using Model.Core.Interface.Containers;
using UnityEngine;

namespace Model.Core.Data.Unity.Config {
    [CreateAssetMenu(menuName = "Configs/LaserConfig")]
    public class LaserConfig : ScriptableObject, IConfigData, IColliderRadiusContainer {

        [Tooltip("Max shots count")]
        public int maxShotsCount = 3;
        [Tooltip("Delay to restore shot")]
        public float shotRestoreCountdown = 3;
        [Space]
        [Tooltip("shots per sec")]
        public float fireRate = 1f;
        [Space]
        public float maxDistance = 8;
        [Space]
        [Tooltip("lifetime")]
        public float duration = 1f;

        [Header("Collision")]
        public float colliderRadius = 0.05f;
        public float ColliderRadius => colliderRadius;

    }
}