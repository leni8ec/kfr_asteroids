using Model.Core.Data.Unity.Config.Base;
using Model.Core.Interface.Containers;
using UnityEngine;

namespace Model.Core.Data.Unity.Config {
    [CreateAssetMenu(menuName = "Configs/BulletConfig")]
    public class BulletConfig : ScriptableObject, IConfigData, IColliderRadiusContainer {
        [Tooltip("shots per sec")]
        public float fireRate = 5;
        [Space]
        public float speed = 5;
        [Tooltip("in sec")]
        public float lifetime = 2;

        [Header("Collision")]
        public float colliderRadius = 0.1f;
        public float ColliderRadius => colliderRadius;

    }

}