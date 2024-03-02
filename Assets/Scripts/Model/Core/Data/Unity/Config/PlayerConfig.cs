using Model.Core.Data.Unity.Config.Base;
using Model.Core.Interface.Containers;
using UnityEngine;

namespace Model.Core.Data.Unity.Config {
    [CreateAssetMenu(menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject, IConfigData, IColliderRadiusContainer {
        public float speed = 3f;
        [Tooltip("in sec to full speed")]
        public float accelerationInertia = 0.5f;
        public float brakingInertia = 5f;
        public float leftOverInertia = 2f;
        [Tooltip("Degrees in sec")]
        public float rotationSpeed = 180;

        [Header("Collision")]
        public float colliderRadius = 0.1f;
        public float ColliderRadius => colliderRadius;

    }

}