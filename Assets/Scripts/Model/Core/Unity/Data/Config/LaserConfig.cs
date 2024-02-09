﻿using Model.Core.Interface.Config;
using Model.Core.Interface.Containers;
using UnityEngine;

namespace Model.Core.Unity.Data.Config {
    [CreateAssetMenu(menuName = "Data/LaserData")]
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