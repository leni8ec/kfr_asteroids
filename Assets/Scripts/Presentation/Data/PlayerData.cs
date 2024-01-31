using UnityEngine;

namespace Presentation.Data {
    [CreateAssetMenu(menuName = "Data/PlayerData")]
    public class PlayerData : ScriptableObject {
        public float speed = 1.5f;
        [Tooltip("Shots in sec")]
        public float fireRateGun = 3;
        [Tooltip("Shots in sec")]
        public float fireRateLaser = 0.3f;
        [Tooltip("Degrees in sec")]
        public float rotationSpeed = 90;

        [Space]
        public float colliderRadius = 10;
    }

}